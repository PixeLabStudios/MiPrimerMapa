using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oceania1Manager : BaseGameManager
{
    public List<AnimalsDrag> options;
    public List<AnimalsOceania> data;

    public int errors;
    public int correct;
    int animalsNumber;
    public bool canDrop;

    public PanelManager panelManager; // Referencia al panel manager
    public StarScoreDisplay starScoreDisplay; // Referencia al sistema de estrellas

    private void Start()
    {
        animalsNumber = data.Count;
        int randomIndex;
        foreach (AnimalsDrag op in options)
        {
            randomIndex = Random.Range(0, data.Count);
            op.animal = data[randomIndex];
            op.LoadData();
            data.RemoveAt(randomIndex);
        }
        canDrop = true;
    }

    public IEnumerator CheckAnswer(AnimalsDrag op, string region, bool canAct)
    {
        if (canAct)
        {
            bool isCorrect = false;
            ChangeDrag(false);

            if (op.animal.animalRegion == region)
            {
                Debug.Log("correcto");
                correct++;
                isCorrect = true;
            }
            else
            {
                Debug.Log("incorrecto");
                errors++;
                isCorrect = false;
            }

            yield return new WaitForSeconds(1f);

            if (isCorrect)
            {
                options.Remove(op);
                Destroy(op.gameObject);

                if (options.Count == 0)
                {
                    // Juego terminado
                    Debug.Log("el juego termino");
                    ShowVictoryPanel();
                }
            }

            ChangeDrag(true);
        }
    }

    void ChangeDrag(bool b)
    {
        foreach (AnimalsDrag op in options)
        {
            canDrop = b;
            op.SetCanDrag(b);
        }
    }

    void ShowVictoryPanel()
    {
        int puntosJugables = correct + errors;
        float score = (puntosJugables == 0) ? 0f : (float)correct / puntosJugables * 120f;

        panelManager.MostrarSoloPanel("PanelFinJuego");
        starScoreDisplay.ShowStars(score);
    }
}
