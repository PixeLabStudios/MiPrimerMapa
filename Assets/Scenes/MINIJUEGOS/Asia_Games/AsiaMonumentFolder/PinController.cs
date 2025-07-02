using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PinController : MonoBehaviour
{
    public string pinID;
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button chooseButton;

    private AsiaMonumentManager gameManager;
    private UIManager uiManager;

    public void Init(AsiaMonumentManager gm, UIManager ui)
    {
        gameManager = gm;
        uiManager = ui;
        chooseButton.onClick.AddListener(OnChoose);
        HidePanel();
    }

    public void OnClickPin()
    {
        //UIManager.Instance.HideAllPinPanels();
        uiManager.HideAllPinPanels();
        ShowPanel();
    }

    public void OnChoose()
    {
        gameManager.EvaluateChoice(pinID);
        HidePanel();
    }

    public void ShowPanel()
    {
        infoPanel.SetActive(true);
    }

    public void HidePanel()
    {
        infoPanel.SetActive(false);
    }

    public void DisablePin()
    {
        GetComponent<Button>().interactable = false;
    }
}
