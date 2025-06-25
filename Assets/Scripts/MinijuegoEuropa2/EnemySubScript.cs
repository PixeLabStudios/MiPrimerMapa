using System.Collections;
using UnityEngine;

public class EnemySubScript : MonoBehaviour
{
    Europe2Manager manager;
    float speed;
    float fireRate;
    bool canShoot;
    float lastShot;
    float initialY;  
    float targetY;
    public GameObject torpedoPrefab;
    public Transform spawn;
    
    float time;
    private void Awake()
    {
        manager = FindFirstObjectByType<Europe2Manager>();

    }
    void Start()
    {
        
        fireRate = 5;
        lastShot = 0;
        time = 0;
        speed = 13;   
        initialY = transform.position.y;
        
    }
    private void OnEnable()
    {
        Debug.Log("Active");
        StartCoroutine(Resurface());
        
    }


    public IEnumerator Resurface() 
    {        
        yield return new WaitUntil(ReachedTarget);
        targetY = -1.3f;
        canShoot = true;             
    }
    public IEnumerator Retreat() 
    {
        targetY = initialY;
        canShoot= false;
        yield return new WaitUntil(ReachedTarget);      
        gameObject.SetActive(false);
    }
    bool ReachedTarget() 
    {
        float distance = Vector3.Distance(transform.position,new Vector3(transform.position.x,targetY,transform.position.z));
        return distance < 0.01f ;
    }
    void Update()
    {
        time += Time.deltaTime;  
      
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x, targetY, transform.position.z),speed * Time.deltaTime);
        if (canShoot && (time > lastShot + fireRate))
        {
            Shoot();
            lastShot = time;
        }
    }

    
    void Shoot()
    {
        Vector3 direction = manager.drakkarScript.GetDrakkarPos() - spawn.position;
     
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;     
        GameObject bullet = Instantiate(torpedoPrefab, spawn.position, Quaternion.identity);
        Bulletship script = bullet.GetComponent<Bulletship>();
        script.SetTarget(manager.drakkarScript.GetDrakkarPos(),spawn.position);
        script.speed = speed;
        
        bullet.transform.rotation = Quaternion.Euler(0, angle, 0);     
    }
}
