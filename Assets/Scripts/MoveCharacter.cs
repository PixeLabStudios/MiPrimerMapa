using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveCharacter : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator animator;
    private Vector3 lastPosition;

    public enum State
    {
        idle,
        moving,
        interacting
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        // Comparamos si ha cambiado de posición
        if (currentPosition != lastPosition)
        {
            animator.SetBool("Change", true);
            Debug.Log("Se está moviendo");
            currentState = State.moving;
        }
        else
        {
            animator.SetBool("Change", false);
            Debug.Log("Está quieto");
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
    public State currentState;
    void Start()
    {
        currentState = State.idle;
        agent = GetComponent<NavMeshAgent>();
        lastPosition = transform.position;

    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }
}