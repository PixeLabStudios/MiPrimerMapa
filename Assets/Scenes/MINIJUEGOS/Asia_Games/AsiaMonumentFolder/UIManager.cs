using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    //public static UIManager Instance;

    public List<PinController> allPins;
    public GameObject errorPanel;
    public TextMeshProUGUI errorText;

    //void Awake()
    //{
    //    if (Instance == null) Instance = this;
    //}

    public void HideAllPinPanels()
    {
        foreach (PinController pin in allPins)
        {
            pin.HidePanel();
        }
    }

    public void ShowError(string msg)
    {
        errorText.text = msg;
        errorPanel.SetActive(true);
        Invoke("HideError", 2f);
    }

    void HideError()
    {
        errorPanel.SetActive(false);
    }
}
