using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MemoTestUIManager : MonoBehaviour
{
    public Button StartButton;
    public GameObject errorsText;
    public GameObject CorrectText;
   // public GameObject ScoreText;
    public PlayerData data;
    TextMeshProUGUI errors;
    TextMeshProUGUI correct;

    void Start()
    {
      //  ScoreText.GetComponent<TextMeshProUGUI>().text = data.Score.ToString();
        errors = errorsText.GetComponent<TextMeshProUGUI>();
        correct = CorrectText.GetComponent<TextMeshProUGUI>();
        
    }

    public void ChangeCorrectText(int i) => correct.text = (i.ToString());
    public void ChangeErrorsText(int i) => errors.text = (i.ToString());

    public void DisableStartButton()
    {
        StartButton.interactable = false;
    }
    public void EnableStartButton() 
    {
        StartButton.interactable = true;
    }
    public void LeaveScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
