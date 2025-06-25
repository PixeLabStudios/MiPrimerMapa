using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DrakkarScript : MonoBehaviour
{
    
    CharacterController controller;
    public GameObject bulletPrefab;
    public Joystick joystick;
    Vector3 inputMobile;
    Vector3 inputKeyboard;

    #region Movement
    float limitVerticalBottom;
    float limitVerticalTop;
    float limitHorizontalLeft;
    float limitHorizontalRight;
    int moveSpeed;
    int lowestSpeed;
    int maxSpeed;
    #endregion

    #region Health
    int maxHp;
    int currentHp;
    bool canBeHit;
    #endregion

    #region Shooting
    float lastShoot;
    float fireRate;
   
    float time;
    public Transform bulletSpawn;
    #endregion




    public enum Device {
        EDITOR,
        MOBILE,
        PC,
    }
    public Device currentDevice;
   
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    public IEnumerator GiveInvincibility() 
    { 
        canBeHit = false;
        yield return new WaitForSeconds(1.5f);
        canBeHit = true;
    }
    private void Start()
    {
        currentDevice = Device.PC;
        maxHp = 5;
        currentHp = maxHp;
        moveSpeed = 30;
        lowestSpeed = 15;
        maxSpeed = 60;
        inputMobile = new Vector3(0, 0, 0);
        inputKeyboard = new Vector3(0, 0, 0);
        limitVerticalBottom = transform.position.z;
        limitVerticalTop = transform.position.z + 50;
        limitHorizontalLeft = transform.position.x - 35;
        limitHorizontalRight = transform.position.x + 35;

        lastShoot = 0;
        fireRate = 0.5f;
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        inputMobile.x = joystick.Horizontal;
        inputMobile.z = joystick.Vertical;

        inputKeyboard.x = Input.GetAxis("Horizontal");
        inputKeyboard.z = Input.GetAxis("Vertical");
        Move(inputKeyboard);

        if (Input.GetMouseButton(0)) 
        {
            Shoot();
        }
    }
    
    void Move(Vector3 input)
    {
        // Reviso que no se pase de los limites de pantalla
        if (moveSpeed * Time.deltaTime * input.z + transform.position.z > limitVerticalTop || moveSpeed * Time.deltaTime * input.z + transform.position.z < limitVerticalBottom)
        {
            input.z = 0;
        }
        if (moveSpeed * Time.deltaTime * input.x + transform.position.x > limitHorizontalRight || moveSpeed * Time.deltaTime * input.x + transform.position.x < limitHorizontalLeft)
        {
            input.x = 0;
        }
        controller.Move(moveSpeed * Time.deltaTime * input);


    }

    public void ChangeSpeed(int value) 
    {
        moveSpeed += value;
        moveSpeed = Mathf.Clamp(moveSpeed,lowestSpeed,maxSpeed);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.TryGetComponent<Obstacle>(out var script)) 
        { 
            script.Impact(this); 
        }
    }
    public void ChangeHp(int a) 
    
    {
        if (canBeHit)
        {
            currentHp += a;
            StartCoroutine(GiveInvincibility());
        }
        else {
            Debug.Log("soy inmune");
        }
       
    }
    
    public Vector3 GetDrakkarPos() 
    {
       return transform.position;
    }

    void Shoot()
    {
        if (time > lastShoot +fireRate) 
        {
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            lastShoot = time;
        }
        
    }
    
}
