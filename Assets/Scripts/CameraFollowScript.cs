using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target; // The target to follow
    public Vector3 offSet;
    public float desiredDistance ; // Distance from the target
    Vector3 desiredPosition; // Desired position of the camera
    float currentDistance; // Current distance from the target
    public float speed; 
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        speed = 1.2f;
        currentDistance = Vector3.Distance(transform.position, target.position);
        desiredPosition = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime *speed);
        desiredPosition = target.position - (transform.rotation * Vector3.forward * currentDistance)+ offSet;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime *speed );



    }
    public void SetDesiredDistance(float target)
    {
        desiredDistance = target;
    }

}
