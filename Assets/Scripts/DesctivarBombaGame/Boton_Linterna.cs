using UnityEngine;
using UnityEngine.UI;

public class Boton_Linterna : MonoBehaviour
{
    public GameObject text_Invicible;
    private bool bandera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bandera = true;
    }

    public void PresionarBoton()
    {
        if (bandera)
        {
            text_Invicible.SetActive(true);
            bandera = false;
        }
        else
        {
            text_Invicible.SetActive(false);
            bandera = true;
        }
    }
}
