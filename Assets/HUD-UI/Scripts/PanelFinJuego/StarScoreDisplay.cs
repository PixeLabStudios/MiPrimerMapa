using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarScoreDisplay : MonoBehaviour
{
    public Image[] stars; // Estrellas con tipo Filled
    public float fillSpeed = 1.5f; // Velocidad de llenado
    public AudioClip fillSound; // Asigna el sonido en el inspector
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void ShowStars(float scorePercent)
    {
        int totalHalfStars = Mathf.RoundToInt(scorePercent / 20f); // 0 a 6 medias estrellas
        StartCoroutine(AnimateStars(totalHalfStars));
    }

    IEnumerator AnimateStars(int halfStars)
    {
        for (int i = 0; i < stars.Length; i++)
        {

            Image star = stars[i];
            star.fillAmount = 0;

            int starValue = Mathf.Clamp(halfStars - i * 2, 0, 2); // 0 = vacía, 1 = media, 2 = llena

            float targetFill = starValue * 0.5f;

            while (star.fillAmount < targetFill)
            {
                star.fillAmount += Time.deltaTime * fillSpeed;
                star.fillAmount = Mathf.Min(star.fillAmount, targetFill);
                yield return null;
            }
            if (targetFill > 0f && fillSound != null && audioSource != null)
                audioSource.PlayOneShot(fillSound);

            yield return new WaitForSeconds(0.2f); // pequeño delay entre estrellas
        }
    }
}
