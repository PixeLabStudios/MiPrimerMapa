using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager instance;

    [Header("Panel de Victoria")]
    public GameObject victoryPanel;
    public Text timeText;
    public Text hitsText;
    public Button restartButton;

    [Header("Animales a verificar")]
    public List<AnimalAI> animals;

    private int totalHits = 0;
    private float levelStartTime;
    private int animalsArrived = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        levelStartTime = Time.time;
        victoryPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartLevel);
    }

    public void RegisterHit()
    {
        totalHits++;
    }

    public void RegisterArrival()
    {
        animalsArrived++;
        if (animalsArrived >= animals.Count)
        {
            ShowVictory();
        }
    }

    private void ShowVictory()
    {
        float totalTime = Time.time - levelStartTime;
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);

        timeText.text = $"Tiempo: {minutes:D2}:{seconds:D2}";
        hitsText.text = $"Veces embestido: {totalHits}";
        victoryPanel.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
