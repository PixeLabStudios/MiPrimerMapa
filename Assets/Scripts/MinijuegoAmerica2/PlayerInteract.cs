using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    FirstPersonController controller;
    Door currentDoor;
    public float interactDistance = 1.5f;
    public LayerMask layerMask;

    private void Awake()
    {
        controller = GetComponent<FirstPersonController>();
    }
    private void Update()
    {
        Debug.Log(currentDoor);
        //cast a ray
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactDistance, layerMask))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door)) 
            { 
                currentDoor = door;
            }
        }
        else 
        {
            currentDoor = null;
        }

        switch (controller.current) 
        {
            case FirstPersonController.Device.PC:
                if (Input.GetKeyDown(KeyCode.E) && currentDoor != null)
                {
                    controller.StartCoroutine(currentDoor.roomParent.CheckAnswer(currentDoor, transform.position));
                }
                break;
            case FirstPersonController.Device.Mobile:
                //if (controller.interactButton.IsPressed && currentDoor != null)
                //{
                //    controller.StartCoroutine(currentDoor.roomParent.CheckAnswer(currentDoor, ));
                //}
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, gameObject.transform.position  -transform.forward * -3);
    }
}
