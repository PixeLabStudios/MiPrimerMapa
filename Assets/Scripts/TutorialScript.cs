using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
   public GameObject panel;
   private Image panelImage;
   float alpha;
    void Start()
    {
      panelImage = panel.GetComponent<Image>();
        alpha = panelImage.color.a;  
    }

    // Update is called once per frame
    void Update()
    {
       // panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, Mathf.PingPong(Time.time, 1)); 
    }
}
