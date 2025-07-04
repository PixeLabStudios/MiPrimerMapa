using UnityEngine;

public class PlaySound : MonoBehaviour
{
   public AudioClip clip;

    bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying)
        {
            isPlaying = true;
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlaying = false;
            //  clip.
        }
    }
}
