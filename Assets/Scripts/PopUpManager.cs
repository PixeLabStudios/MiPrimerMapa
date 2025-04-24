using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{


    public void OpenInformation(GameObject canvas) 
    {
        canvas.SetActive(true);
    }
    public void CloseInformation(GameObject canvas)
    {
        canvas.SetActive(false);
    }

}
