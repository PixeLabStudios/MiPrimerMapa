using UnityEngine;

public class RunnerCamera : MonoBehaviour
{

    public Transform target;
    public Vector3 offSet;
    Vector3 desiredPosition;
    public float distance;
    RunnerScript1 runnerScript1;
    private void Start()
    {
        runnerScript1 = target.GetComponent<RunnerScript1>();
    }
    private void Update()
    {
       MoveCamera();
    }
    void MoveCamera() 
    {
        transform.position += new Vector3(runnerScript1.movementSpeed * Time.deltaTime, 0, 0);                
        //desiredPosition = target.position - (transform.rotation * Vector3.forward * distance) + offSet;
        //desiredPosition.y = 0;
        //transform.position = desiredPosition;
    }
}
