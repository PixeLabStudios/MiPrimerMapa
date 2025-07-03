using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryPanel : MonoBehaviour
{
    public TextMeshProUGUI successText;
    public TextMeshProUGUI errorText;
    public GameObject panel;
    //public Button restartButton;
    private AsiaMonumentManager gameManager;

    public void Init(AsiaMonumentManager gm) {
        gameManager = gm;
        //restartButton.onClick.AddListener(() => gameManager.RestartGame());
        panel.SetActive(false);
    }

    public void Show(int correctFirstTry, int totalErrors) {
        panel.SetActive(true);
        successText.text = "Aciertos a la primera: " + correctFirstTry;
        errorText.text = "Errores: " + totalErrors;
    }
}
