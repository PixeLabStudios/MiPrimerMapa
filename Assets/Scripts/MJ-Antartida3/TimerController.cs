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
    private bool timerPaused = false;
    private Coroutine timerCoroutine;

    public delegate void TimerEnded();
    public event TimerEnded OnTimerEnd;

    public void StartTimer()
    {
        remainingTime = gameDuration;
        timerRunning = true;
        timerPaused = false;

        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    public void PauseTimer()
    {
        timerPaused = true;
    }

    public void ResumeTimer()
    {
        if (timerRunning && timerPaused)
        {
            timerPaused = false;
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingTime > 0f)
        {
            if (!timerPaused)
            {
                remainingTime -= Time.deltaTime;
                UpdateVisuals();
            }
            yield return null;
        }

        remainingTime = 0f;
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
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();
    }

    public bool IsRunning() => timerRunning && !timerPaused;
    public bool IsPaused() => timerPaused;
}
