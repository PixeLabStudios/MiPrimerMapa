using UnityEngine;

public class SubmarineGameManager : MonoBehaviour
{
    public GenerateObject generateObjects; // Referencia al script GenerateObjects
    public StarScoreDisplay starScoreDisplay; // Referencia al script StarScoreDisplay
    public GameObject GameoverPanel;
    public int goal; //cuantos punto debe llegar para ganar el juego
    public int successes; // cuantos peces llegaron a la meta.
    public int errors; //cuenta cuantos disparos a peces y cuantos subs pasaron
    int points;
    bool canShoot;
    bool gameOver = false;
    //0-1 errores -> 3 estrellas
    //Cada 4 errores pierde una estrella
    // a los 12 errores no tiene estrellas
    void Start()
    {
        canShoot = true;
        gameOver = false;
        goal = 15;
        points = 120; 
        successes = 0;
        errors = 0;
        StartCoroutine(generateObjects.Generate(5f)); // Inicia la generación de objetos
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameOver) 
        {
            if (other.CompareTag("Animal"))
            {
                successes++;
                if (IsGameOver())
                {
                    EndGame(); // Llama a la función de fin de juego si se cumplen las condiciones           
                }

            }
            if (other.CompareTag("Submarino"))
            {
                errors++;
            }
            Destroy(other.gameObject);
        }
        
    }
    int CalculatePoints()
    {

        int result = points - (errors * 10);
        return Mathf.Clamp(result, 0, result);
    }
    public bool IsGameOver()
    {
        return successes >= goal;
    }

    void EndGame()
    {
        canShoot = false;
        gameOver = true; 
        Debug.Log("Game Over!");
        StopAllCoroutines(); // Detiene la generación de objetos
        GameoverPanel.SetActive(true);
        starScoreDisplay.ShowStars(CalculatePoints());
    }
    public bool GetCanShoot()
    {
        return canShoot;
    }

}
