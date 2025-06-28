using UnityEngine;
using UnityEngine.AI;

public class FlyState : State
{
    public FlyState(GameObject robot, Animator anim, Transform player, NavMeshAgent agent, MeleeAttack script) : base(robot, anim, player, agent, script)
    {
        name = STATE.FLY;
        agent.enabled = false;


    }
    public override void Enter()
    {
        robot.GetComponent<Collider>().enabled = false;
        Debug.Log("Me Agarraron.  F....");
        robot.transform.localPosition = Vector3.zero;
        base.Enter();
    }
    public override void Update()
    {
       
    }
    public override void Exit() 
    {
        base.Exit();
    }
}
