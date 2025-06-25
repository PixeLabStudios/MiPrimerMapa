using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


public class BossScript : MonoBehaviour
{
    Vector3 targetPos;
    public Transform[] playSpots;
    public Transform retreatSpot;
    public List<TurretScript> sideTurrets;
    public TurretScript mainTurret;
    public int currentStage;

    float moveSpeed;
   
    public int hp;
    int MaxHp;
    bool canLoseHP;
    
    public enum Direction 
    {
       Left,
       Right,
       Center,
       Retreat
    }
    public Direction currentDirection;
    void Start()
    {
        foreach (var turret in sideTurrets) 
        {
            SetTurretStats(turret,2,1,25);
        }
        SetTurretStats(mainTurret,5,4,35);
        canLoseHP = false;
        currentStage = 0;
     // currentDirection = Direction.Retreat;
        MaxHp = 120;
        hp = MaxHp;      
        moveSpeed = 20;
     
       
       
    }

    public void SetTurretStats(TurretScript turret,float newCool,float size, float speed) 
    {
        turret.speed = speed;
        turret.fireCooldown = newCool;
        turret.scale = size;
    }
    private void Update()
    {
        MovePosition(currentDirection);
    }
    void MovePosition(Direction d )
    {
        switch (d)
        {
            case Direction.Left:
                
                transform.position = Vector3.MoveTowards(transform.position, playSpots[1].position, moveSpeed * Time.deltaTime);
                
                break;
            case Direction.Right:
               
                transform.position = Vector3.MoveTowards(transform.position, playSpots[2].position, moveSpeed * Time.deltaTime);
                
                break;
            case Direction.Center:
                
                transform.position = Vector3.MoveTowards(transform.position, playSpots[0].position, moveSpeed * Time.deltaTime);
                
                break;
            case Direction.Retreat:
               
                transform.position = Vector3.MoveTowards(transform.position,retreatSpot.position, moveSpeed * Time.deltaTime);
                
                break;
        }
    }

    bool isNotMoving() 
    {
        return Vector3.Distance(transform.position, targetPos) < moveSpeed * Time.deltaTime;
    }

    private void OnEnable()
    {
        hp = 120 - 40 * currentStage;
        transform.position = retreatSpot.position;
        
        
    }
   public IEnumerator HandleMovement() 
    {
       
        canLoseHP = false;
        Debug.Log("Inicie el mov");
        //cuando se active ira al centro
        currentDirection = Direction.Center;
        targetPos = playSpots[0].position;
        yield return new WaitUntil(isNotMoving);
        canLoseHP = true;
        StartShooting();
        yield return new WaitForSeconds(5);

        while (canLoseHP)
        {
            if (canLoseHP)
            {
                currentDirection = Direction.Left;
                targetPos = playSpots[1].position;
                yield return new WaitUntil(isNotMoving);
                yield return new WaitForSeconds(4);
            }
           

            if (canLoseHP)
            {
                currentDirection = Direction.Right;
                targetPos = playSpots[2].position;
                yield return new WaitUntil(isNotMoving);
                yield return new WaitForSeconds(4);
               
            }
            if (canLoseHP)
            {
                currentDirection = Direction.Center;
                targetPos = playSpots[0].position;
                yield return new WaitUntil(isNotMoving);
                yield return new WaitForSeconds(5);
              
            }
        }

    }
    
    public void StopShooting() 
    {
        foreach (TurretScript script in sideTurrets) 
        {
            script.StopCoroutine(script.SingleShot());
        }
        mainTurret.StopCoroutine(mainTurret.SingleShot());
    }

    public void StartShooting() 
    {
        foreach (TurretScript script in sideTurrets)
        {
            script.StartCoroutine(script.SingleShot());
        }
        mainTurret.StartCoroutine(mainTurret.SingleShot());
    }
    public void DestroyTurret() 
    {
        //Dejo de disparar y quito una torreta
        StopShooting();
        Destroy(sideTurrets[sideTurrets.Count-1].gameObject);
        sideTurrets.RemoveAt(sideTurrets.Count - 1);

       
    }
    public void TakeDamage(int damage) 
    {
        if (canLoseHP)
        {
            hp -= damage;
            Debug.Log("El jefe perdioVida " + hp);
            if (hp <= 0)
            {
                Destroy(gameObject);

                StopCoroutine(HandleMovement());
                return;
            }
            float percentange = (float)hp / (float)MaxHp;
            
            switch (currentStage)
            {
                case 0:
                    if (percentange < 0.66f)
                    {
                        canLoseHP= false;
                        Debug.Log("El jefe debe retirarse " + hp);
                        DestroyTurret();
                        StopCoroutine(HandleMovement());
                        StartCoroutine(Retreat());
                       
                    }
                    break;
                case 1:
                    if (percentange < 0.33f)
                    {
                        canLoseHP = false;
                        Debug.Log("El jefe debe retirarse " + hp);
                        DestroyTurret();
                        StopCoroutine(HandleMovement());
                        StartCoroutine(Retreat());

                    }
                    break;

            }
        }
        else { Debug.Log("El jefe es inmune"); }
        
       
        
    }

    public IEnumerator Retreat() 
    {
        
        
        targetPos = retreatSpot.position;
        StopShooting();
        StopCoroutine(HandleMovement());
        yield return new WaitForSeconds(1);
        currentStage++;
        currentDirection = Direction.Retreat;
        yield return new WaitUntil(isNotMoving);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            other.GetComponent<DrakkarScript>().ChangeHp(-1);
        }
        if (other.CompareTag("balaDrakar")) 
        {
            TakeDamage(other.GetComponent<DrakkarBullet>().damage);
            Destroy(other.gameObject);
        }
    }

}
