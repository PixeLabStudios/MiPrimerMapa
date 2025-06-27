using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float range = 2f;
    public LayerMask targetLayers;
    public float nextAttack = 0f;
    public int damage;
    public float rate;

    public void Attack(int damage) 
    {
       
       Collider[] colliders = Physics.OverlapSphere(attackPoint.position,range, targetLayers);
       
        foreach (Collider collider in colliders) 
        {
            if (collider.TryGetComponent<Unit>(out var unit))
            {
                unit.TakeDamage(damage);
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        
        if (attackPoint == null) { return; }
        Gizmos.DrawSphere(attackPoint.position, range);
    }
}
