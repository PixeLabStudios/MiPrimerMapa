using System.Collections;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    Europe2Manager manager;
    public GameObject prefab;
    public Transform spawnLocation;
    public float fireCooldown;
    public float fireTime;
    public float scale;
    public float speed;

    void Start()
    {
        manager = FindFirstObjectByType<Europe2Manager>();
        fireCooldown = 3;
        
    }
    private void Update()
    {
        LookAtPlayer();
    }
    public IEnumerator SingleShot() 
    {
        while (true) 
        {
            GameObject b = Instantiate(prefab,spawnLocation.position,prefab.transform.rotation);
            Bulletship script = b.GetComponent<Bulletship>();
            script.transform.localScale *= scale;
            script.speed = speed;
            b.GetComponent<Bulletship>().SetTarget(manager.drakkarScript.GetDrakkarPos(),spawnLocation.position) ;
            yield return new WaitForSeconds(fireCooldown);
        }
    }
    void LookAtPlayer() 
    {
        Vector3 direction = (manager.drakkarScript.GetDrakkarPos()-spawnLocation.position);
        direction = Vector3.Normalize(direction);
        float angleX = Mathf.Atan2(direction.z, direction.y) * Mathf.Rad2Deg;
        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0,angleY+180,0);
        rotation = Quaternion.Normalize(rotation);
       transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,90*Time.deltaTime);
        
    }
    public void OnDisable()
    {
        StopCoroutine(SingleShot());
    }
}
