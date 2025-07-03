using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Room roomParent;
    public string doorName;
    public Sprite sprite;
    public Image  image;
    
    #region doorMovement
    bool isOpen;
    Vector3 forward;
    float speed = 2f;
    float rotationAngle = 90f;
    float forwardDirection = 0;
    Coroutine rotationCoroutine;
    Vector3 startRotation;
    #endregion
    private void Awake()
    {
        roomParent = GetComponentInParent<Room>();
    }
    void Start()
    {
        
        isOpen = false; 
        forward = transform.forward;
        startRotation = transform.rotation.eulerAngles;
    }


    
   public void Open(Vector3 player)
    {
        if (!isOpen) 
        {
            if (rotationCoroutine !=null) 
            {
                StopCoroutine(rotationCoroutine);
            }
            float dot  = Vector3.Dot(forward, (player - transform.position).normalized);
            Debug.Log($"Dot: {dot }");
            rotationCoroutine = StartCoroutine(OpenDoor(dot));
        }
    }
    IEnumerator OpenDoor(float direction)
    {
       Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (direction >= forwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y + rotationAngle, 0));
        }
        else 
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y - rotationAngle, 0));
        }

        isOpen = true;
        float elapsedTime = 0f;
        while (elapsedTime < 1f )
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

    }
    void Close()
    {
        if (isOpen) 
        {
            if (rotationCoroutine !=null) 
            {
                StopCoroutine(rotationCoroutine);
            }
            rotationCoroutine = StartCoroutine(CloseDoor());
        }
    }
    IEnumerator CloseDoor()
    {
        Quaternion start = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation);
        isOpen = false;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Lerp(start, endRotation, elapsedTime);
            yield return null;
            elapsedTime += Time.deltaTime * speed;
        }
    }
    public void Choose() { }
}
