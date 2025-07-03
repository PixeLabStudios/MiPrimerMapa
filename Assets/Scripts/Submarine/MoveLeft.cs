using System.Collections;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    float speed = 4f; 
    public Animator animator;
    public Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Destroy() 
    {
        yield return new WaitForSeconds(1.5f); // Wait for 2 seconds before destroying
        Destroy(gameObject); // Destroy the object
    }
}
