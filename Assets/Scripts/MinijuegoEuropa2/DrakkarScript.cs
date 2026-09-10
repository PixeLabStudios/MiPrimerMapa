using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrakkarScript : MonoBehaviour
{
    Europa1UI ui;
    CharacterController controller;
    public GameObject bulletPrefab;
    public GameObject Fuego1;
    public GameObject Fuego2;
    public Joystick joystick;
    public GameObject mobilePanel;
    Vector3 inputMobile;
    Vector3 inputKeyboard;
    public AudioClip audioClip;
    public AudioSource audioSource;

    #region Movement
    public ShootButton shootButton;
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
        
        MOBILE,
        PC,
    }
    public Device currentDevice;
   
    private void Awake()
    {
        ui = FindFirstObjectByType<Europa1UI>();
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
       
        maxHp = 5;
        currentHp = maxHp;
        moveSpeed = 30;
        lowestSpeed = 15;
        maxSpeed = 40;
        inputMobile = new Vector3(0, 0, 0);
        inputKeyboard = new Vector3(0, 0, 0);
        limitVerticalBottom = transform.position.z;
        limitVerticalTop = transform.position.z + 50;
        limitHorizontalLeft = transform.position.x - 50;
        limitHorizontalRight = transform.position.x + 50;

        lastShoot = 0;
        fireRate = 0.5f;
        time = 0;
        canBeHit = true;
        switch (currentDevice) 
        {
            
            case Device.MOBILE:
                mobilePanel.SetActive(true);
                break;
            case Device.PC:
                mobilePanel.SetActive(false);
                break;
        }
    }
    
    private void Update()
    {
        switch (currentDevice) 
        {
            
            case Device.MOBILE:
                #region Movil
                inputMobile.x = joystick.Horizontal;
                inputMobile.z = joystick.Vertical;

                Move(inputMobile);
                if (shootButton.buttonPressed)
                {
                    Shoot();
                }
                #endregion
                break;
            case Device.PC:
                #region Pc
                inputKeyboard.x = Input.GetAxis("Horizontal");
                inputKeyboard.z = Input.GetAxis("Vertical");
                Move(inputKeyboard);
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
                #endregion
                break;
        }

        
        
        time += Time.deltaTime;
                
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
        Rotacion(input);


    }
    void Rotacion(Vector3 input)
    {
        //controller.transform.Rotate(0, input.x, 0);
        if (input.sqrMagnitude > 0.01f)
        {
            // Calculamos la rotación deseada orientando el frente del objeto hacia la dirección del movimiento.
            Quaternion rotacionObjetivo = Quaternion.LookRotation(input, Vector3.up);

            // Suavizamos la transición desde la rotación actual hacia la objetivo.
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 20.0f * Time.deltaTime);
        }
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
            ui.HideHearts(currentHp);
            StartCoroutine(GiveInvincibility());
            fuegoHp();
        }
        else {
            Debug.Log("soy inmune");
        }
       
    }
    public void fuegoHp()
    {
        if(currentHp < 4 )
        {
            Fuego1.SetActive(true);
            if (currentHp < 2)
            {
                Fuego2.SetActive(true);
            }
        }
    }
    
    public Vector3 GetDrakkarPos() 
    {
       return transform.position;
    }

    public void Shoot()
    {
        if (time > lastShoot +fireRate) 
        {
            audioSource.PlayOneShot(audioClip);
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            lastShoot = time;
        }
        
    }
    
}
