using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour
{
    [Header("Bandera")]
    public Renderer flagRenderer;        // El Renderer del objeto donde se muestra la bandera
    public Material flagMaterial;        // El material editable (copiado) para aplicar la bandera
    public string countryName;           // Nombre del país (opcional, por lógica de juego)
    public bool isAllowed;               // Si está permitido el ingreso según la bandera

    private Coroutine movementCoroutine; // Referencia al movimiento para evitar múltiples al mismo tiempo
    // Asigna la textura, país y permiso al barco
    public void SetFlag(Texture2D flagTexture, string country, bool allowed)
    {
        if (flagMaterial == null || flagRenderer == null)
        {
            Debug.LogError("🚨 Falta asignar flagMaterial o flagRenderer en BoatController.");
            return;
        }

        Material newMaterial = new Material(flagMaterial); // Clonar material base
        newMaterial.mainTexture = flagTexture;
        flagRenderer.material = newMaterial;

        countryName = country;
        isAllowed = allowed;
    }

    // Mueve el barco hacia un destino dado
    public void MoveTo(Vector3 destination, bool destroyOnArrival = false,float speed = 5)
    {
        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MoveToPosition(destination, destroyOnArrival,speed));
    }

    // Corrutina para mover el barco suavemente
    private IEnumerator MoveToPosition(Vector3 target, bool destroyOnArrival,float speed)
    {
        float rotationSpeed = 5f;

        while (true)
        {
            Vector3 direction = target - transform.position;
            float distance = direction.magnitude;

            if (distance < 0.50f)
            {
                if (destroyOnArrival)
                {
                    Destroy(gameObject); // 💥 Solo se destruye si está permitido
                }
                yield break;
            }

            direction.Normalize();

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

            yield return null;
        }
    }


}

