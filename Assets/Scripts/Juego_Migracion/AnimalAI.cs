using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class AnimalAI : MonoBehaviour
{
    public Transform originPoint;
    public Animator animator;
    public float detectRadius = 8f;
    public float provokeDelay = 0.5f;
    public bool isAggressive = true;
    protected GameObject player;
    public NavMeshAgent agent;
    protected bool returning;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAggressive || returning) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < detectRadius && CanAttack())
        {
            StartCoroutine(AttackSequence());
        }
    }

    protected abstract bool CanAttack();
    protected abstract IEnumerator AttackSequence();

    protected IEnumerator ReturnToOrigin()
    {
        returning = true;
        agent.isStopped = false;
        agent.SetDestination(originPoint.position);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, originPoint.position) < 0.5f);
        agent.isStopped = true;
        returning = false;
        animator.Play("Idle");
    }
}
