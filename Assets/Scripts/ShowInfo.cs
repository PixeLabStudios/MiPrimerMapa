using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    public int index;
    public GameObject infoPanel;
    public GameObject button;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player entered trigger zone");
            infoPanel.SetActive(true);
            button.SetActive(false);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            infoPanel.SetActive(false);
            button.SetActive(true);
        }
    }
}
