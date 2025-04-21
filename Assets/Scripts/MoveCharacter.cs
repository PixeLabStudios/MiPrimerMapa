using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveCharacter : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
