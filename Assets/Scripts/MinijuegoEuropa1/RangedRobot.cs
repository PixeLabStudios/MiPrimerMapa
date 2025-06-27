using UnityEngine;
using UnityEngine.AI;

public class RangedRobot : Unit
{
    Europe1Manager manager;
    float bulletSpeed;
    float range;
    NavMeshAgent agent;
    public GameObject bullet;
    public Transform player;
    public Transform spawn;
    float nextAttack=0;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerScript>().transform;
        manager = FindFirstObjectByType<Europe1Manager>();
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
        if (CanAttack())
        {
            agent.isStopped = true;
            Attack();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    public override void Attack()
    {
        Vector3 direction = player.transform.position - transform.position;
        
        direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 4);
        if (Time.time >=nextAttack) 
        { 
           
          GameObject  d=  Instantiate(bullet,spawn.position,Quaternion.identity);
          BulletMove script = d.GetComponent<BulletMove>();
          script.SetTarget(player.position,spawn.position);
          script.speed = bulletSpeed;
          script.damage = damage;
          nextAttack = Time.time + 1/attackRate;
          
        }
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
