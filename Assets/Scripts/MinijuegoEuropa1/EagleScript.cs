using Unity.XR.Oculus.Input;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    Europe1Manager manager;
    public Transform attachPoint;
    Unit grabedEnemy;
    Vector3 restPosition;
    Vector3 target;
    public Transform outOfScreenPos;
    bool isAttacking;
    float moveSpeed;
    
   
    private void Awake()
    {
        manager = FindFirstObjectByType<Europe1Manager>();
    }
    void Start()
    {
        target = transform.position;
        restPosition = transform.position;
        moveSpeed = 10;
        Attack();
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        
    }

    public void Attack() 
    {
        isAttacking = true;
        target = manager.unitList[Random.Range(0,manager.unitList.Count)].transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemigo")) 
        {
            other.transform.SetParent(attachPoint.transform);
            grabedEnemy = other.gameObject.GetComponent<Unit>();
            Debug.Log("choque");
            target = outOfScreenPos.position;
        }
        if (other.gameObject.CompareTag("fuera")) 
        {
            // deberia ir este audio https://youtu.be/elAB59cKZRc?t=2
            grabedEnemy.GetComponent<Unit>().TakeDamage(10000);
            isAttacking = false;
            target = restPosition;
        }
        
    }
}
