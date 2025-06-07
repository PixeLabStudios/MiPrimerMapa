using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;


public class AudioGameManager : MonoBehaviour
{
    int correct; // Player's correct answers
    int errors; // Player's errors
    int currentRound; // Current round number
    int maxRounds; 
    public string answer; 
    public bool candrag;
    AudioClip audioClip; 
    AudioSource source;
    SerializableList<AudioData> audioDataList;
    SerializableList<AudioData> audioDataRandom;

    #region UI
    StarScoreDisplay scoreDisplay;
    public DragAndDrop[] flags;
    public TextMeshProUGUI resultText;
    public GameObject gameOverPanel;
    #endregion


    private void Awake()
    {
        source = GetComponent<AudioSource>();
        scoreDisplay = FindFirstObjectByType<StarScoreDisplay>();
    }
    private void Start()
    {
        resultText.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        scoreDisplay = FindFirstObjectByType<StarScoreDisplay>();
        candrag = true;         
        correct = 0;
        maxRounds = 3; 
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

        Debug.Log("El pais elegido es " + answer);
    }

    public IEnumerator CheckAnswer(string country, string flag) 
    {


        resultText.gameObject.SetActive(true);
        if (country == flag && answer ==flag)
        {
            correct++; // Incrementa el contador de aciertos si la respuesta es correcta
            Debug.Log("correcta");
            resultText.text = "Correcto!"; 
        }
        else
        {    
            errors++; // Incrementa el contador de errores si la respuesta es incorrecta
            Debug.Log("incorrecta, la correcta era: " + answer);
            resultText.text = "Incorrecto! La respuesta correcta era: " + answer; // Muestra la respuesta correcta
        }
        candrag = false; // Desactiva el arrastre de las banderas mientras se verifica la respuesta
        yield return new WaitForSeconds(2f); // Espera un segundo antes de continuar
        
        currentRound++; // Incrementa el contador de rondas
        

        if (currentRound > maxRounds) 
        {
           Debug.Log("Juego terminado!"); 
           float scorePercent = correct * 40; 
            Debug.Log("Puntuacion: " + scorePercent );
            gameOverPanel.SetActive(true); 
            scoreDisplay.ShowStars(scorePercent); 
        }
        else 
        {
            candrag = true;
            Load(); 
            Debug.Log("Ronda " + currentRound + " de " + maxRounds);
            resultText.gameObject.SetActive(false);
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
