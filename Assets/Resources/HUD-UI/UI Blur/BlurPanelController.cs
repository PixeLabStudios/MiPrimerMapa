using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlurFadeController : MonoBehaviour
{
    [Header("Configuración")]
    public RawImage blurImage;           // Asigna aquí el RawImage que contiene el blur
    public float fadeSpeed = 1.5f;         // Velocidad del fade (cuanto mayor, más rápido)

    private Coroutine currentRoutine;

    void Awake()
    {
        if (blurImage != null)
        {
            Color c = blurImage.color;
            c.a = 0f;
            blurImage.color = c;
            blurImage.gameObject.SetActive(false);
        }
    }

    public void FadeIn()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        blurImage.gameObject.SetActive(true);
        currentRoutine = StartCoroutine(FadeToAlpha(1f));
    }

    public void FadeOut()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(FadeToAlpha(0f, () => blurImage.gameObject.SetActive(false)));
    }

    private IEnumerator FadeToAlpha(float targetAlpha, System.Action onComplete = null)
    {
        Color c = blurImage.color;
        while (!Mathf.Approximately(c.a, targetAlpha))
        {
            c.a = Mathf.MoveTowards(c.a, targetAlpha, Time.deltaTime * fadeSpeed);
            blurImage.color = c;
            yield return null;
        }

        c.a = targetAlpha;
        blurImage.color = c;
        onComplete?.Invoke();
    }
}
