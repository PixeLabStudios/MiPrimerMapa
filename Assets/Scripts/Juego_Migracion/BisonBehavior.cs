using UnityEngine;
using System.Collections;

public class BisonBehavior : AnimalAI
{
    public float chargeDistance = 10f;
    public float chargeSpeed = 10f;

    protected override bool CanAttack() => true;

    protected override IEnumerator AttackSequence()
    {
        isAggressive = false;
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        directionToPlayer.y = 0f; // No queremos que suba/baje

        transform.rotation = Quaternion.LookRotation(directionToPlayer);
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(provokeDelay);
        //Vector3 target = transform.position + transform.forward * chargeDistance;
        //Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        //directionToPlayer.y = 0f; // No queremos que suba/baje

        //transform.rotation = Quaternion.LookRotation(directionToPlayer);
        float traveled = 0f;

        while (traveled < chargeDistance)
        {
            float step = chargeSpeed * Time.deltaTime;
            transform.position += transform.forward * step;
            traveled += step;
            yield return null;
        }
        animator.SetBool("attack", false);
        //StartCoroutine(ReturnToOrigin());
        yield return new WaitForSeconds(1f);

        isAggressive = true;
    }
}
