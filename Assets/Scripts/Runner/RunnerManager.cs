using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerManager : BaseGameManager
{
    public RunnerUI runnerUI;
    public GenerateObject objectGenerator;
    public RunnerScript1 runnerScript1; 
    
    public void Start()
    {
        runnerUI = GetComponent<RunnerUI>();
        StartCoroutine(objectGenerator.Generate(5f)); // Inicia la generacion de objetos
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            runnerScript1.ChangeSpeed(0);
            StopAllCoroutines(); // Detiene la generacion de objetos
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
  
}
