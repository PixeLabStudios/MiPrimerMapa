using UnityEngine;

public class RunnerCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet;
     Vector3 desiredPosition;
    public float distance;

    private void Update()
    {
       MoveCamera();
    }
    void MoveCamera() 
    {
        desiredPosition = target.position - (transform.rotation * Vector3.forward * distance) + offSet;
        desiredPosition.y = 0;
        transform.position = desiredPosition;
    }
}
