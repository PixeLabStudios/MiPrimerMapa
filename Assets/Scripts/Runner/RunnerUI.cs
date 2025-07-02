using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunnerUI : MonoBehaviour
{
    RunnerManager runnerManager;
    //public Button pauseButton;
    //public Button piramideButton;
    //public GameObject panel;
    //public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public GameObject[] Hearts;
    private void Start()
    {
        runnerManager = FindFirstObjectByType<RunnerManager>();
        //panel.SetActive(false);
        //resultPanel.SetActive(false);
    }
    public void ShowFoodChainPanel() 
    {

        //panel.SetActive(true);
        //piramideButton.interactable = false;
        runnerManager.PauseGame();
        Debug.Log("ShowFoodChainPanel");
    }
    public void HideFoodChainPanel() 
    {
        //panel.SetActive(false);
        //piramideButton.interactable = true;
        runnerManager.ResumeGame();
        
    }
    public void ShowResults(string result) 
    {
        //resultPanel.SetActive(true);
        resultText.text = result;

    }
    public void ShowAllLives()
    {

        foreach (GameObject go in Hearts) {
        go.SetActive(true);
        }
    }
    public void HideHearts(int i) 
    {
        Hearts[i].SetActive(false);
    }
    public void ShowHeart(int i) 
    {
        Hearts[i-1].SetActive(true);
    }
}
