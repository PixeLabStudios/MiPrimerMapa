using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    Vector3 direction;
    Rigidbody rb;
    CharacterController controller;
    public Transform cameraTransform;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    public void Move(int speed, Vector3 input, bool hasTurn)
    {

        float delta = speed * Time.deltaTime;
        direction = SetDirection(input);
        
        Turn(hasTurn, input);

        controller.Move(delta * direction);
    }
    public void MoveRB(int speed, Vector3 input, bool hasToTurn) 
    {
        
        direction = SetDirection(input);
        Turn(hasToTurn, input);

        direction.x = direction.x * speed; 
        direction.z = direction.z * speed;
        direction.y = rb.linearVelocity.y; 
        rb.linearVelocity= direction;
       // rb.MovePosition(transform.position + direction *speed *0.25f);
    }
    void Turn(bool hasMouse,Vector3 input) 
    {
        if (hasMouse) // Mira donde esta el mouse
        {
            Vector3 objective = MousePosition();
            objective.y = transform.position.y;
            Vector3 lookDirection = objective - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 6);
        }

        else  // Mira hacia donde se mueve, calculado por la direccion
        {
            if (input.y != 0 || input.x != 0) // Solo gira si se mueve, sino se queda mirando donde estaba
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 6);
            }

        }
    }
    public static Vector3 MousePosition()
    {
        Vector3 mousePos = new Vector3(0, 0, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            mousePos = raycastHit.point;
            return mousePos;
        }
        return mousePos;
    }

    Vector3 SetDirection(Vector3 input)
    {
        Vector3 inputDirection = new Vector3(input.x, 0, input.y).normalized;
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return forward * inputDirection.z + right * inputDirection.x;
        }
        else 
        {
            return inputDirection;
        }
    }
}
