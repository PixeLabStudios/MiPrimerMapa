using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Africa3Manager : MonoBehaviour
{
    public List<Monument> monumentsRandom = new List<Monument>();
    [HideInInspector]public List<Monument> monumentsList = new List<Monument>(); 
    public List<MonumentScript>  options = new List<MonumentScript>();

    public int errors;
    public int correct;
    int monumentsNumber;
    public bool canDrop;
    private void Start()
    {
        monumentsNumber = monumentsRandom.Count;
        foreach (Monument copy in monumentsRandom) 
        {
            monumentsList.Add(copy); // guardo esta copia para usar en la guia
        }
        errors = 0;
        correct = 0;
        int randomIndex;
        foreach (MonumentScript op in options) 
        {
            randomIndex = Random.Range(0, monumentsRandom.Count);
            op.monument = monumentsRandom[randomIndex];
            op.LoadData();
            monumentsRandom.RemoveAt(randomIndex);
           
        }
        canDrop = true;
    }

   public IEnumerator CheckAnswer(MonumentScript op, string region,bool canAct) 
    {
       if (canAct) 
        {
            ChangeDrag(false);
            bool iscorrect;
            if (op.monument.monumentRegion == region)
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
                    else {
                        Destroy(op.gameObject);
                    }
                }
                ChangeDrag(true);
            }
        }
    }
    void ChangeDrag(bool b) 
    {
        foreach (MonumentScript op in options) 
        {
            canDrop = b;
            op.SetCanDrag(b);
        }
    }
}
