using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    FirstPersonController controller;
    Door currentDoor;
    public float interactDistance = 1.5f;
    public LayerMask layerMask;
    public Button chooseButton;
    private void Awake()
    {
        controller = GetComponent<FirstPersonController>();
    }
    private void Start()
    {
        switch (controller.current)
        {
            case FirstPersonController.Device.PC:
                chooseButton.gameObject.SetActive(false);
                break;
            case FirstPersonController.Device.Mobile:
                chooseButton.gameObject.SetActive(true);
                break;
        }
    }
    private void Update()
    {
       
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
                if (currentDoor != null)
                {
                    chooseButton.interactable =true;
                }
                else
                {
                    chooseButton.interactable = false;
                }
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, gameObject.transform.position  -transform.forward * -3);
    }

  public  void Choose() 
    {
        StartCoroutine(currentDoor.roomParent.CheckAnswer(currentDoor, transform.position));
    }
}
