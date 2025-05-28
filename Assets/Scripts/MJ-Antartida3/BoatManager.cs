using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private int puntosJugables;

    private int acumuladorAciertos;
    private int acumuladorErrores;

    public TimerController timerController;

    private BoatController currentBoat;
    private bool gameActive = false;

    private void Start()
    {
        timerController.OnTimerEnd += EndGameDueToTime;
        timerController.StartTimer();
        gameActive = true;
        DisableButton();
        SpawnBoat();

    }

    public void SpawnBoat()
    {
        //|| puntosJugables < 0
        if (!gameActive ) return;

        GameObject newBoat = Instantiate(boatPrefab, spawnPoint.position, Quaternion.identity);
        currentBoat = newBoat.GetComponent<BoatController>();
        int index = Random.Range(0, flagTextures.Count);
        currentBoat.SetFlag(flagTextures[index], countryNames[index], countryAllowed[index]);
        currentBoat.MoveTo(inspectionPoint.position, false, velocidadBoteinicial, true);
        
    }

    public void AcceptBoat()
    {
        if (!gameActive) return;
        DisableButton();
        currentBoat.MoveTo(acceptPoint.position, true, velocidadBoteinicial, false);
        CheckDecision(true);
    }

    public void RejectBoat()
    {
        if (!gameActive) return;
        DisableButton();
        currentBoat.MoveTo(rejectPoint.position, true, velocidadBoteinicial, false);
        CheckDecision(false);
    }

    private void CheckDecision(bool accepted)
    {
        if (currentBoat.isAllowed == accepted)
        {
            acumuladorAciertos++;
            audioSource.PlayOneShot(succes);
        }
        else
        {
            acumuladorErrores++;
            audioSource.PlayOneShot(error);
        }

        velocidadBoteinicial += 1f;
        //puntosJugables--;
        Invoke(nameof(SpawnBoat), 2f);
    }

    private void EndGameDueToTime()
    {
        gameActive = false;
        DisableButton();
        ShowResults();
    }

    private void ShowResults()
    {
        ///aqui va la logica que llama al panel de resultados que muestras las estrellas
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
