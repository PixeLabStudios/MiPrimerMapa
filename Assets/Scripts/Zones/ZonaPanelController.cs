using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;

public class ZonaPanelController : MonoBehaviour
{

    public static ZonaPanelController panelController;
    [Header("UI")]
    public TMP_Text nombreText;
    public TMP_Text descripcionText;
    public Image imagenDisplay;
    public VideoPlayer videoPlayer;
    public AudioSource narrador;
    public GameObject panel;
    public Button botonSiguiente;
    public Button botonAnterior;

    [Header ("JSON")]

    public TextAsset jsonFile;

    private List<Zona> zonas;
    private int imagenActual = 0;
    private Zona zonaActual;

    void Start()
    {
        string json = jsonFile.text;
        ZonaData data = JsonUtility.FromJson<ZonaData>(json);
        zonas = new List<Zona>(data.zonas);

        panel.SetActive(false);
        //Debug.Log(json + "Zonas cargadas: " + zonas.Count);
    }
    private void Awake()
    {
        if (panelController == null)
        {
            panelController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MostrarZona(int id)
    {
        zonaActual = zonas.Find(z => z.id == id);

        if (zonaActual != null)
        {
            panel.SetActive(true);  
            nombreText.text = zonaActual.nombre;
            descripcionText.text = zonaActual.descripcion;

            imagenActual = 0;
            MostrarImagen();

            if (!string.IsNullOrEmpty(zonaActual.video))
            {
                videoPlayer.clip = Resources.Load<VideoClip>(zonaActual.video);
            }

            if (!string.IsNullOrEmpty(zonaActual.narracion))
            {
                narrador.clip = Resources.Load<AudioClip>(zonaActual.narracion);
            }

            Debug.Log("Zona mostrada: " + zonaActual.nombre);
        }
        else
        {
            Debug.LogWarning("Zona con ID " + id + " no encontrada en JSON");
        }
    }

    public void CerrarPanel()
    {
        panel.SetActive(false);
        narrador.Stop();
        videoPlayer.Stop();
    }

    public void EscucharNarracion()
    {
        if (narrador.clip != null)
        {
            narrador.Play();
        }
    }

    public void MostrarSiguienteImagen()
    {
        if (zonaActual == null || zonaActual.imagenes.Count == 0) return;
        imagenActual = (imagenActual + 1) % zonaActual.imagenes.Count;
        MostrarImagen();
    }

    public void MostrarAnteriorImagen()
    {
        if (zonaActual == null || zonaActual.imagenes.Count == 0) return;
        imagenActual = (imagenActual - 1 + zonaActual.imagenes.Count) % zonaActual.imagenes.Count;
        MostrarImagen();
    }

    private void MostrarImagen()
    {
        Sprite imagen = Resources.Load<Sprite>(zonaActual.imagenes[imagenActual]);
        if (imagen != null)
        {
            imagenDisplay.sprite = imagen;
        }
    }
}
