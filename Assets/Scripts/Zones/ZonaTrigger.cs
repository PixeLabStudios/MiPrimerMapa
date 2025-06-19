using UnityEngine;

public class ZonaTrigger : MonoBehaviour
{
    public int idZona;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ZonaPanelController.panelController != null)
        {
            Debug.Log("jugador esta en zona : " + idZona);
            ZonaPanelController.panelController.MostrarZona(idZona);
        }
    }
}
