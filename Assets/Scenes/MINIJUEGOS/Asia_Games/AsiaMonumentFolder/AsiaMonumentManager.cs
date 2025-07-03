using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class AsiaMonumentManager : MonoBehaviour
{
    //public List<MonumentData> monumentList;
    public List<PlaceData> placeList;
    //private List<MonumentData> availableMonuments;
    private List<PlaceData> availableplaces;

    //public Image monumentImageUI;
    public TextMeshProUGUI monumentNameUI;

    public StarScoreDisplay scoreDisplay;
    public PanelManager panelManager;

    public VictoryPanel victoryPanel;
    public Transform pinParent;

    public UIManager uiManager;

    //private MonumentData currentMonument;
    private PlaceData currentPlace;
    private int correctFirstTry = 0;
    private int totalErrors = 0;
    private bool answeredCorrectly = false;

    private Dictionary<string, PinController> pins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        availableplaces = new List<PlaceData>(placeList);
        pins = new Dictionary<string, PinController>();

        foreach (Transform child in pinParent)
        {
            PinController pc = child.GetComponent<PinController>();
            pins.Add(pc.pinID, pc);
            pc.Init(this, uiManager);
        }

        NextMonument();
    }

    public void NextMonument()
    {
        if (availableplaces.Count == 0)
        {
            //ShowVictoryPanel();
            //panelManager.MostrarSoloPanel("PanelFinJuego");
            sendResult();
            return;
        }

        int index = Random.Range(0, availableplaces.Count);
        currentPlace = availableplaces[index];
        availableplaces.RemoveAt(index);

        //monumentImageUI.sprite = currentMonument.image;
        monumentNameUI.text = currentPlace.name;
        answeredCorrectly = false;
    }

    public void EvaluateChoice(string selectedPinID)
    {
        if (selectedPinID == currentPlace.correctPinID)
        {
            if (!answeredCorrectly)
            {
                correctFirstTry++;
                answeredCorrectly = true;
            }

            pins[selectedPinID].DisablePin();
            NextMonument();
        }
        else
        {
            totalErrors++;
            //uiManager.ShowError("¡Incorrecto! Intenta de nuevo.");
            
            //UIManager.Instance.ShowError("¡Incorrecto! Intenta de nuevo.");

        }
    }
    public void sendResult()
    {
        panelManager.MostrarSoloPanel("PanelFinJuego");

        if (totalErrors >= 10)
        {
            scoreDisplay.ShowStars(0);

        }
        else if ( totalErrors == 0) {

            scoreDisplay.ShowStars(100*10f);
        }
        else
        {

        }
        {
            scoreDisplay.ShowStars(totalErrors * 10f);
        }
    }

    void ShowVictoryPanel()
    {
        victoryPanel.Show(correctFirstTry, totalErrors);

    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
