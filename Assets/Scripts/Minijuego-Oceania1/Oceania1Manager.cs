using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oceania1Manager : MonoBehaviour
{
    public List<AnimalsDrag> options;
    public List<AnimalsOceania> data;

    public int errors;
    public int correct;
    int animalsNumber;
    public bool canDrop;

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
            ChangeDrag(false);
            bool iscorrect;
            if (op.animal.animalRegion == region)
            {
                Debug.Log("correcto");
                correct++;
                iscorrect = true;

            }
            else
            {
                Debug.Log("incorrecto");
                //Incorrecto
                errors++;
                iscorrect = false;
            }
            yield return new WaitForSeconds(1f);
            if (correct >= monumentsNumber)
            {
                //Juego terminado, mostrar resultados
                //Panel Star
                Debug.Log("el juego termino");
            }
            else
            {
                if (iscorrect)
                {
                    if (monumentsRandom.Count > 0)
                    {
                        int randomIndex = Random.Range(0, monumentsRandom.Count);
                        op.monument = monumentsRandom[randomIndex];
                        op.LoadData();
                        monumentsRandom.RemoveAt(randomIndex);
                    }
                    else
                    {
                        Destroy(op.gameObject);
                    }
                }
                ChangeDrag(true);
            }
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
}

