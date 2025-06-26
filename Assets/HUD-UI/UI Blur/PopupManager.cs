using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PopupManager : MonoBehaviour
{
    //public GameObject popup;
    public BlurFadeController blurController;

    public void ActivateBlur()
    {
        blurController.FadeIn();
        //popup.SetActive(true);
    }

    public void DisactivateBlur()
    {
        blurController.FadeOut();
        //popup.SetActive(false);
    }
}
