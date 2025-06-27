using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int maxHp;
    public int hp;
    public int damage;
    public int moveSpeed;
    public float attackRate;
   
    public abstract void Attack();

   

    public void TakeDamage(int value)    
    {
        hp -= value;
        //mover al atacado un poquito hacia atras donde mira
        
        if (hp<=0) { OnDeath(); }
    }
    public void SetStats(int hpValue, int speed, int damageValue, float rate)
    {
        maxHp = hpValue;
        hp = maxHp;
        moveSpeed = speed;
        damage = damageValue;
        attackRate = rate;

    }
    public abstract void OnDeath();
}
