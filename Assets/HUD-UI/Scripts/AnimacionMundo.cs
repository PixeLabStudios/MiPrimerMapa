using UnityEngine;

public class AnimacionMundo : MonoBehaviour
{
    [Header("Rotaci�n")]
    public float rotationSpeed = 30f;

    [Header("Escalado / Pulsaci�n")]
    public float scaleSpeed = 2f; // velocidad de la pulsaci�n
    public float scaleAmount = 0.1f; // cu�nto var�a el tama�o
    private Vector3 initialScale;

    void Start()
    {
        // Guardamos la escala original
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Rotaci�n constante en el eje Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = initialScale + Vector3.one * scaleOffset;
    }
}