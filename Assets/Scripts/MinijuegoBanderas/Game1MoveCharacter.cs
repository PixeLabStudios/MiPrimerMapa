using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Game1MoveCharacter : MonoBehaviour
{
    public LayerMask mask;
    public TextMeshProUGUI text;
    public Animator animator;
    NavMeshAgent agent;
    bool isUsingMouse;
    private Vector3 lastPosition;
    public enum State
    {
        idle,
        moving,
        stunned,
    }
    public State currentState;
    private void Awake()
    {
        
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) 
        {
            isUsingMouse = false;
        }
        else
        {
            isUsingMouse = true;
        }
    }
    private void Start()
    {
        currentState = State.idle;
        agent = GetComponent<NavMeshAgent>();    
        

    }
    public void StopCharacter() 
    {
        agent.isStopped = true;
    }
    public void ControlMovement()
    {
        
        text.text = Application.platform.ToString();
        if (isUsingMouse)
        {
            Debug.Log("Mouse");
            //logica para mover el personaje con el ratón
        }
        else 
        {
            if (Input.touchCount ==1 && GetTouchPosition() != Vector3.zero) 
            {
                MoveCharacter(GetTouchPosition());
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Debug.Log("touch ended");
                    agent.isStopped = true;
                }              
                else 
                {
                    agent.isStopped = false;
                }
            }

           

            //logica para mover el personaje con el touch
        }
        ControlAnimation();
    }

    void ControlAnimation() 
    {
        Vector3 currentPosition = transform.position;

        // Comparamos si ha cambiado de posición
        if (currentPosition != lastPosition)
        {
            animator.SetBool("Change", true);
            //Debug.Log("Se está moviendo");
            currentState = State.moving;
        }
        else
        {
            animator.SetBool("Change", false);
           // Debug.Log("Está quieto");
            // Mirar suavemente hacia la cámara

            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 direction = cameraPosition - transform.position;
            direction.y = 0f; // Mantenemos la altura del personaje

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            currentState = State.idle;


        }
        lastPosition = currentPosition;
    }
    void MoveCharacter(Vector3 target)
    {
        agent.SetDestination(target);
    }
     Vector3 GetTouchPosition() 
    {
        Touch firstTouch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(firstTouch.position);
        if (Physics.Raycast(ray, out  hit,1000,mask))
        {
            
            return hit.point; 

        }
        return Vector3.zero;
    }
   


}
