using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class FirstPersonController : MonoBehaviour
{
    
    int moveSpeed;
    int sensitivity;
    float horizontal;
    float vertical;
    float xRotation;
    float yRotation;
    Vector2 moveInput;
    Vector2 lookInput;
    Vector3 direction;
    CharacterController controller;
    public Transform orientation;
    public Joystick movementJoystick;
    public Joystick lookJoystick;
    public enum Device
    {
        PC, Mobile
    }
    public Device current = Device.Mobile;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        
    }

    private void Start()
    {
        //if (Input.touchSupported)
        //{
        //    Debug.Log("Touch is supported, setting device to Mobile.");
        //    current = Device.Mobile;
        //}
        //else 
        //{
        //    current = Device.PC;
        //}

            switch (current)
            {
                case Device.PC:
                    movementJoystick.gameObject.SetActive(false);
                    lookJoystick.gameObject.SetActive(false);
                    //Cursor.lockState = CursorLockMode.Locked;
                    break;
                case Device.Mobile:
                    movementJoystick.gameObject.SetActive(true);
                    lookJoystick.gameObject.SetActive(true);
                    break;
            }
        moveSpeed = 10;
        sensitivity = 180;
    }
    private void Update()
    {
        switch (current) 
        {
            case Device.PC:
                lookInput.x = Input.GetAxis("Mouse X");
                lookInput.y = Input.GetAxis("Mouse Y");
                moveInput.x = Input.GetAxis("Horizontal");
                moveInput.y = Input.GetAxis("Vertical");
                Move(moveInput);
                Look(lookInput);
                break;
            case Device.Mobile:
                lookInput.x = lookJoystick.Horizontal;
                lookInput.y = lookJoystick.Vertical;
                moveInput.x = movementJoystick.Horizontal;
                moveInput.y = movementJoystick.Vertical;
                Move(moveInput);
                Look(lookInput);


                break;
        }
        
    }

    void Move(Vector2 input) 
    {
        direction = new Vector3()
        {
            x = transform.forward.x * input.y + transform.right.x * input.x,
            y = 0f,
            z = transform.forward.z * input.y + transform.right.z * input.x
        };
       
        controller.Move(moveSpeed * Time.deltaTime * direction);
      
    }
    public void Look(Vector2 input)
    {
        horizontal = input.x * Time.deltaTime * sensitivity;
        vertical =   input.y * Time.deltaTime * sensitivity;

        yRotation += horizontal;
        xRotation -= vertical;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        orientation.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
