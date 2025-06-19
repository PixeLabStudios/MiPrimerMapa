using UnityEngine;

public class Flag : MonoBehaviour
{
    public int id;
    string countryName;
    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }
    
    public void ReturnToPosition() 
    {
        transform.position = initialPosition;
    }
}
