using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    int index;
    public List<GameObject> popUps = new List<GameObject>();
     GameObject currentCanvas;
    public GameObject buttonBack;
    public GameObject buttonNext;
    private void Start()
    {
        index = 0;
        currentCanvas = popUps[index];
        currentCanvas.SetActive(true);
        buttonBack.SetActive(false);
    }
    public void NextCanvas() 
    {
        popUps[index].SetActive(false);
        buttonBack.SetActive(true);
        if (index < popUps.Count - 1)
        {
            index++;          
        }
        if (index ==popUps.Count-1) 
        {
            buttonNext.SetActive(false);
        }
        
       
        popUps[index].SetActive(true);
    }
    public void PreviousCanvas() 
    {
       
        popUps[index].SetActive(false);
        buttonNext.SetActive(true);
        if (index >0) 
        {
            index--;
        }
        if (index ==0) 
        {
            buttonBack.SetActive(false);
        }
        
        popUps[index].SetActive(true);
    }

    //public void SkinColorRigh()
    //{
    //    if (skinColorIndex < skinColors.Count - 1)
    //        skinColorIndex++;
    //    else
    //        skinColorIndex = 0;
    //    ApplySelection(CustomSystem.TypeCloth.SKIN_COLOR, skinColorIndex);


    //}
    //public void SkinColorLeft()
    //{
    //    if (skinColorIndex > 0)
    //        skinColorIndex--;
    //    else
    //        skinColorIndex = skinColors.Count - 1;
    //    ApplySelection(CustomSystem.TypeCloth.SKIN_COLOR, skinColorIndex);
    //}

        public void OpenInformation(GameObject canvas) 
    {
        canvas.SetActive(true);
    }
    public void CloseInformation(GameObject canvas)
    {
        canvas.SetActive(false);
    }

}
