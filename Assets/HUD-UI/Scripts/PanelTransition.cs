using System.Collections;
using UnityEngine;

public class PanelTransition : MonoBehaviour
{
    public float duration = 0.5f;

    public IEnumerator FadeOut(GameObject panel)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        if (cg == null) cg = panel.AddComponent<CanvasGroup>();
        yield return UIAnimator.Fade(cg, 1f, 0f, duration);
        panel.SetActive(false);
    }

    public IEnumerator FadeIn(GameObject panel)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        if (cg == null) cg = panel.AddComponent<CanvasGroup>();
        panel.SetActive(true);
        yield return UIAnimator.Fade(cg, 0f, 1f, duration);
    }
}
