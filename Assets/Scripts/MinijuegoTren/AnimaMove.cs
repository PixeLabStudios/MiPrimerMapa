using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class AnimalMove : MonoBehaviour
{
    NavMeshAgent agent;
    public int points;
    public Vector3 initialPos;
    public AfricanAnimal data;

    void Awake() 
    
    {
        agent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
    }
    private void Start()
    {
        points = 10;
    }

    public void MoveToCenter(Vector3 target) 
    {
       target.x -= Random.Range(-5f, 5f);
       target.z -= Random.Range(-10f, 10f);
       agent.isStopped = false;
       agent.SetDestination(target);      
    }
    public void MoveTo(Vector3 target) 
    {
        
        agent.SetDestination(target);
        
    }
    
    public void Warp() 
    {
        
        bool a= agent.Warp(initialPos);
       
    }
    public bool IsMoving() 
    {
        return agent.velocity.magnitude > 0.05f;
    }
    

}
