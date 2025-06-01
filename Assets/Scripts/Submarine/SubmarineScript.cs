
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
        if (manager.GetCanShoot() && time > lastShot + rateOfFire)
        {
           GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position,Quaternion.identity);
           bullet.GetComponent<BulletScript>().SetDirection(target - spawnPoint.position);
           bullet.transform.LookAt(target);
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
