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

}
