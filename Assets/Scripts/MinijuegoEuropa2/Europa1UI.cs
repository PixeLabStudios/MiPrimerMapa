using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Europa1UI : MonoBehaviour
{
    public GameObject[] hearts;
    public GameObject hpBar;
    public Slider slider;
    Europe2Manager manager;
    public GameObject gameOverPanel;
    private void Awake()
    {
        manager = FindFirstObjectByType<Europe2Manager>();
    }
    private void Start()
    {
        gameOverPanel.SetActive(false);
        ShowAllLives();
        hpBar.SetActive(false);

    }
    public void SetHp()
    {
        slider.maxValue = manager.bossScript.MaxHp;
        slider.value = manager.bossScript.hp;
        
    }
    public void ShowAllLives()
    {

        foreach (GameObject go in hearts)
        {
            go.SetActive(true);
        }
    }
    public void HideHearts(int i)
    {
        if (i <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        hearts[i].SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
