using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardScript : Card
{
   
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
    
    


}
