using UnityEngine;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
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
            panelActivoActual = panelPrincipal;
        }
    }

    public void MostrarSoloPanel(string nombre)
    {
        if (!paneles.ContainsKey(nombre))
        {
            Debug.LogWarning($"Panel '{nombre}' no encontrado.");
            return;
        }

        if (panelActivoActual != null)
            panelActivoActual.SetActive(false);

        paneles[nombre].SetActive(true);
        panelActivoActual = paneles[nombre];
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
