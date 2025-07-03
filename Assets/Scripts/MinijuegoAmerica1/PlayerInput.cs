using UnityEngine;

public class PlayerInput: MonoBehaviour
{
    public GameObject panelMobile;
    public Joystick joystick;
    int moveSpeed;
    PlayerMove movement;
    Vector3 input;
    public enum Device 
    {
        PC, Mobile
    }

    public Device current;

    private void Awake()
    {
        movement = GetComponent<PlayerMove>();
    }
    private void Start()
    {
        moveSpeed = 10;
        // currentDevice = Device.PC;
        switch (current) 
        {
            case Device.PC:
                panelMobile.SetActive(false);
                break;
            case Device.Mobile:
                panelMobile.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        switch (current)
        {
            case Device.Mobile:


                input.x = joystick.Horizontal;
                input.y = joystick.Vertical;
                input.z = 0;
                movement.Move(moveSpeed, input, true);
               

                break;
            case Device.PC:

                input.x = Input.GetAxis("Horizontal");
                input.y = Input.GetAxis("Vertical");
                input.z = 0;

                movement.Move(moveSpeed, input, true);
               
                break;
        }
    }

}
