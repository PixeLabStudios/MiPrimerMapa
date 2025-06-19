using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ActivateButtons : MonoBehaviour
{
    public List<GameObject> righButtons = new List<GameObject>();
    public List<GameObject> leftButtons = new List<GameObject>();

    public GameObject colorRightButton;
    public GameObject colorLeftButton;
    public Image colorIconImage;
    public CustomSystem customSystem;

    void Start()
    {
        for (int i = 0; i < righButtons.Count; i++)
        {
            if (righButtons[i] != null)
            {
                righButtons[i].SetActive(false);
            }
        }

        for (int i = 0; i < leftButtons.Count; i++)
        {
            if (leftButtons[i] != null)
            {
                leftButtons[i].SetActive(false);
            }
        }
        colorLeftButton.SetActive(false);
        colorRightButton.SetActive(false);
        colorIconImage.gameObject.SetActive(false);
    }

    public void ActivarBotones(int index)
    {
        
        for (int i = 0; i < righButtons.Count; i++)
        {
            if (righButtons[i] != null)
            {
                if (i == index)
                {
                    righButtons[i].SetActive(true);
                }
                else
                righButtons[i].SetActive(false);
            }
        }

        for (int i = 0; i < leftButtons.Count; i++)
        {
            if (leftButtons[i] != null)
            {
                if (i == index)
                {
                    leftButtons[i].SetActive(true);
                }
                else
                leftButtons[i].SetActive(false);
            }
        }
    }

    public void ActivarColorButton(int index)
    {
        colorLeftButton.SetActive(true);
        colorRightButton.SetActive(true);
        colorIconImage.gameObject.SetActive(true);
        customSystem.currentObjeto = index;

        switch (index)
        {
            case 0:
                customSystem.currentCloth = customSystem.currentHair;
                customSystem.currentClothF = customSystem.currentHairF;
                break;
            case 1:
                customSystem.currentCloth = customSystem.currentChest;
                customSystem.currentClothF = customSystem.currentChestF;
                break;
            case 2:
                customSystem.currentCloth = customSystem.currentLegs;
                customSystem.currentClothF = customSystem.currentLegsF;
                break;
            case 3:
                customSystem.currentCloth = customSystem.currentFeet;
                customSystem.currentClothF = customSystem.currentFeetF;
                break;
        }
    }

    public void DesactivarColorButton()
    {
        colorLeftButton.SetActive(false);
        colorRightButton.SetActive(false);
        colorIconImage.gameObject.SetActive(false);
    }

}
