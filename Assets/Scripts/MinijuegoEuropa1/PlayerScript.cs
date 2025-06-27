using UnityEngine;

public class PlayerScript : Unit
{
    Joystick joystick;
    PlayerMove movement;
    MeleeAttack melee;
    Vector3 input;
    public enum Device 
    {
        PC,Mobile
    }
    public Device currentDevice;
    private void Awake()
    {
       
        movement = GetComponent<PlayerMove>();
        melee = GetComponent<MeleeAttack>();
    }
    private void Start()
    {
        currentDevice = Device.PC;
        SetStats(350, 10, 50, 2);
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
                    movement.Move(moveSpeed, input);
               
               
                break;
            case Device.PC:
                
                input.x = Input.GetAxis("Horizontal");
                input.y = Input.GetAxis("Vertical");
                input.z = 0;

                movement.Move(moveSpeed, input);
                if (Input.GetMouseButtonDown(0)) 
                {
                    Attack();
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
            TakeDamage(script.damage); 
        }
    }





    public override void OnDeath()
    {
        Debug.Log("me mori pipipi");
    }
}
