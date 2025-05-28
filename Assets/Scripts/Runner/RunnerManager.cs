using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerManager : MonoBehaviour
{
    public RunnerUI runnerUI;
    public GenerateObject objectGenerator;
    public RunnerScript1 runnerScript1; 
    
    public void Start()
    {
        runnerUI = GetComponent<RunnerUI>();
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            runnerScript1.ChangeSpeed(0);
            StopCoroutine(objectGenerator.Generate()); // Detiene la generacion de objetos
            runnerUI.ShowResults("Ganaste"); // Muestra el panel de resultados
            
            //Despues deberia mostrar el puntaje
        }
    }
    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    public void ExitGame() 
    {
        SceneManager.LoadScene("Antartida");
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
