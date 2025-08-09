using UnityEngine;

public class Spike : MonoBehaviour
{
    int damage = 10;
    float rate = 2f;
    int speedReduction = 7;
    float timeSinceLastHit = 0f;
    Ben4Script playerScript;

    private void Awake()
    {
        playerScript = FindFirstObjectByType<Ben4Script>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player")) 
        {
            playerScript.ChangeSpeed(-speedReduction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            playerScript.ChangeSpeed(speedReduction);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player") && Time.time >timeSinceLastHit)
        {
            playerScript.TakeDamage(damage);
            timeSinceLastHit = Time.time + 1/rate;
        }
    }
}
