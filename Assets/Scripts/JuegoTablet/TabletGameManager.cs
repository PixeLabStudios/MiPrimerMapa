using System.Collections;
using UnityEngine;

public class TabletGameManager : MonoBehaviour
{
    int correctAnswers = 0;
    int incorrectAnswers = 0;
    int totalAnimals;
    public AnimalScript[] animals;
    AnimalScript currentAnimal;
    TabletScript tabletScript;
    string currentAnimalType;
    private void Awake()
    {
        tabletScript = FindFirstObjectByType<TabletScript>();
    }


    void Start()
    {
        totalAnimals = animals.Length;
    }

    public void SetAnswer(AnimalScript data) 
    {
        currentAnimalType = data.animalData.classification;
        currentAnimal =data;

    }
    public void Choose(string a) 
    {
        StartCoroutine(CheckAnswer(a));
        tabletScript.herbivoreButton.interactable = false;
        tabletScript.carnivoreButton.interactable = false;
    }

    IEnumerator CheckAnswer(string answer) 
    {
        tabletScript.resultText.gameObject.SetActive(true);
        if (answer == currentAnimalType) 
        {
            correctAnswers++;
            tabletScript.resultText.text = "Correcto!";
            Debug.Log("Correct answer! Total correct: " + correctAnswers);
            //mostrar algo en la tablet
        } 
        else 
        {
            incorrectAnswers++;
            tabletScript.resultText.text = "Incorrecto!";
            Debug.Log("Incorrect answer! Total incorrect: " + incorrectAnswers);
            //mostrar algo en la tablet
        }

        yield return new WaitForSeconds(1f); // Wait for 1 second before next action

        if (correctAnswers + incorrectAnswers >= totalAnimals)
        {
            Debug.Log("Termino el juego");
            // Termina el juego. Mostrar panel de estrellas
        }
        else 
        {
            tabletScript.resultText.gameObject.SetActive(false);
            currentAnimal.DisableCollider();
            tabletScript.HideAnimal();
        }
    }



   
}
