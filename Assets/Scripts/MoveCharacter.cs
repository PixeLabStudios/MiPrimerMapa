using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveCharacter : MonoBehaviour
{
    NavMeshAgent agent;
    public enum State
    {
        idle,
        moving,
        interacting
    }
    public State currentState;
    void Start()
    {
        currentState = State.idle;
        agent = GetComponent<NavMeshAgent>();
    }
    
    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
