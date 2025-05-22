using UnityEngine;

public class RunnerScript1 : MonoBehaviour
{

    #region vida
    int maxLives;
    int lives;
    #endregion
    #region Movimiento
    Vector3 desiredPosition;
    Vector3 currentPosition;
    float movementSpeed;
    float swipeThreshold;
    int currentRow;
    bool alreadyMoving;
    #endregion



    private void Start()
    {
        alreadyMoving = false;
        currentRow = 2; // inicializa en la fila 2 (en el medio), las filas irian de 1 a 4
        currentPosition = transform.position;
        desiredPosition = currentPosition;
        movementSpeed =2.0f *Time.deltaTime;
        maxLives = 3;
        lives = maxLives;
        swipeThreshold = 45f;
    }

    private void Update()
    {
        Debug.Log(currentRow);
        if (Input.touchCount ==1) 
        {
            Touch touch = Input.GetTouch(0);
           
            switch (touch.phase)
            {
                case TouchPhase.Began:
                   // Debug.Log("toco");
                    break;
                case TouchPhase.Moved:
                    if (touch.deltaPosition.y < -swipeThreshold && !alreadyMoving)
                    {
                     //   Debug.Log("mueve hacia abajo" + touch.deltaPosition.y);
                        alreadyMoving = true;
                        Move(-1);
                    } else if (touch.deltaPosition.y >=swipeThreshold && !alreadyMoving) 
                    {
                     //   Debug.Log("mueve hacia arriba" + touch.deltaPosition.y);
                        alreadyMoving = true;
                        Move(1);
                    }
                   
                    break;
                case TouchPhase.Ended:
                  //  Debug.Log("solto");
                    alreadyMoving = false;
                    break;
            }
           
            
        }
        transform.position += new Vector3(1*movementSpeed,0,0);
       
    }

    void Move(int i)
    {
        if (currentRow + i <= 4 && (currentRow +i) >=1) //Revisa que no sea menor que 1 y mayor que 4
        {
            currentRow += i;
            transform.position += new Vector3(0,2*i,0); // si el i es negativo se mueve hacia abajo y arriba si es positivo 
        }
    }
    public void ChangeSpeed(float speed)
    {
        movementSpeed = speed *Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Comida"))
        {
            Debug.Log("choque con comida");
            if (lives < maxLives)
            {
                ChangeLives(1);
            }
            Destroy(other.gameObject);
        }
        else 
        {
            
            Debug.Log("choque con otro animal");
            ChangeLives(-1);
        }
    }

    void ChangeLives(int i)
    {
       lives += i;
    }
}
