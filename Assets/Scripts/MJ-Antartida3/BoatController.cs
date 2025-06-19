using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour
{
    [Header("Bandera")]
    public Renderer flagRenderer;
    public Material flagMaterial;
    public string countryName;
    public bool isAllowed;

    private BoatManager boatManager;
    private Coroutine movementCoroutine;

    private void Start()
    {
        boatManager = FindFirstObjectByType<BoatManager>(FindObjectsInactive.Include);
        if (boatManager == null)
        {
            Debug.LogError("🚨 No se encontró BoatManager en la escena.");
        }
    }

    public void SetFlag(Texture2D flagTexture, string country, bool allowed)
    {
        if (flagMaterial == null || flagRenderer == null)
        {
            Debug.LogError("🚨 Falta asignar flagMaterial o flagRenderer en BoatController.");
            return;
        }

        Material newMaterial = new Material(flagMaterial);
        newMaterial.mainTexture = flagTexture;
        flagRenderer.material = newMaterial;

        countryName = country;
        isAllowed = allowed;
    }

    public void MoveTo(Vector3 destination, bool destroyOnArrival = false, float speed = 5f, bool activateButtonsOnArrival = false)
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }

        movementCoroutine = StartCoroutine(MoveToPosition(destination, destroyOnArrival, speed, activateButtonsOnArrival));
    }

    private IEnumerator MoveToPosition(Vector3 target, bool destroyOnArrival, float speed, bool activateButtonsOnArrival)
    {
        float rotationSpeed = 5f;

        while (true)
        {
            Vector3 direction = target - transform.position;
            float distance = direction.magnitude;

            if (distance < 0.50f)
            {
                if (activateButtonsOnArrival && boatManager != null)
                {
                    boatManager.EnableButton(); // ✅ Solo activa los botones si se indicó
                }

                if (destroyOnArrival)
                {
                    Destroy(gameObject);
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


