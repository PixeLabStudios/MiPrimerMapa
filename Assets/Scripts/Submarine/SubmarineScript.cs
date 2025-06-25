
using UnityEngine;

public class SubmarineScript : MonoBehaviour
{
    float rateOfFire;
    float lastShot;
    float time;
    SubmarineGameManager manager;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public LayerMask backgroundMask;
    void Start()
    {
        manager = FindFirstObjectByType<SubmarineGameManager>();
        rateOfFire = .8f;
        lastShot = 0f;      
        time = 0f;
    }

    void Shoot(Vector3 target)
    {
        Vector3 direction = target - spawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (manager.GetCanShoot() && time > lastShot + rateOfFire && angle <90 && angle >0)
        {
           GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position,Quaternion.identity);
            
            bullet.GetComponent<BulletScript>().SetDirection(direction);
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
               lastShot = time;
        }
    }
    void Update()
    {
        time += Time.deltaTime;
        CheckInputMobile();
    }

    void CheckInputMobile()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Touch touch = Input.GetTouch(0);

            Shoot(GetTouchPosition(touch));
        }
    }
    Vector3 GetTouchPosition(Touch touch)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        Vector3 position = Vector3.zero;
        if (Physics.Raycast(ray, out hit, 1000, backgroundMask))
        {         
            position = hit.point;
            position.z = Camera.main.nearClipPlane;         
            return position;
        }
        else 
        {
            return Vector3.zero; 
        }


    }

    void CheckInputPC() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, backgroundMask))
            {
                Shoot(hit.point);
            }
            else 
            {
                Debug.Log("no toque fondo");
            }
        }
    }
}
