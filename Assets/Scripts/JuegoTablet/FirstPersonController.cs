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
    Vector3 direction;
    CharacterController controller;
    public Transform orientation;
    public Joystick movementJoystick;
    public Joystick lookJoystick;   
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        
    }

    private void Start()
    {
        moveSpeed = 10;
        sensitivity = 180;
    }
    private void Update()
    {
        Move();
        Look();
    }

    void Move() 
    {
        direction = new Vector3()
        {
            x = transform.forward.x * movementJoystick.Vertical +transform.right.x * movementJoystick.Horizontal,
            y = 0f,
            z = transform.forward.z * movementJoystick.Vertical + transform.right.z * movementJoystick.Horizontal
        };
       
        controller.Move(direction * moveSpeed * Time.deltaTime);
      
    }
    public void Look()
    {
        horizontal = lookJoystick.Horizontal * Time.deltaTime * sensitivity;
        vertical =   lookJoystick.Vertical * Time.deltaTime * sensitivity;

        yRotation += horizontal;
        xRotation -= vertical;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        orientation.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
