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
                isCorrect=true;

            }
            else
            {
                Debug.Log("incorrecto");
                //Incorrecto
                errors++;
                isCorrect = false;
            }
            yield return new WaitForSeconds(1f);
            if (isCorrect) 
            {
                //Saco el correcto de la lista y lo destruyo
                options.Remove(op);
                Destroy(op.gameObject);
                if (options.Count == 0)
                {
                    //Juego terminado, mostrar resultados
                    //Panel Star
                    Debug.Log("el juego termino");
                }
            }
            //  habilito el poder arrastrar de nuevo
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
}

