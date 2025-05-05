using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoTestManager : MonoBehaviour
{
    public List<Sprite> imagesList = new List<Sprite>();
    public GameObject prefab;
    public Transform canvasTransform;
    public bool canClick;
    public List<CardScript> selectedCards = new List<CardScript>();
    public int score;
    public int errors;
    public TextMeshPro text;
    List<CardScript> cardList = new List<CardScript>();  
    CardScript[,] cardsGrid;
    
    private void Start()
    {
        CreateCards();
        cardsGrid = new CardScript[4, 3];
        canClick = true;
    }
    /// <summary>
    /// Crea dos cartas por cada imagen en la lista de imagenes
    /// </summary>
    void CreateCards() 
    {
        int i = 0;
        GameObject ob;
        CardScript cardscript;
        foreach (Sprite image in imagesList)
        {
          ob = Instantiate(prefab, canvasTransform);
          cardscript = ob.GetComponent<CardScript>();          
          cardscript.ChangeImage(image);
          cardscript.id = i;
          cardList.Add(cardscript);

          ob = Instantiate(prefab, canvasTransform);
          cardscript  = ob.GetComponent<CardScript>();            
          cardscript.ChangeImage(image);
          cardscript.id = i;
          cardList.Add(cardscript);
            i++;
        }
    }
    /// <summary>
    /// mezcla las cartas dentro de la matriz
    /// </summary>
    void ShuffleCards() 
    {
        cardsGrid = new CardScript[4,3];
        int x, y;
        foreach (CardScript cardscript in cardList) 
        {
            do 
            {
                x = Random.Range(0, cardsGrid.GetLength(0));
                y = Random.Range(0, cardsGrid.GetLength(1));
            } while (cardsGrid[x,y] != null);      
            cardsGrid[x, y] = cardscript;
            
        }
    }
    void MoveCards() 
    {
        for (int i = 0; i < cardsGrid.GetLength(0); i++)
        {
            for (int j = 0; j < cardsGrid.GetLength(1); j++)
            {
                cardsGrid[i, j].transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(i* 240+ -700,j * 240 -200);
            }
        }
    }

    IEnumerator ShowAllCards(int time) 
    {
        canClick = false;
        foreach (CardScript cardscript in cardList)
        {
            
           StartCoroutine( cardscript.ShowCard());
        }
        yield return new WaitForSeconds(time);
        foreach (CardScript cardscript in cardList)
        {
           StartCoroutine( cardscript.HideCard());
        }
        canClick = true;
    }
    void HideAllCards()
    {
        foreach (CardScript cardscript in cardList)
        {
            StartCoroutine(cardscript.HideCard());

        }
    }
        public void StartGame() 
    {
        score = 0;
        errors = 0;
        ShuffleCards();
        HideAllCards();
        MoveCards();
        StartCoroutine(ShowAllCards(3));
    }
    public IEnumerator CheckCards() 
    {
        canClick = false;
        yield return new WaitForSeconds(1);
        if (selectedCards[0].id == selectedCards[1].id)
        {
            score++;
        }
        else 
        {
            errors++;
            text.text = "erroes" + errors;           
            selectedCards[0].StartCoroutine(selectedCards[0].HideCard());
            selectedCards[1].StartCoroutine(selectedCards[1].HideCard());
        }
        selectedCards.Clear();
        canClick = true;
    }
}
