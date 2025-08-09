using System;
using System.Collections.Generic;
using UnityEngine;

public class Ben4Script : Unit
{
    
    public List<GameObject> models = new();
    public Joystick joystick;
    public ShootButton attackButton;
    public GameObject panelMobile;
    public LayerMask terrainMask;
    public Device currentDevice;
    public HealthBar healthBar;
    public GameObject pibe;
    CharacterController controller;    
    GameObject currentform;
    Animator   currentAnimator;    
    MeleeAttack melee;
    PlayerMove movement;
    Rigidbody rb;
    Vector3 input;
    Action specialAction;
    float  jumpForce = 30f;
    bool isAMan =true;

    Dictionary<string, GameObject> dictionaryAnimals = new();
    private void Awake()
    {
        currentform = pibe;
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMove>();
        melee = GetComponent<MeleeAttack>();
        rb = GetComponent<Rigidbody>();
        SetStats(350, 10, 50, 2);
        healthBar.SetHealth(maxHp, hp);
    }
    void SaveModels() 
    {

        if (isAMan)
        {
            pibe.transform.GetChild(0).gameObject.SetActive(true);
            pibe.transform.GetChild(1).gameObject.SetActive(false);
            dictionaryAnimals.Add("pibe", pibe);
        }
        else 
        {   
            pibe.transform.GetChild(1).gameObject.SetActive(true);
            pibe.transform.GetChild(0).gameObject.SetActive(false);
            dictionaryAnimals.Add("pibe", pibe);
        }
        foreach (GameObject model in models) 
        {
            if (model != null)
            {
                dictionaryAnimals.Add(model.name, model);
                model.SetActive(false);
            }
            else
            {
                Debug.LogError("Model is null: " + model.name);
            }
        }
    }
    void Start()
    {
        switch (currentDevice)
        {
            case Device.PC:
                panelMobile.SetActive(false);
                break;
            case Device.Mobile:
                panelMobile.SetActive(true);
                break;
        }
        SaveModels();
        Transform("pibe");
    }
    
    
    public enum Device
    {
        PC,
        Mobile
    }
    public override void TakeDamage(int value)
    {
        healthBar.SetCurrentHealth(hp - value);
        base.TakeDamage(value);
    }



    public override void Attack()
    {
        if (Time.time >= melee.nextAttack)
        {
            melee.Attack(damage);
           // anim.SetTrigger("attack");
            melee.nextAttack = Time.time + 1f / attackRate;
        }
    }

    public override void OnDeath()
    {
        //muere
    }
    void Update()
    {
        
        
        switch (currentDevice)
        {
            case Device.Mobile:


                input.x = joystick.Horizontal;
                input.y = joystick.Vertical;
                input.z = 0;
                if (input.y == 0 && input.x == 0)
                {
                //    currentAnimator.SetBool("moving", false);
                }
                else
                {
                //    currentAnimator.SetBool("moving", true);
                }
                movement.MoveRB(moveSpeed, input, false);
                if (attackButton.buttonPressed)
                {
                    Attack();
                }

               
                break;
            case Device.PC:

                input.x = Input.GetAxis("Horizontal");
                input.y = Input.GetAxis("Vertical");
                input.z = 0;
                if (input.y == 0 && input.x == 0)
                {
              //      currentAnimator.SetBool("moving", false);
                }
                else
                {
              //      currentAnimator.SetBool("moving", true);
                }
                movement.MoveRB(moveSpeed, input, false);
                if (Input.GetMouseButtonDown(0))
                {
                    Attack();

                }
                if (Input.GetMouseButtonDown(1))
                {
                   Jump();
                }
                break;
        }
    }
    void Transform(string key)
    {
        if (dictionaryAnimals.ContainsKey(key))
        {
            currentform.SetActive(false) ;
            currentform = dictionaryAnimals[key];
            currentAnimator = currentform.GetComponent<Animator>();
           // currentAnimator.SetBool("moving", false);
            currentform.SetActive(true);

        }
    }
    void Jump() 
    {
        if (IsGrounded()) 
        {
             Debug.Log("Salte");
             rb.AddForce(Vector3.up * 30f, ForceMode.Impulse);
            
        }
    }
    bool IsGrounded()
    {
        Physics.Raycast(transform.position + controller.center, Vector3.down, out RaycastHit hit, 5,terrainMask);
        if(hit.collider != null)
        {
         return true;
        }
        return false;
    }
    public void ChangeSpeed(int newSpeed)
    {
        moveSpeed += newSpeed;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Terrain"))
        {
            // Handle collision with terrain
            Debug.Log("Collided with terrain");
        }
    }
}
