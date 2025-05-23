using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
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

    private void Start()
    {
        bandera = puntosJugables;
        SpawnBoat();
    }
    public void SpawnBoat()
    {
        GameObject newBoat = Instantiate(boatPrefab, spawnPoint.position, Quaternion.identity);
        currentBoat = newBoat.GetComponent<BoatController>();

        int index = Random.Range(0, flagTextures.Count);
        currentBoat.SetFlag(flagTextures[index], countryNames[index], countryAllowed[index]);

        // 🚫 No se destruye al ir a inspección
        currentBoat.MoveTo(inspectionPoint.position,false,velocidadBoteinicial);
    }

    public void AcceptBoat()
    {
        Debug.Log("Botón Aceptar presionado");

        if (currentBoat == null)
        {
            Debug.LogError("❌ currentBoat está null. ¿Llamaste a SpawnBoat?");
            return;
        }

        // ✅ Se destruye al llegar al punto de aceptación
        currentBoat.MoveTo(acceptPoint.position, true,velocidadBoteinicial);
        CheckDecision(true);
    }

    public void RejectBoat()
    {
        Debug.Log("Botón Rechazar presionado");

        // ✅ Se destruye al llegar al punto de rechazo
        currentBoat.MoveTo(rejectPoint.position, true,velocidadBoteinicial);
        CheckDecision(false);
    }


    private void CheckDecision(bool accepted)
    {
        if (currentBoat.isAllowed == accepted)
        {
            acumuladorAciertos += 1;
            Debug.Log("Decisión correcta");
            velocidadBoteinicial += 1.5f;

        }
        else
        {
            acumuladorErrores += 1;
            Debug.Log("Decisión incorrecta");
            velocidadBoteinicial += 1.5f;
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
}
