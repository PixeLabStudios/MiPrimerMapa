using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target; // The target to follow
    public float desiredDistance ; // Distance from the target
    Vector3 desiredPosition; // Desired position of the camera
    float currentDistance; // Current distance from the target
    public float speed; 
    void Start()
    {
        speed = 1.2f;
        currentDistance = Vector3.Distance(transform.position, target.position);
        desiredPosition = transform.position;
        desiredDistance = 20f;   
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime *speed);
        desiredPosition = target.position - (transform.rotation * Vector3.forward * currentDistance);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime *speed );



    }
    
}
