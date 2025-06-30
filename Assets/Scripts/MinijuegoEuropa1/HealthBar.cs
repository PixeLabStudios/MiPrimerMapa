using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    

    public void SetHealth(float max,float current)
    {
        slider.maxValue = max;
        slider.value =current;
        slider.minValue = 0;
    }

    public void SetCurrentHealth(float current)
    {
        slider.value = current;
    }
}
