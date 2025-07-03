using System.Security.Cryptography;
using UnityEngine;

public class PlayerScript : Unit
{
    public ShootButton attackButton;
    public ShootButton eagleAttackButton;
    public Joystick joystick;
    public GameObject panelMobile;
    PlayerMove movement;
    MeleeAttack melee;
    EagleScript eagle;
    Vector3 input;
    public HealthBar healthBar;
    public enum Device 
    {
        PC,Mobile
    }
    public Device currentDevice;
    private void Awake()
    {
        eagle = FindFirstObjectByType<EagleScript>();
        movement = GetComponent<PlayerMove>();
        melee = GetComponent<MeleeAttack>();
        SetStats(350, 10, 50, 2);
        healthBar.SetHealth(maxHp, hp);
    }
    private void Start()
    {
        // currentDevice = Device.PC;
        switch (currentDevice) 
        {
            case Device.PC:
                panelMobile.SetActive(false);
                break;
            case Device.Mobile:
                panelMobile.SetActive(true);
                break;
        }
        melee.damage = damage;
        melee.range = 3;
        melee.rate = attackRate;

    }
    private void Update()
    {
        
        
        switch (currentDevice) 
        {
            case Device.Mobile:
                
                
                    input.x = joystick.Horizontal;
                    input.y = joystick.Vertical;
                    input.z = 0;
                    movement.Move(moveSpeed, input,false);
                if (attackButton.buttonPressed) 
                { 
                    Attack();
                }
                
                if (eagleAttackButton.buttonPressed) 
                {
                    eagle.Attack();
                }   
                break;
            case Device.PC:
                
                input.x = Input.GetAxis("Horizontal");
                input.y = Input.GetAxis("Vertical");
                input.z = 0;

                movement.Move(moveSpeed, input,true);
                if (Input.GetMouseButtonDown(0)) 
                {
                    Attack();
                }
                if (Input.GetMouseButtonDown(1)) 
                {
                    eagle.Attack();
                }
                break;
        }
        
    }
    public override void Attack()
    {
        if (Time.time >= melee.nextAttack) 
        {
            melee.Attack(damage);
           
            melee.nextAttack = Time.time + 1f/attackRate;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.TryGetComponent<BulletMove>(out var script)) 
        {
            Debug.Log("choque con bala");
            Debug.Log(script.damage);
            TakeDamage(script.damage); 
            Destroy(other.gameObject);
        }
    }

    public override void TakeDamage(int damage)
    {
        healthBar.SetCurrentHealth(hp - damage);
        base.TakeDamage(damage);
    }



    public override void OnDeath()
    {
        Debug.Log("me mori pipipi");
        //logica de muerte y panel que llama al star
    }
}
