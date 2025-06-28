using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackState :State
{
    
    public AttackState(GameObject robot, Animator anim, Transform player, NavMeshAgent agent, MeleeAttack script) : base(robot, anim, player, agent,script) 
    {
        name = STATE.ATTACK;     
        agent.isStopped = true; 
    }
    public override void Enter()
    {
        
        //animacion ataca
        base.Enter();

    }
    public override void Update()
    {
        Vector3 direction= player.transform.position - robot.transform.position;
        float angle = Vector3.Angle(direction, robot.transform.forward);
        direction.y = 0;

        robot.transform.rotation = Quaternion.Slerp(robot.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 4);
        if (Time.time > meleeAttack.nextAttack) 
        {
            meleeAttack.Attack(meleeAttack.damage);
            meleeAttack.nextAttack = Time.time +1f /meleeAttack.rate;
        }

        if (!CanAttackPlayer(agent.stoppingDistance)) 
        {
            nextState = new RunState(robot,anim,player,agent,meleeAttack);
            stage = EVENT.EXIT;
        }
       
    }
    public override void Exit()
    { 
        //terminar animacion de ataque;
        base.Exit(); 
    }

}
