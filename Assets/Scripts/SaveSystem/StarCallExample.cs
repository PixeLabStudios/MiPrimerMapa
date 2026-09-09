using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarCallExample : MonoBehaviour
{
    public string boardId;
    public string miniGameId;
    //public int stars;
    public int starsAsPoints;
    public TextMeshProUGUI starsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        starsText.text = starsAsPoints.ToString();

    }
    public void onclickbuttoin()
    {
        GameProgressManager.Instance.UpdateMiniGame(boardId, miniGameId, starsAsPoints, true);
    }
}
