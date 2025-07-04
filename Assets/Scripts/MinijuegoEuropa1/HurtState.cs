using UnityEngine;
using UnityEngine.AI;

public class HurtState : State
{
    public HurtState(GameObject robot, Animator anim, Transform player, NavMeshAgent agent, MeleeAttack script) : base(robot, anim, player, agent, script)
    {
        name = STATE.HURT;
        

    }
    float time;
    public override void Enter()
    {
        Debug.Log("Entre");
        time = 0;
        agent.ResetPath();
        anim.SetTrigger("hurt");
        // Play hurt animation
        base.Enter();
    }

    
    public override void Update()
    {
        time += Time.deltaTime;
       
        if (time >.7f) // 
        {
            if (Vector3.Distance(robot.transform.position,player.position) > 6) 
            {
                nextState = new RunState(robot, anim, player, agent, meleeAttack);
                stage = EVENT.EXIT;
                Debug.Log("Hurt State Time: " + time);
            }
            else 
            {
                nextState = new AttackState(robot, anim, player, agent, meleeAttack);
                stage = EVENT.EXIT;
                Debug.Log("Hurt State Time: " + time);
            }
        }
        
        

    }
}
