using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TimerController : MonoBehaviour
{
    public float gameDuration = 60f;
    public Image timerFillImage;
    public TextMeshProUGUI timerText;

    private float remainingTime;
    private bool timerRunning = false;

    public delegate void TimerEnded();
    public event TimerEnded OnTimerEnd;

    public void StartTimer()
    {
        remainingTime = gameDuration;
        timerRunning = true;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
            UpdateVisuals();
        }
        timerRunning = false;
        UpdateVisuals();
        OnTimerEnd?.Invoke();
    }

    private void UpdateVisuals()
    {
        float fillAmount = remainingTime / gameDuration;
        if (timerFillImage != null)
            timerFillImage.fillAmount = fillAmount;

        if (timerText != null)
            timerText.text = "Tiempo: " + Mathf.CeilToInt(remainingTime).ToString() + "s";
    }

    public bool IsRunning() => timerRunning;
}

