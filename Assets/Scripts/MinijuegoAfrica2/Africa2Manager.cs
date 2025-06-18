using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Africa2Manager : MonoBehaviour
{
    public List<AnimalButtonScript> buttonsList = new(); 
    List<AnimalButtonScript> buttonsRandom = new();
    public List<AfricanAnimal> animals = new();
    List<AfricanAnimal> animalRandom = new();
    public List<Sprite> fakeAnimals= new();
    public List<Sprite> regions = new();
    public int errors;
    int buttonCount;
    string currentRegion;
    public int currentRound;
    public int correct;

    public GameObject starPanel;
    public GameObject regionPanel;
    public TextMeshProUGUI regionName;
    StarScoreDisplay starScoreDisplay;

    private void Awake()
    {
        starScoreDisplay = FindFirstObjectByType<StarScoreDisplay>();
    }
    private void Start()
    {
        foreach (AnimalButtonScript button in buttonsList) 
        {
            buttonsRandom.Add(button);
        }

        starPanel.SetActive(false);
        errors = 0;
        buttonCount = buttonsRandom.Count;
        Shuffle();
        currentRegion = "sabana";
        correct = 0;
        currentRound = 1;
        StartCoroutine(ShowRegion());
    }

    void Shuffle() 
    {
        
        correct = 0;
        int index;
        foreach (AnimalButtonScript script in buttonsList) 
        {
            script.Delete();  
        }
        List<int> random = new List<int>();
        for (int i = 0;i< buttonCount;i++) 
        {
            random.Add(i);
            
        }


        int contador = 0;
        foreach (AfricanAnimal a in animals) 
        {
            index = Random.Range(0, random.Count);
            
            buttonsList[random[index]].LoadData(a);
                       
            random.RemoveAt(index);
            contador++;
        }
        Debug.Log("ejecute esto: " + contador + " veces");
    }

    IEnumerator ShowRegion() 
    {
      
        int randomIndex;
        randomIndex = Random.Range(0, regions.Count);
        Sprite sprite = regions[randomIndex];
        regionPanel.gameObject.SetActive(true);
        currentRegion = sprite.name;
        regionPanel.gameObject.GetComponent<Image>().sprite = sprite;
        regionName.text = currentRegion;
        regions.RemoveAt(randomIndex);
        //quizas mostrar animacion de puerta abriendose
        yield return new WaitForSeconds(4f);
        
        regionPanel.SetActive(false);

    }
    public void NextRound(int i) 
    {
        correct = 0;
        switch (i)
        {
            case 2: //Ronda 2
                //agrego los animales falsos.
                Debug.Log("Inicio Ronda 2");
                FillFakeAnimal();
                StartCoroutine(ShowRegion());
            break;

            case 3: //Ronda 3
                Debug.Log("Inicio Ronda 3");
                //mezclo los animales y Lleno los lugares que faltan con animales falsos 
                Shuffle();
                FillFakeAnimal();
                StartCoroutine(ShowRegion());
                break;
            
            case 4: //Termino el juego Mostrar panel de estrellas 
                Debug.Log("Termino el juego");
                starPanel.SetActive(true);
                int points = 120 - errors * 10;
                starScoreDisplay.ShowStars(Mathf.Clamp(points,20,points));
                
                break;
        }
    }

    void FillFakeAnimal() 
    {
        int randomNumber;
        foreach (AnimalButtonScript animal in buttonsList) 
        {
            if (animal.data==null) 
            {
                randomNumber = Random.Range(0, fakeAnimals.Count);
                Sprite sprite = fakeAnimals[randomNumber];
                animal.LoadFake(sprite);
                fakeAnimals.RemoveAt(randomNumber);
            }

        }

    }

    public string GetCurrentRegion() {
    return currentRegion;
    }

    /// <summary>
    /// Remueve de la lista el animal ya seleccionado correctamente
    /// </summary>
    /// <param name="animal"></param>
    public void RemoveAnimal(AfricanAnimal animal) 
    {
        animals.Remove(animal);


        Debug.Log("quite un animal "+ animal.name );
    }
}
