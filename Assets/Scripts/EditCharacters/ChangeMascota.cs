using System.Collections.Generic;
using UnityEngine;

public class ChangeMascota : MonoBehaviour
{
    public List<GameObject> mascotas = new List<GameObject>();

    [HideInInspector]public int currentMascota = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //mascotas[currentMascota].SetActive(true);
        /*if (mascotas != null)
        {
            for (int i = 0; i < mascotas.Count; i++)
            {
                mascotas[i].SetActive(false);
            }
        }*/
    }

    public void MascotaLeft()
    {
        if (currentMascota > 0)
            currentMascota++;
        else
            currentMascota = mascotas.Count - 1;

        ActivarMascota(currentMascota);
    }
    public void MascotaRight()
    {
        if (currentMascota < mascotas.Count - 1)
            currentMascota++;
        else
            currentMascota = 0;

        ActivarMascota(currentMascota);
    }

    void ActivarMascota(int indice)
    {
        for (int i = 0; i < mascotas.Count; i++)
        {
            if (i == indice)
            {
                mascotas[indice].SetActive(true);
            }
            else
                mascotas[i].SetActive(false);
        }
    }

    public void DesactivarMascotas()
    {
        if (mascotas != null)
        {
            for (int i = 0; i < mascotas.Count; i++)
            {
                mascotas[i].SetActive(false);
            }
        }
    }
}
