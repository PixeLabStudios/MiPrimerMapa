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
            GameObject b = Instantiate(prefab,spawnLocation.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y + 270, transform.rotation.z));
            b.transform.LookAt(manager.drakkarScript.GetDrakkarPos());
            Bulletship script = b.GetComponent<Bulletship>();
            //script.transform.localScale *= scale;
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
        Quaternion rotation = Quaternion.Euler(0,angleY+90,0);
        rotation = Quaternion.Normalize(rotation);
       transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,90*Time.deltaTime);
        
    }
    void LookAtPlayer2()
    {
        Vector3 direction = manager.drakkarScript.GetDrakkarPos() - spawnLocation.position;

        // Ignoramos la inclinación en Y para que la torreta solo gire horizontalmente
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90 * Time.deltaTime);
        }
    }

    public void OnDisable()
    {
        StopCoroutine(SingleShot());
    }
}
