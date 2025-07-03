using Unity.XR.Oculus.Input;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    Europe1Manager manager;
    public Transform attachPoint;
    Unit chosenEnemy;
    Vector3 restPosition;
    Vector3 target;
    public Transform outOfScreenPos;
    bool targetIsgrabed;
    bool isAttacking;
    float moveSpeed;
    float nextAttack =0f;
    
   
    private void Awake()
    {
        manager = FindFirstObjectByType<Europe1Manager>();
    }
    void Start()
    {
        targetIsgrabed=false;
        target = transform.position;
        restPosition = transform.position;
        moveSpeed = 25;
        Attack();
    }

    
    void Update()
    {
        if (isAttacking) 
        {
            if (chosenEnemy == null)
            {
                if (manager.unitList.Count == 0)
                {
                    isAttacking = false;
                    target = restPosition;
                }
                else
                {
                    target = manager.unitList[Random.Range(0, manager.unitList.Count)].transform.position;
                }

            }
            else 
            {
                if (!targetIsgrabed) 
                {
                    target = chosenEnemy.transform.position;
                }
               
            }
            
        }
        
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        
    }

    public void Attack() 
    {
        if (manager.unitList.Count >0 && !isAttacking && Time.time > nextAttack) // si hay enemigos y no esta llendo a atacar a un enemigo
        {
            targetIsgrabed =false;
            isAttacking = true;
            chosenEnemy = manager.unitList[Random.Range(0, manager.unitList.Count)];
            target = chosenEnemy.transform.position;
            nextAttack = Time.time +20f;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemigo") && !targetIsgrabed) 
        {
            targetIsgrabed = true;
            chosenEnemy = other.GetComponent<Unit>();
            other.transform.SetParent(attachPoint.transform);            
            target = outOfScreenPos.position;
        }
        if (other.gameObject.CompareTag("fuera")) // cuando llega aqui, mata al robot y vuelve a su position
        {
            isAttacking = false;
            // deberia ir este audio https://youtu.be/elAB59cKZRc?t=2
            chosenEnemy.GetComponent<Unit>().TakeDamage(10000);
           
            target = restPosition;
            targetIsgrabed = false;
        }
        
    }
}
