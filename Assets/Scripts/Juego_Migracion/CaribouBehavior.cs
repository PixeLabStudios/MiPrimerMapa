using System.Collections;
using UnityEngine;

public class CaribouBehavior : AnimalAI
{
    public float chargeDistance = 12f;
    public float chargeSpeed = 10f;
    public float groupRadius = 15f;

    protected override bool CanAttack()
    {
        return true;
    }

    protected override IEnumerator AttackSequence()
    {
        isAggressive = false;
        animator.Play("Provoke");
        yield return new WaitForSeconds(provokeDelay);

        // Notificar a todos los caribúes cercanos
        Collider[] colliders = Physics.OverlapSphere(transform.position, groupRadius);
        foreach (Collider col in colliders)
        {
            CaribouBehavior c = col.GetComponent<CaribouBehavior>();
            if (c != null && c != this)
            {
                c.StartCoroutine(c.IndividualCharge());
            }
        }

        yield return StartCoroutine(IndividualCharge());

        StartCoroutine(ReturnToOrigin());
        yield return new WaitForSeconds(2f);
        isAggressive = true;
    }

    private IEnumerator IndividualCharge()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        float traveled = 0f;

        while (traveled < chargeDistance)
        {
            float step = chargeSpeed * Time.deltaTime;
            transform.position += dir * step;
            traveled += step;
            yield return null;
        }
    }
}
