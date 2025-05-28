using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TMPro;

public class BoatManager : MonoBehaviour
{
    public Button buttonAccept, buttonReject, StartButton;
    public AudioClip succes, error;
    public AudioSource audioSource;
    public List<Texture2D> flagTextures;
    public List<string> countryNames;
    public List<bool> countryAllowed;

    public GameObject boatPrefab;
    public Transform spawnPoint, inspectionPoint, acceptPoint, rejectPoint;

    public float velocidadBoteinicial;
    public int puntosJugables;
    private int acumuladorAciertos;
    private int acumuladorErrores;

    private int bandera;

    private BoatController currentBoat;

    
    public TextMeshProUGUI timerText; // o public Text timerText si usás el Text normal
    public float gameDuration = 60f; // Duración total del minijuego
    private float remainingTime;
    private bool gameActive = true;

private void Start()
    {
        bandera = puntosJugables;
        remainingTime = gameDuration;
        UpdateTimerDisplay();
        StartCoroutine(GameTimer());
        SpawnBoat();
    }
    public void SpawnBoat()
    {
        GameObject newBoat = Instantiate(boatPrefab, spawnPoint.position, Quaternion.identity);
        currentBoat = newBoat.GetComponent<BoatController>();

        int index = Random.Range(0, flagTextures.Count);
        currentBoat.SetFlag(flagTextures[index], countryNames[index], countryAllowed[index]);

        // 🚫 No se destruye al ir a inspección
        currentBoat.MoveTo(inspectionPoint.position, false, velocidadBoteinicial, true); // ✅ activa botones al llegar

    }
    private IEnumerator GameTimer()
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
            UpdateTimerDisplay();
        }

        gameActive = false;
        DisableButton();
        Debug.Log("⏰ Tiempo agotado. Fin del juego.");
        EndGameDueToTime();
    }
    private void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(remainingTime);
        timerText.text = "Tiempo: " + seconds.ToString() + "s";
    }
    private void EndGameDueToTime()
    {
        Debug.Log("Aciertos: " + acumuladorAciertos);
        Debug.Log("Errores: " + acumuladorErrores);
        Debug.Log("Puntos totales: " + bandera);
        // Podés añadir aquí: desactivar la UI, mostrar un panel de fin, etc.
    }



    public void AcceptBoat()
    {
        Debug.Log("Botón Aceptar presionado");

        if (currentBoat == null)
        {
            Debug.LogError("❌ currentBoat está null. ¿Llamaste a SpawnBoat?");
            return;
        }

        DisableButton(); // ✅ los desactiva antes de moverse
        currentBoat.MoveTo(acceptPoint.position, true, velocidadBoteinicial, false);
        CheckDecision(true);
    }

    public void RejectBoat()
    {
        Debug.Log("Botón Rechazar presionado");

        DisableButton(); // ✅ los desactiva antes de moverse
        currentBoat.MoveTo(rejectPoint.position, true, velocidadBoteinicial, false);
        CheckDecision(false);
    }



    private void CheckDecision(bool accepted)
    {
        if (currentBoat.isAllowed == accepted)
        {
            acumuladorAciertos += 1;
            Debug.Log("Decisión correcta");
            velocidadBoteinicial += 1.5f;
            audioSource.PlayOneShot(succes);

        }
        else
        {
            acumuladorErrores += 1;
            Debug.Log("Decisión incorrecta");
            velocidadBoteinicial += 1.5f;
            audioSource.PlayOneShot(error);
        }
        puntosJugables -= 1;


        endGame();
    }

    public void endGame()
    {
        if (puntosJugables < 0)
        {
            
            Debug.Log("Fin del juego");
            Debug.Log("Aciertos: " + acumuladorAciertos);
            Debug.Log("Errores: " + acumuladorErrores);
            Debug.Log("Puntos totales: " + bandera);
        }
        else
        {
            Debug.Log("Juego en progreso");
            Invoke(nameof(SpawnBoat), 2f);
        }
    }
    public void DisableButton()
    {
        
        buttonAccept.interactable = false;
        buttonReject.interactable = false;
    }
    public void EnableButton()
    {
        buttonReject.interactable = true;
        buttonAccept.interactable = true;
    }
}
