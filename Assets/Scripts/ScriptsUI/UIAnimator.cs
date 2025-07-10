using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    public static IEnumerator ScaleBounce(Transform target, Vector3 targetScale, float duration)
    {
        Vector3 original = target.localScale;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            float overshoot = 1.70158f;
            t = 1f - Mathf.Pow(1f - t, 2) * ((overshoot + 1f) * (1f - t) - overshoot);
            target.localScale = Vector3.LerpUnclamped(original, targetScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        target.localScale = targetScale;
    }

    public static IEnumerator Fade(CanvasGroup cg, float from, float to, float duration)
    {
        float time = 0f;
        cg.alpha = from;

        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(from, to, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        cg.alpha = to;
    }

    public static IEnumerator Flash(Image image, Color flashColor, float flashDuration)
    {
        Color original = image.color;
        float t = 0f;

        while (t < flashDuration)
        {
            image.color = Color.Lerp(original, flashColor, Mathf.PingPong(t * 4f, 1f));
            t += Time.deltaTime;
            yield return null;
        }

        image.color = original;
    }
}
