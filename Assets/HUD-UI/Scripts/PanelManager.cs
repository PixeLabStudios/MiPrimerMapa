using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject panelPrincipal;
    public GameObject[] paneles;

    private int indicePanelActivo = -1;

    void Start()
    {
        panelPrincipal = this.gameObject;
        // Opcional: desactivar todos los paneles al inicio excepto el principal
        //for (int i = 0; i < paneles.Length; i++)
        //{
            //paneles[i].SetActive(false);
        //}
        //panelPrincipal.SetActive(true);
        //indicePanelActivo = -1;
    }

    // Activa solo el panel con el índice dado y desactiva el anterior
    public void ActivarSoloPanel(int indice)
    {
        if (indice < 0 || indice >= paneles.Length)
            return;

        // Desactiva el panel activo anterior si existe
        if (indicePanelActivo >= 0 && indicePanelActivo < paneles.Length)
        {
            paneles[indicePanelActivo].SetActive(false);
        }

        // Activa el nuevo panel
        paneles[indice].SetActive(true);
        indicePanelActivo = indice;
    }
    public void changeViewAllPanels(bool visibilidad)
    {
        foreach (GameObject panel in paneles)
        {
            if (panel != panelPrincipal)
            {
                panel.SetActive(visibilidad);
            }
        }
    }
}
