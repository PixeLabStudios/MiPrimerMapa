using UnityEngine;
using UnityEngine.AI;

public class MeleeRobotAI : Unit
{
   Europe1Manager manager;
   NavMeshAgent agent;
   public Transform player;
   Animator animator;
   State currentState;
   MeleeAttack meleeAttack;
    private void Awake()
    {
        meleeAttack = GetComponent<MeleeAttack>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerScript>().transform;
        manager = FindFirstObjectByType<Europe1Manager>();
    }
    private void Start()
    {
        
        
        currentState = new RunState(this.gameObject, animator,player,agent,meleeAttack);
        agent.speed= moveSpeed;
        meleeAttack.rate = attackRate;
        meleeAttack.damage = damage;
        
    }

    
    private void Update()
    { 

        currentState = currentState.Process();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
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
