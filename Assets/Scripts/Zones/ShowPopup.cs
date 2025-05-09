using UnityEngine;

public class ShowPopup : MonoBehaviour
{
    public int index;
    public GameObject popup;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            popup.SetActive(false);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            popup.SetActive(true);
        }
    }
}
