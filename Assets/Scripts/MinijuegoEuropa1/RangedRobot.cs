using UnityEngine;
using UnityEngine.AI;

public class RangedRobot : Unit
{
    Animator anim;
    Europe1Manager manager;
    float bulletSpeed;
    float range;
    NavMeshAgent agent;
    Collider col;
    public GameObject bullet;
    public Transform player;
    public Transform spawn;
    float nextAttack=0;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerScript>().transform;
        manager = FindFirstObjectByType<Europe1Manager>();
        col= GetComponent<Collider>();
        
    }

    private void Start()
    {
        moveSpeed = 6;
        agent.speed = moveSpeed;
        bulletSpeed = 20f;
        range = 26f;
        attackRate = 0.5f;
    }
    bool CanAttack() 
    {
        return Vector3.Distance(transform.position,player.position) <= range;
    }

    

    
    void Update()
    {
        if (transform.parent == null)
        {
            if (CanAttack())
            {
                agent.isStopped = true;
                anim.SetBool("moving", false);
                Attack();
            }
            else
            {
                agent.isStopped = false;
                anim.SetBool("moving", true);
                agent.SetDestination(player.position);
                anim.ResetTrigger("attack");
                
            }
        }
        else 
        {
            anim.SetTrigger("grabed");
            agent.enabled = false;
            col.enabled = false;
        }
    }

    public override void Attack()
    {
        Vector3 direction = player.transform.position - transform.position;
        
        direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 4);
        if (Time.time >=nextAttack) 
        {
            anim.SetTrigger("attack");
          GameObject  d=  Instantiate(bullet,spawn.position,Quaternion.identity);
          BulletMove script = d.GetComponent<BulletMove>();
          script.SetTarget(player.position,spawn.position);
          script.speed = bulletSpeed;
          script.damage = damage;
          nextAttack = Time.time + 1/attackRate;
          
        }
    }
    public override void TakeDamage(int damage)
    {
        
        
            anim.SetTrigger("hurt");        
            base.TakeDamage(damage);
    }
    public override void OnDeath()
    {
        
        if (manager.unitList.Contains(this)) 
        {
            manager.unitList.Remove(this);
        }
            Destroy(gameObject);
    }
}
