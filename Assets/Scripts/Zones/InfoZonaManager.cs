using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class InfoZonaManager : MonoBehaviour
{
    public Text nombre;
    public Text descripcion;

    private List<Zona> zonas;

    void Start()
    {
        string json = Resources.Load<TextAsset>("InfoAntartida").text;
        ZonaData data = JsonUtility.FromJson<ZonaData>(json);
        zonas = data.zonas;
    }

    public void UpdateData(int id)
    {
        foreach (Zona zona in zonas)
        {
            if (zona.id == id)
            {
                nombre.text = zona.nombre;
                descripcion.text = zona.descripcion;
                return;
            }
        }

        nombre.text = "";
        descripcion.text = "";
    }
}
