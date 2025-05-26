using UnityEngine;
using UnityEngine.UI;

public class RunnerUI : MonoBehaviour
{
    RunnerManager runnerManager;
    public Button pauseButton;
    public Button piramideButton;
    public GameObject panel;


    private void Start()
    {
        runnerManager = FindFirstObjectByType<RunnerManager>();
        panel.SetActive(false);
    }
    public void ShowFoodChainPanel() 
    {

        panel.SetActive(true);
        piramideButton.interactable = false;
        runnerManager.PauseGame();
        Debug.Log("ShowFoodChainPanel");
    }
    public void HideFoodChainPanel() 
    {
        panel.SetActive(false);
        piramideButton.interactable = true;
        runnerManager.ResumeGame();
        Debug.Log("HideFoodChainPanel");
    }

}
