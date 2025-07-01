using System.Runtime.Serialization;
using UnityEngine;

public class RunnerScript1 : MonoBehaviour
{

    #region vida
    int maxLives;
    int lives;
    RunnerUI runnerUI;
    #endregion
    #region Movimiento
    Vector3 desiredPosition;
    Vector3 currentPosition;
    public float movementSpeed;
    float swipeThreshold;
    int currentRow;
    bool alreadyMoving;
    #endregion
    public PanelManager panelManager;



    private void Start()
    {
        runnerUI = FindFirstObjectByType<RunnerUI>();
        alreadyMoving = false;
        currentRow = 2; 
        currentPosition = transform.position;
        desiredPosition = currentPosition;
        movementSpeed =5f;
        maxLives = 3;
        lives = maxLives;
        swipeThreshold = 45f;
        //panelManager = GetComponent<PanelManager>();
        
    }

    private void Update()
    {
        InputMobile();
        
        transform.position += new Vector3(1*movementSpeed *Time.deltaTime,0,0);
       
    }
    /// <summary>
    /// Controla los inputs en moviles
    /// </summary>
    void InputMobile() 
    {
        if (Input.touchCount == 1)
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
                    }
                    else if (touch.deltaPosition.y >= swipeThreshold && !alreadyMoving)
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
    }
    /// <summary>
    /// Mueve al jugador a la fila de arriba o abajo dependiendo del valor de i
    /// </summary>
    /// <param name="i"></param>
    void Move(int i)
    {
        if (currentRow + i <= 3 && (currentRow +i) >=1) //Revisa que no sea menor que 1 y mayor que 3
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
            // Debug.Log("choque con comida");
            if (lives < maxLives)
            {
                ChangeLives(1);
                runnerUI.ShowHeart(lives);
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Animal") && lives > 0)
        {
            ChangeLives(-1);
            runnerUI.HideHearts(lives);
            Debug.Log("choque con otro animal  Vidas: " + lives);
            if (lives <= 0)
            {
                RunnerManager runnerManager = FindFirstObjectByType<RunnerManager>();
                runnerManager.StopAllCoroutines();
                //runnerManager.runnerUI.ShowResults("Perdiste");
                ChangeSpeed(0);
                panelManager.MostrarSoloPanel("FinJuego");
                // Debug.Log("perdiste todas las vidas");
                //GameOver
                //Destroy(gameObject);
                //PauseGame();
                Destroy(other.gameObject);
            }

        }

    }

    void ChangeLives(int i)
    {
       lives += i;
    }
    
}
