using UnityEngine;
using System.Collections;
using static State;

public class MooseBehavior : AnimalAI
{
    public float detectionRange = 10f;
    public float embestidaRange = 3f;
    public float pursuitSpeed = 4f;
    public float embestidaSpeed = 10f;

    protected override bool CanAttack()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance < detectionRange;
    }

    protected override IEnumerator AttackSequence()
    {
        isAggressive = false;
        animator.Play("Provoke");
        yield return new WaitForSeconds(provokeDelay);

        // Perseguir
        agent.isStopped = false;
        agent.speed = pursuitSpeed;

        while (Vector3.Distance(transform.position, player.transform.position) > embestidaRange)
        {
            agent.SetDestination(player.transform.position);

            if (Vector3.Distance(transform.position, originPoint.position) > detectionRange * 2f)
            {
                agent.isStopped = true;
                yield return StartCoroutine(ReturnToOrigin());
                isAggressive = true;
                yield break;
            }

            yield return null;
        }

        // Embestida
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float traveled = 0f;
        agent.isStopped = true;
        animator.Play("Attack");

        while (traveled < embestidaRange)
        {
            float step = embestidaSpeed * Time.deltaTime;
            transform.position += direction * step;
            traveled += step;
            yield return null;
        }

        StartCoroutine(ReturnToOrigin());
        yield return new WaitForSeconds(2f);
        isAggressive = true;
    }
}
