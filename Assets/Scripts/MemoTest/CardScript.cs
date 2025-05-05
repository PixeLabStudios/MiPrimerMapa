using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardScript : MonoBehaviour
{
    public int id; // id de la carta
    public Image cardImage; // imagen de la carta
    public Button cardButton; // boton de la carta
    public bool isFlipped; // estado de la carta (volteada o no)
    MemoTestManager memoTestManager;
    private void Start()
    {       
        isFlipped = false;
        cardImage.gameObject.SetActive(false); 
        cardButton.onClick.AddListener(Onclick);
        memoTestManager = GameObject.Find("GameManager").GetComponent<MemoTestManager>();
    }
   
    void Onclick() 
    {
        if (memoTestManager.canClick ) 
        {
            StartCoroutine(ShowCard());
            memoTestManager.selectedCards.Add(this);
            if (memoTestManager.selectedCards.Count >=2) 
            {
            StartCoroutine(memoTestManager.CheckCards());
            }
        }
        

    }
    public IEnumerator ShowCard()
    {
        

        //card =Application.persistentDataPath + "/d.json";
        
        if (isFlipped==false)
        {
            cardButton.GetComponent<Button>().interactable = false;
            while (transform.rotation.eulerAngles.y < 179)
            {
               
                transform.Rotate(0, 10, 0);

                if (transform.rotation.eulerAngles.y >= 90)
                {
                    cardImage.gameObject.SetActive(true);
                }
                yield return new WaitForSeconds(0.01f);
            }
            isFlipped = !isFlipped;
        }
        
    }
    public IEnumerator HideCard() 
    {
        if (isFlipped==true) 
        {
            while (transform.rotation.eulerAngles.y > 1)
            {
                transform.Rotate(0, -10, 0);

                if (transform.rotation.eulerAngles.y <= 90)
                {
                    cardImage.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(0.02f);
            }
            cardButton.GetComponent<Button>().interactable = true;
            isFlipped = !isFlipped;
        }
       

        
    }
    public void ChangeImage(Sprite sprite) 
    {
        cardImage.sprite = sprite; 
    }      
    


}
