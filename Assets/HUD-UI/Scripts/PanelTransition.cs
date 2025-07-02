using System.Collections;
using UnityEngine;

public class PanelTransition : MonoBehaviour
{
    public float duration = 0.5f;

    public IEnumerator FadeOut(GameObject panel)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        if (cg == null) cg = panel.AddComponent<CanvasGroup>();
        float t = 0;
        while (t < duration)    
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1, 0, t / duration);
            yield return null;
        }
        cg.alpha = 0;
        panel.SetActive(false);
    }

    public IEnumerator FadeIn(GameObject panel)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        if (cg == null) cg = panel.AddComponent<CanvasGroup>();
        panel.SetActive(true);
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0, 1, t / duration);
            yield return null;
        }
        cg.alpha = 1;
    }
}
