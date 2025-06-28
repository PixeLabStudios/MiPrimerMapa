using UnityEngine;
using UnityEngine.AI;

public class State
{
   public enum STATE 
    {
        ATTACK,RUN,FLY,DEAD
    };
    public enum EVENT 
    {
        ENTER,UPDATE,EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject robot;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;
    protected MeleeAttack meleeAttack;
     
   public bool CanAttackPlayer(float range) 
    {
        
        return Vector3.Distance(robot.transform.position, player.transform.position) <= range +1;
       
    }
    public State( GameObject robot, Animator anim, Transform player, NavMeshAgent agent,MeleeAttack melee)
    {
        
        this.robot = robot;
        this.anim = anim;
        this.player = player;
        this.stage = EVENT.ENTER;
        this.agent = agent;
        this.meleeAttack = melee;
    }
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process() 
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT) 
        {
            Exit();
            return nextState;
        }

        return this;
    }

   
}
