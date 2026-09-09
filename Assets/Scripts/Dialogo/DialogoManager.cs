using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogoManager : MonoBehaviour
{
    [System.Serializable]
    public class LineaDialogo
    {
        public string personaje;
        public string nombre;
        public string imagen;
        public string texto;
        public string multimedia;
    }

    [System.Serializable]
    public class DialogoData
    {
        public List<LineaDialogo> dialogos;
    }

    public Image iconoPersonaje;
    public TMP_Text nombrePersonaje;
    public TMP_Text textoDialogo;
    public Image multimediaExtra;

    public GameObject panelDialogo;
    public Button btnSiguiente;


    private DialogoData dialogoActual;
    private int indiceActual = 0;

    void Start()
    {
        if (panelDialogo == null)
            panelDialogo = GameObject.Find("PanelDialogo"); // o usa tag, etc.
    }

    public void CargarDialogoDesdeJSON(TextAsset archivoJson)
    {
        dialogoActual = JsonUtility.FromJson<DialogoData>(archivoJson.text);
        indiceActual = 0;
        panelDialogo.SetActive(true);
        MostrarDialogoActual();
    }

    public void MostrarDialogoActual()
    {
        if (dialogoActual == null || indiceActual >= dialogoActual.dialogos.Count)
        {
            panelDialogo.SetActive(false);
            return;
        }

        var linea = dialogoActual.dialogos[indiceActual];
        nombrePersonaje.text = linea.nombre;
        textoDialogo.text = linea.texto;

        iconoPersonaje.sprite = Resources.Load<Sprite>(linea.imagen);
        if (!string.IsNullOrEmpty(linea.multimedia))
            multimediaExtra.sprite = Resources.Load<Sprite>(linea.multimedia);
        else
            multimediaExtra.sprite = null;
    }

    public void SiguienteDialogo()
    {
        indiceActual++;
        MostrarDialogoActual();
    }
}
