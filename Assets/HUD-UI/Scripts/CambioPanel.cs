using NUnit.Framework.Constraints;
using UnityEngine;

public class CambioPanel : MonoBehaviour
{
    public GameObject actualPanel;
    public GameObject panelMascota;
    public GameObject panelGuardarropa;
    public GameObject panelHome;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actualPanel = this.gameObject;

    }

    // Update is called once per frame

    public void changeToMascota()
    {
        actualPanel.SetActive(false);
        panelMascota.SetActive(true);
    }

    public void changeToHome()
    {
        actualPanel.SetActive(false);
        panelHome.SetActive(true);
    }
    public void changeToGuardarropa()
    {
        actualPanel.SetActive(false);
        panelGuardarropa.SetActive(true);
    }

}




