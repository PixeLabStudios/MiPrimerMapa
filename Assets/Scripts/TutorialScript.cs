using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public GameObject panel;
    private Image panelImage;
    private Color panelColor;
    float currentAlpha;
    public float desiredAlpha;
    bool completedPart;
    public Button marketButton;
    void Start()
    {
        panelImage = panel.GetComponent<Image>();
        panelColor = panelImage.color;
        currentAlpha = panelImage.color.a;
        desiredAlpha = currentAlpha;
        completedPart = false;
    }

    // Update is called once per frame
    void Update()
    {
        marketButton.transform.SetAsFirstSibling();
        currentAlpha = Mathf.Lerp(currentAlpha, desiredAlpha, Time.deltaTime * 1.7f);
        panelColor.a = currentAlpha;
        panelImage.color = panelColor;
        
    }
    IEnumerator FadeIn()
    {
        desiredAlpha = 1f;
       
        yield return new WaitUntil(IsCompleted);
        desiredAlpha = 0f;
    }
    public bool IsCompleted()
    {
        return completedPart;
    }

}
