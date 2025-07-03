using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    America2Manager manager;
    public Door[] doors;
    public QuestionData data;
    bool isComplete;

    private void Awake()
    {
        manager = FindFirstObjectByType< America2Manager >();
    }
    private void Start()
    {
        LoadDoors();
    }
    void LoadDoors() 
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].doorName = data.answers[i];
            doors[i].image.sprite = data.sprites[i]; // Asigna la imagen de la puerta
        }
        isComplete = false ;
    }

    public IEnumerator CheckAnswer(Door chosen, Vector3 player)
    {
        if (!isComplete)
        {
            chosen.Open(player);
            if (chosen.doorName != data.correctAnswer)
            {
                Debug.Log("incorrecto");
                //Es incorrecto, pierde una vida
                manager.hp--;
                manager.errors++;
            }
            else
            {
                Debug.Log("correcto");
                manager.correct++;
                // La respuesta es correcta, marca la sala como completa
            }
            isComplete = true; // Marca la sala como completa después de elegir una puerta
            yield return new WaitForSeconds(1.5f);
            manager.NextQuestion();
        }
        else 
        {
            Debug.Log("La sala ya ha sido completada, no se puede elegir otra puerta.");
            yield return null; // Evita elegir puertas si ya se completó la sala
        }
    }

    
}
