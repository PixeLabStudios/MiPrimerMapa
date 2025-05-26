using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public RunnerScript1 runnerScript1; 
    float time;
    bool gameOver;
    public void Start()
    {
        gameOver = false;
        time = 0;
    }
    
    private void Update()
    {
       
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            runnerScript1.ChangeSpeed(0);
            Debug.Log("gano");
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pausa el juego
        // StopCoroutine(GenerateObject.Generate());
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el juego
        // StartCoroutine(GenerateObject.Generate());
    }
}
