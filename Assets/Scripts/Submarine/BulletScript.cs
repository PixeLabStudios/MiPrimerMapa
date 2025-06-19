using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    SubmarineGameManager gameManager; 
    float speed;
    Rigidbody rb;
    Vector2 direction;
    
    private void Start()
    {
        gameManager = FindFirstObjectByType<SubmarineGameManager>();
        speed = 7f; 
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = direction * speed; // Set the bullet's velocity
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // Normalize the direction vector       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal")) 
        {
            gameManager.errors++; 
            Destroy(other.gameObject);
           
        }
        if (other.CompareTag("Submarino")) 
        {
           
           
            Destroy(other.gameObject); // Destroy the submarine
           
        }
        Destroy(gameObject); // Destroy the bullet
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
