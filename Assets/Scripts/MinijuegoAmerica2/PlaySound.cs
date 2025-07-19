using UnityEngine;

public class PlaySound : MonoBehaviour
{
   public AudioClip clip;
    public America2Manager manager;


    private void Awake()
    {
        manager = FindFirstObjectByType<America2Manager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            manager.audioSource.Stop();
            manager.audioSource.clip = clip;
            manager.audioSource.Play();
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            manager.audioSource.Stop();

        }
    }
}
