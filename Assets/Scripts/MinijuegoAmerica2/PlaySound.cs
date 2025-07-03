using UnityEngine;

public class PlaySound : MonoBehaviour
{
   America2Manager manager;
    public AudioClip clip;
   

    private void Awake()
    {
        manager = FindFirstObjectByType<America2Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
           manager.audioSource.resource = clip;
            
           manager.audioSource.Play();
            // Reproduce el sonido 
            manager.audioSource.PlayOneShot(clip);
           
            // Aquí puedes agregar lógica adicional si es necesario
            // Por ejemplo, actualizar el estado del juego o la UI
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.audioSource.Stop();
        }
    }
}
