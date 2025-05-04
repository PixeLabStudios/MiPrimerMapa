using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalCorrectLabels = 0;
    public int requiredCorrectLabels = 6;
    public int errorCount = 0;

    public GameObject[] starImages; // Arreglo de estrellas para mostrar al final
    public GameObject victoryPanel;

    public void RegisterCorrectDrop()
    {
        totalCorrectLabels++;
        CheckVictory();
    }

    public void RegisterError()
    {
        errorCount++;
    }

    void CheckVictory()
    {
        if (totalCorrectLabels >= requiredCorrectLabels)
        {
            victoryPanel.SetActive(true);
            ShowStars();
        }
    }

    void ShowStars()
    {
        int stars = 1;
        if (errorCount < 3) stars = 3;
        else if (errorCount < 6) stars = 2;

        for (int i = 0; i < stars; i++)
        {
            starImages[i].SetActive(true);
        }
    }
}
