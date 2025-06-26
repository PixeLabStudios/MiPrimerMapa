using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PopupManager : MonoBehaviour
{
    //public GameObject popup;
    public BlurFadeController blurController;

    public void ShowPopup()
    {
        blurController.FadeIn();
        //popup.SetActive(true);
    }

    public void HidePopup()
    {
        blurController.FadeOut();
        //popup.SetActive(false);
    }
}
