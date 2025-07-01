using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerManager : MonoBehaviour
{
    public RunnerUI runnerUI;
    public GenerateObject objectGenerator;
    public RunnerScript1 runnerScript1; 
    public PanelManager panelManager;

    public void Start()
    {
        runnerUI = GetComponent<RunnerUI>();
        StartCoroutine(objectGenerator.Generate(5f)); // Inicia la generacion de objetos
        panelManager = GetComponent<PanelManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            runnerScript1.ChangeSpeed(0);
            StopAllCoroutines(); // Detiene la generacion de objetos
            runnerUI.ShowResults("Ganaste"); // Muestra el panel de resultados
            panelManager.MostrarSoloPanel("FinJuego"); // Muestra el panel de fin de juego

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
