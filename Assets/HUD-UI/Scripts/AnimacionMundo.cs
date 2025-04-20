using UnityEngine;

public class AnimacionMundo : MonoBehaviour
{
    [Header("Rotación")]
    public float rotationSpeed = 30f;

    [Header("Escalado / Pulsación")]
    public float scaleSpeed = 2f; // velocidad de la pulsación
    public float scaleAmount = 0.1f; // cuánto varía el tamaño
    private Vector3 initialScale;

    void Start()
    {
        // Guardamos la escala original
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Rotación constante en el eje Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = initialScale + Vector3.one * scaleOffset;
    }
}