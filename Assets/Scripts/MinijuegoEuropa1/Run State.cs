using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class RunState :State
{
    
    public RunState(GameObject robot, Animator anim, Transform player, NavMeshAgent agent,MeleeAttack script) : base(robot, anim, player, agent,script) 
    {
        name = STATE.RUN;      
        agent.isStopped = false;
       
       
    }
    
    public override void Enter()
    {
        Debug.Log("estoy en estado correr");
      //anim.SetTrigger("run");
        base.Enter();
    }
    public override void Update()
    {
        agent.SetDestination(player.transform.position);
        if (agent.hasPath) 
        {
            if (CanAttackPlayer(agent.stoppingDistance)) 
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
        
    }

    public override void Exit() { base.Exit(); }
    
}
