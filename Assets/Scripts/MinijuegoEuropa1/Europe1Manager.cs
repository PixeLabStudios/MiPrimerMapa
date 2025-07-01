using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Europe1Manager : BaseGameManager
{
    public List<Unit> unitList =new();
    public List<Transform> spawnPoints =new();
    public GameObject meleeRobot;
    public GameObject rangeRobot;
    public GameObject lionRobot;
    int currentRound;
    int waitTime;
    void Start()
    {
        currentRound = 1;
        waitTime = 2;
        StartCoroutine(Game());
    }

    IEnumerator Game() 
    {
        Debug.Log("Ronda 1");
        StartCoroutine(StartRoundOne());
        yield return new WaitUntil(AreNoEnemies);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Ronda 2");
        StartCoroutine(StartRoundTwo());
        yield return new WaitUntil(AreNoEnemies);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Ronda 3");
        StartCoroutine(StartRoundThree());
        yield return new WaitUntil(AreNoEnemies);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Ronda 4");
        StartCoroutine(StartRoundFour());
        yield return new WaitUntil(AreNoEnemies);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Ronda 5");
        StartCoroutine(StartRoundFive());
        yield return new WaitUntil(AreNoEnemies);
        //logica que llama al POanel de estrellas


        Debug.Log("termino el juego");
    }
    bool AreNoEnemies() { return unitList.Count <= 0; }
    
    
    IEnumerator StartRoundOne() 
    {
        //primer oleada: //salen 3 robots, con una diferencia de 2 segundos cada uno, cuerpo a cuerpo con 100 de vida que atacan al jugador

        for (int i = 0;i<3;i++) 
        {
            GameObject a=  Instantiate(meleeRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position,Quaternion.identity);
            Unit unit = a.GetComponent<Unit>();
            unit.SetStats( 100, 5, 5, 0.6f);
            
            unitList.Add(unit);
            yield return new WaitForSeconds(2);
        }
       
    }
    IEnumerator StartRoundTwo()
    {
        //segunda oleada: salen 5 robots con 200 de vida con diferencia de 2 segundos

        for (int i = 0; i < 5; i++)
        {
            GameObject a = Instantiate(meleeRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            Unit unit = a.GetComponent<Unit>();
            unit.SetStats(200, 5, 5, 0.6f);
            unitList.Add(unit);
            yield return new WaitForSeconds(2);
        }

    }
    IEnumerator StartRoundThree()
    {
        //tercer oleada:salen salen 3 robots de 100 de vida con una diferencia de 3 segundos cada uno
        // y salen 3 robots leones con 150 de vida con una diferencia de 3 segundos(primero sale el robot normal, luego de 1 segundo sale el robot leon)
        
        for (int i = 0; i < 3; i++)
        {
            GameObject a = Instantiate(meleeRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            Unit unit = a.GetComponent<Unit>();
            unit.SetStats(100, 5, 5, 0.6f);
            unitList.Add(unit);

            yield return new WaitForSeconds(1);

            a = Instantiate(lionRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            unit = a.GetComponent<Unit>();
            unit.SetStats(150, 8, 7, 0.5f);
            unitList.Add(unit);
            yield return new WaitForSeconds(2);
        }

    }
    IEnumerator StartRoundFour()
    {
        //cuarta oleada:
        //    salen 5 robots de 200 de vida con diferencia de 3 segundos cada uno y salen
        //    3 robots leones de 300 de vida con diferencia de 3 segundos cada uno

        for (int i = 0; i < 5; i++)
        {
            GameObject a = Instantiate(meleeRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            Unit unit = a.GetComponent<Unit>();
            unit.SetStats(200, 5, 10, 0.5f);
            unitList.Add(unit);
            yield return new WaitForSeconds(3);
        }
        for (int i = 0; i < 3; i++) 
        {
            GameObject a = Instantiate(lionRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            Unit unit = a.GetComponent<Unit>();
            unit.SetStats(300, 15, 12, 0.5f);
            unitList.Add(unit);
            yield return new WaitForSeconds(3);
        }
    }
    IEnumerator StartRoundFive()
    {
        //quinta oleada:
        //    salen 3 robots de 200 de vida cada 4 segundos
        //    despues de 1 segundo del primer robot sale el primer robot leon de 300 de vida
        //    un total de 3 robots leones con diferencia de 4 segundos
        //    Salen 3 robots con arma de 50 de vida con diferencia de 4 segundos cada uno(atacan a distancia, disparos con la velocidad suficiente para poder esquivarlos)
        //    caminan mas lento que el jugador e intentan mantener distancia con el jugador.

        for (int i = 0; i < 3; i++)
        {
            GameObject a = Instantiate(meleeRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            Unit unit = a.GetComponent<Unit>();
            unit.SetStats(200, 5, 10, 0.5f);
            unitList.Add(unit);
            yield return new WaitForSeconds(1);
            a = Instantiate(meleeRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            unit = a.GetComponent<Unit>();
            unit.SetStats(200, 10, 10, 0.5f);
            unitList.Add(unit);
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject a = Instantiate(rangeRobot, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            Unit unit = a.GetComponent<Unit>();
            unit.SetStats(50, 5, 10, 0.5f);
            unitList.Add(unit);
            yield return new WaitForSeconds(4);
        }
    }

}
