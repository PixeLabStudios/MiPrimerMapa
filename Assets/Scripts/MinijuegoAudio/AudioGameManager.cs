using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;


public class AudioGameManager : MonoBehaviour
{
    int correct; // Player's score
    int errors; // Player's errors
    int currentRound; // Current round number
    int maxRounds; 
    public string answer; // Correct answer for the current round
    AudioClip audioClip; // Audio clip for the current round
    AudioSource source;
    SerializableList<AudioData> audioDataList;
    SerializableList<AudioData> audioDataRandom;
   
    public DragAndDrop[] flags;
    public bool candrag; // Flag to indicate if dragging is allowed 
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        candrag = true;         
        correct = 0;
        maxRounds = 5; 
        errors = 0;
        currentRound = 1;
        source.playOnAwake = false; 


        audioDataList = JsonUtility.FromJson<SerializableList<AudioData>>(File.ReadAllText(Application.dataPath + "/Resources/JSON/DatosAudio.json"));
        
        Load(); 

    }

    void Load() 
    {
        audioDataRandom = JsonUtility.FromJson<SerializableList<AudioData>>(File.ReadAllText(Application.dataPath + "/Resources/JSON/DatosAudio.json"));
       
        List<int> pos = new List<int>(); // Lista de posiciones de las banderas
        pos.Add(0); // Agrega la primera bandera a la lista de posiciones
        pos.Add(1); 
        pos.Add(2); 
      
        int randomCountry;
        int randomBlock;
       
        // Asingno el correcto a una bandera
        randomCountry = Random.Range(0, audioDataRandom.list.Count); // el primer numero es para el correcto       
        randomBlock = Random.Range(0, pos.Count); // Seleccionar un bloque aleatorio para la bandera correcta
        audioClip = Resources.Load<AudioClip>(audioDataRandom.list[randomCountry].audioPath); // Cargar el audio
        answer = audioDataRandom.list[randomCountry].name; // Asignar la respuesta correcta
        flags[pos[randomBlock]].ChangeImage(Resources.Load<Sprite>(audioDataRandom.list[randomCountry].imagePath)); // Cambiar la imagen de la bandera correcta
        flags[pos[randomBlock]].flagName = audioDataRandom.list[randomCountry].name; // Asignar el nombre de la bandera correcta
        pos.RemoveAt(randomBlock); // Eliminar la posición del bloque seleccionado para evitar duplicados
        audioDataRandom.list.RemoveAt(randomCountry); // Eliminar el audio seleccionado para evitar duplicados
        // Asigno las banderas aleatorias a las demas.
        for (int i = 0; i < 2;i++) 
        {
            randomBlock = Random.Range(0, pos.Count); // Seleccionar un bloque aleatorio para las banderas incorrectas
            randomCountry = Random.Range(0, audioDataRandom.list.Count); // Seleccionar un audio aleatorio para las banderas incorrectas
            flags[pos[randomBlock]].ChangeImage(Resources.Load<Sprite>(audioDataRandom.list[randomCountry].imagePath)); // Cambiar la imagen de la bandera incorrecta
            flags[pos[randomBlock]].flagName = audioDataRandom.list[randomCountry].name; // Asignar el nombre de la bandera incorrecta
            pos.RemoveAt(randomBlock); // Eliminar la posición del bloque seleccionado para evitar duplicados
            audioDataRandom.list.RemoveAt(randomCountry); // Eliminar el audio seleccionado para evitar duplicados
            
        }

        Debug.Log("La correcto es: " + answer);
    }

    public IEnumerator CheckAnswer(string country, string flag) 
    {
        
        
        
        if (country == flag && answer ==flag)
        {
            correct++; // Incrementa el contador de aciertos si la respuesta es correcta
            Debug.Log("correcta");
        }
        else
        {    
            errors++; // Incrementa el contador de errores si la respuesta es incorrecta
            Debug.Log("incorrecta, la correcta era: " + answer);
        }
        candrag = false; // Desactiva el arrastre de las banderas mientras se verifica la respuesta
        yield return new WaitForSeconds(1.5f); // Espera un segundo antes de continuar
        
        currentRound++; // Incrementa el contador de rondas


        if (currentRound > maxRounds) 
        {
           Debug.Log("Juego terminado!"); // Imprime en la consola que el juego ha terminado
           //Mostrar panel de estrellas 
        }
        else 
        {
            candrag = true;
            Load(); // Carga los datos para la siguiente ronda
            Debug.Log("Ronda " + currentRound + " de " + maxRounds); 
        }
    }

    public void PlayAudio() 
    {
        if (audioDataRandom != null && !source.isPlaying) 
        {
            source.PlayOneShot(audioClip);
        }
        
    }



}
