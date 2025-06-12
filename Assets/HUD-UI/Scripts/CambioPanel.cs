using NUnit.Framework.Constraints;
using UnityEngine;

public class CambioPanel : MonoBehaviour
{
    public GameObject actualPanel;
    public GameObject panelMascota;
    public GameObject panelGuardarropa;
    public GameObject panelHome;
    public GameObject pj;
    public GameObject pjF;
    public GameObject mundo;
    public ChangeMascota changeMascota;
    //public ChangeColorBack changeColorBack;

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
        mundo.SetActive(false);
        changeMascota.mascotas[changeMascota.currentMascota].SetActive(true);
        //changeColorBack.cambiarcolor(changeMascota.mascotas[changeMascota.currentMascota].GetComponent<Renderer>().material.color);
    }

    public void changeToHome()
    {
        actualPanel.SetActive(false);
        panelHome.SetActive(true);
        pj.SetActive(false);
        pjF.SetActive(false);
        mundo.SetActive(true);
    }
    public void changeToGuardarropa()
    {
        actualPanel.SetActive(false);
        panelGuardarropa.SetActive(true);
        pj.SetActive(true);
        mundo.SetActive(false);
    }

}




