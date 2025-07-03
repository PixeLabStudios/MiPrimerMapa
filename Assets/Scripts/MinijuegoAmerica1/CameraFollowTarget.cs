using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollowTarget : MonoBehaviour
{

    float speed = 1.5f;
    float currentDistance;

    Vector3 desiredPosition;
    public float desiredDistance;
    public bool useLerp;
    public Vector3 offSet; // Offset from the target position
    public Transform target; // The target to follow
    void Start()
    {

        currentDistance = Vector3.Distance(transform.position, target.position);
        desiredDistance = currentDistance;
        desiredPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        if (useLerp)
        {
            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * speed);
            desiredPosition = target.position - (transform.rotation * Vector3.forward * currentDistance) + offSet;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);
        }
        else
        {
            currentDistance = desiredDistance;
            desiredPosition = target.position - (transform.rotation * Vector3.forward * currentDistance) + offSet;
            transform.position = desiredPosition;
        }
    }

    void Zoom()
    {
    }
}
