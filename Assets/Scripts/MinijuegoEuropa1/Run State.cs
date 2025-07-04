using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class RunState :State
{
    
    public RunState(GameObject robot, Animator anim, Transform player, NavMeshAgent agent,MeleeAttack script) : base(robot, anim, player, agent,script) 
    {
        name = STATE.RUN;      
        agent.isStopped = false;
      //  agent.stoppingDistance = 3f;
       
    }
    public float currenthp;
    Unit stat;
    public override void Enter()
    {
        stat = robot.GetComponent<Unit>();
        currenthp = stat.hp;
        Debug.Log("estoy en estado correr");
        anim.SetTrigger("run");
        base.Enter();
    }
    public override void Update()
    {
       
        agent.SetDestination(player.transform.position);
        if (agent.hasPath) 
        {
            if (CanAttackPlayer(6)) 
            {
               
                nextState = new AttackState(robot,anim,player,agent,meleeAttack);
                stage = EVENT.EXIT;
            }
        }
        if (robot.transform.parent !=null) 
        {
            nextState = new FlyState(robot, anim, player, agent, meleeAttack);
            stage = EVENT.EXIT;
        }
        if (currenthp > stat.hp)
        {
            Debug.Log("Cambiando a estado de herido");
            nextState = new HurtState(robot, anim, player, agent, meleeAttack);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit() {
        anim.ResetTrigger("run");
        base.Exit(); }
    
}
