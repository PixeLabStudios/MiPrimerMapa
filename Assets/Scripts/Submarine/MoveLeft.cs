using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    float speed = 4f; 
    

    // Update is called once per frame
    void Update()
    {
       transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
