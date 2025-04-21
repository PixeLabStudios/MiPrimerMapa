using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    public Transform[] positions;
    Transform currentPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPos = positions[4];
    }

    // Update is called once per frame
    public void ChangeCameraPosition(int index)
    {
        currentPos = positions[index];
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentPos.position, Time.deltaTime * moveSpeed);
        Vector3 desiredAngle = new Vector3(
            Mathf.Lerp(transform.localEulerAngles.x, currentPos.localEulerAngles.x, Time.deltaTime * rotSpeed),
            Mathf.Lerp(transform.localEulerAngles.y, currentPos.localEulerAngles.y, Time.deltaTime * rotSpeed),
            Mathf.Lerp(transform.localEulerAngles.z, currentPos.localEulerAngles.z, Time.deltaTime * rotSpeed));
        transform.localEulerAngles = desiredAngle;
    }
}
