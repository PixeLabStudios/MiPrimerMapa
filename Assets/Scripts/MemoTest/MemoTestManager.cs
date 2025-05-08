using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class MemoTestManager : MonoBehaviour
{
  //  public PlayerData playerData;
    public List<Sprite> imagesList = new();
    public GameObject prefab;
    public Transform canvasTransform;
    public bool canClick;
    public List<CardScript> selectedCards = new();
     
    
    public Button StartButton;
    List<CardScript> cardList = new();  
    CardScript[,] cardsGrid;
    MemoTestUIManager memoTestUIManager;
    int correct;
    int errors;
    private void Start()
    {
       
        memoTestUIManager = GetComponent<MemoTestUIManager>();
        CreateCards();
        cardsGrid = new CardScript[4, 3];
        canClick = false;
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
    /// <summary>
    /// ordena las cartas en la pantalla
    /// </summary>
    void MoveCards() 
    {
        Vector2 firstPoint = new Vector2(-700, -300); // punto de inicio
        float width= prefab.GetComponent<RectTransform>().rect.width *1.5f;
        float height = prefab.GetComponent<RectTransform>().rect.height *1.5f;
        for (int i = 0; i < cardsGrid.GetLength(0); i++)
        {
            for (int j = 0; j < cardsGrid.GetLength(1); j++)
            {           
                   cardsGrid[i, j].transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(i* width+ +firstPoint.x,j * height +firstPoint.y);
            }
        }
    }


    /// <summary>
    /// Muestra todas las cartas durante un tiempo determinado
    /// </summary> 
    /// <returns></returns>
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
        correct = 0;
        errors = 0;
        memoTestUIManager.ChangeCorrectText(correct);
        memoTestUIManager.ChangeErrorsText(errors);
        
        ShuffleCards();
       // HideAllCards();
        MoveCards();
        StartCoroutine(ShowAllCards(3));
        memoTestUIManager.DisableStartButton();
    }
    public IEnumerator CheckCards() 
    {
        canClick = false;
        yield return new WaitForSeconds(1);
        if (selectedCards[0].id == selectedCards[1].id)
        {
            correct++;
            memoTestUIManager.ChangeCorrectText(correct);
            if (    correct == imagesList.Count) 
            {
                memoTestUIManager.EnableStartButton();
            }
        }
        else 
        {
            canClick = false;
            errors++;
            memoTestUIManager.ChangeErrorsText(errors);
            selectedCards[0].StartCoroutine(selectedCards[0].HideCard());
            selectedCards[1].StartCoroutine(selectedCards[1].HideCard());
        }
        selectedCards.Clear();
        canClick = true;
    }
}
