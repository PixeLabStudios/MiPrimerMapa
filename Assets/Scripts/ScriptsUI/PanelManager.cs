using UnityEngine;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    public PanelTransition transicion;

    public GameObject panelPrincipal;
    public List<GameObject> panelesLista;

    private Dictionary<string, GameObject> paneles = new Dictionary<string, GameObject>();
    private GameObject panelActivoActual = null;

    void Awake()
    {
        foreach (GameObject panel in panelesLista)
        {
            if (panel != null && !paneles.ContainsKey(panel.name))
            {
                paneles.Add(panel.name, panel);
                panel.SetActive(false); // Desactiva todos al inicio
            }
        }

        if (panelPrincipal != null)
        {
            panelPrincipal.SetActive(true);
            //panelActivoActual = panelPrincipal;
        }
    }

    public void MostrarSoloPanel(string nombre)
    {
        if (transicion == null)
        {
            Debug.LogError("Transición no está asignada al PanelManager.");
            return;
        }

        if (!paneles.ContainsKey(nombre))
        {
            Debug.LogWarning($"Panel '{nombre}' no encontrado.");
            return;
        }

        GameObject nuevoPanel = paneles[nombre];

        if (panelActivoActual != null)
            StartCoroutine(transicion.FadeOut(panelActivoActual));

        StartCoroutine(transicion.FadeIn(nuevoPanel));
        panelActivoActual = nuevoPanel;
    }

    public void MostrarTodos(bool estado)
    {
        foreach (var kvp in paneles)
        {
            if (kvp.Value != panelPrincipal)
                kvp.Value.SetActive(estado);
        }
    }
}
