using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class TrainManager : MonoBehaviour
{
    
    public int errors;
    public int points;
    int maxAnimals;
    bool isRoundStarted;
    float time;
    float maxTime;

    public Transform centerPosition;
    public Transform initialPosition;
    public  List<AnimalMove> africanAnimals = new List<AnimalMove>();
    public  List<AnimalMove> otherAnimals = new List<AnimalMove>();
    public List<AnimalMove> animalsInTrain = new List<AnimalMove>();
    public List<AnimalMove> animalsSelected = new List<AnimalMove>();
    public LayerMask mask;

    private void Start()
    {
        maxAnimals = africanAnimals.Count;
        isRoundStarted = false;
        maxTime = 30f;
        time = maxTime;
        errors = 0;
        points = 0;
        StartRound();
    }

    private void Update()
    {
        
        
        if (isRoundStarted)
        {
            if (Input.touchCount == 1) 
            { 
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit,1000,mask))
                    {
                        StartCoroutine(CheckAnswer(hit.collider.GetComponent<AnimalMove>()));
                        isRoundStarted = false;
                    }
                }
            }

            time -= Time.deltaTime;
            if (time <= 0)
            {
                isRoundStarted=false;
            }
        }
    }
    void StartRound() 
    {
        isRoundStarted = false;
        time = maxTime;
        int randomIndex;
        if (africanAnimals.Count < 2)
        {
            animalsSelected.Add(africanAnimals[0]);// solo mando el que queda
        }
        else 
        {               
         for (int i = 0; i < 2; i++)
         {
            randomIndex = Random.Range(0, africanAnimals.Count);
            animalsSelected.Add(africanAnimals[randomIndex]);

            africanAnimals.RemoveAt(randomIndex);

         }
         
        }
        //agarro un animal que no es africano
            randomIndex = Random.Range(0, otherAnimals.Count);
            animalsSelected.Add(otherAnimals[randomIndex]);
            otherAnimals.RemoveAt(randomIndex);
            StartCoroutine(MoveAnimalsToCenter(centerPosition.position));
        
    }

     IEnumerator MoveAnimalsToCenter(Vector3 pos)
    { 
        Debug.Log("Empezando ronda");
        foreach (AnimalMove animal in animalsSelected)
        {
            animal.MoveToCenter(pos);
        }
        
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(AnimalsStopped);
        Debug.Log("Todos los animales han llegado al centro");
        isRoundStarted = true;
    }
    bool AnimalsStopped() 
    {
        foreach (AnimalMove animal in animalsSelected)
        {
           
            if (animal.IsMoving())
            {
                return false;
            }
        }
        return true;
    }
    public IEnumerator CheckAnswer(AnimalMove animal) 
    {
        isRoundStarted = false;
        AnimalMove script= animal.GetComponent<AnimalMove>();
        foreach (AnimalMove s in animalsSelected)
        {
            Vector3 trainPos = transform.position;
            trainPos.z += Random.Range(-6f, 5f);
            s.MoveTo(trainPos);
        }
        animal.MoveTo(script.initialPos);
        
        
        if (script.data== null)
        {      
            Debug.Log("Correcto");
            Debug.Log(AnimalsStopped());
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(AnimalsStopped);

            
           

        }
        else 
        {
            Debug.Log("Incorrecto");
            script.points = 0;
            errors++;
            africanAnimals.Add(script);

        }
        

        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(AnimalsStopped);
        
        if (animalsInTrain.Count >= maxAnimals) 
        {
            Debug.Log("Fin del juego, has conseguido " + points + " puntos y has cometido " + errors + " errores.");
            //llamar al coso de estrellas
        }
        else 
        {
            animalsSelected.Clear();
            StartRound();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        AnimalMove script = other.GetComponent<AnimalMove>();
        if (script.data == null)
        {
            //entro un animal que no es africano
            Debug.Log("Animal no africano entró al tren: " + script.name);
            script.Warp();
            otherAnimals.Add(script);
            animalsSelected.Remove(script);
        }
        else 
        {
            points += script.points;
            animalsInTrain.Add(script);
            animalsSelected.Remove(script);
            //poner sonido de acierto
        }
    }
}
