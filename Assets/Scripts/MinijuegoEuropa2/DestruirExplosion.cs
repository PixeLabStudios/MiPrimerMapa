using UnityEngine;

public class DestruirExplosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    
}
