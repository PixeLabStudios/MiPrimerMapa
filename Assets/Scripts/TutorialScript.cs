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
    void Start()
    {
        panelImage = panel.GetComponent<Image>();
        panelColor = panelImage.color;
        currentAlpha = panelImage.color.a;
        desiredAlpha = currentAlpha;
    }

    // Update is called once per frame
    void Update()
    {
        currentAlpha = Mathf.Lerp(currentAlpha, desiredAlpha, Time.deltaTime * 1.7f);
        panelColor.a = currentAlpha;
        panelImage.color = panelColor;

    }
    IEnumerator FadeIn()
    {
        desiredAlpha = 1f;
        yield return new WaitForSeconds(1f);
        desiredAlpha = 0f;
    }
   
}
