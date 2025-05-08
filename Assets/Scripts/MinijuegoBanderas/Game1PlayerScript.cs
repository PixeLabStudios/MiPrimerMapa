using System.Collections;
using UnityEngine;

public class Game1PlayerScript : MonoBehaviour
{
    Game1MoveCharacter moveCharacter;
    bool hasAFlag;
    bool canMove;
    GameObject flagAttached;
    Game1Manager manager;
    private void Awake()
    {
        moveCharacter = GetComponent<Game1MoveCharacter>();
        manager = FindFirstObjectByType<Game1Manager>();
        
    }
    void Start()
    {
        hasAFlag = false;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) 
        {
            moveCharacter.ControlMovement();
        }                    
    }

    public void AttachFlag(GameObject flag) 
    {
        flag.transform.SetParent(transform);
        flag.transform.localPosition = Vector3.zero +Vector3.up * 10;
        flagAttached = flag;
        hasAFlag = true;
    }
    void DettachFlag() 
    {
        flagAttached.transform.SetParent(null);
        flagAttached.GetComponent<Flag>().ReturnToPosition();   
        flagAttached =null;
    }
    IEnumerator StunCharacter(float duration) 
    {
        canMove = false;
        moveCharacter.StopCharacter();
        moveCharacter.currentState = Game1MoveCharacter.State.stunned;
        yield return new WaitForSeconds(duration);
        moveCharacter.currentState = Game1MoveCharacter.State.idle;
        canMove = true;
    }
    private void OnTriggerEnter(Collider other)
    {

        switch(other.tag)
        {
            case "bandera":
                if (!hasAFlag)
                {
                    Debug.Log("Agarre a una bandera");
                    AttachFlag(other.gameObject);
                    hasAFlag = true;
                }
                else 
                {
                    Debug.Log("Ya tengo una bandera");
                }
            break;
            case "Base":
                Debug.Log("Entre a una base");
                if (hasAFlag)
                {
                    if(manager.CheckFlag(flagAttached.GetComponent<Flag>(), other.GetComponent<Base>())) 
                    {
                        Destroy(flagAttached);
                        flagAttached = null;
                        hasAFlag = false;
                        //revisa si es correcta
                    }

                }

            break;
            case "Animal":
                if (hasAFlag)
                {              
                    DettachFlag();
                    hasAFlag = false;
                }
                StartCoroutine(StunCharacter(2f));
                break;
        }
        

    }
}
