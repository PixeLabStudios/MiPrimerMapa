using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Europe2Manager : MonoBehaviour
{
    public DrakkarScript drakkarScript;
    public BossScript bossScript;
    ObstacleGenerator obstacleGenerator;
    public List<EnemySubScript> enemySubs;
    public int progress;    
    public int objectsSpeed;
    public int threshold;

    private void Awake()
    {
        obstacleGenerator = FindFirstObjectByType<ObstacleGenerator>();
        drakkarScript = FindFirstObjectByType<DrakkarScript>();
    }

    void Start()
    {
        progress = 0;
        objectsSpeed = 25;
        threshold = 35;
        StartCoroutine(StartGame());
    }
    bool IsInactive() 
    {
        return !(bossScript.gameObject.activeSelf);
    }
    IEnumerator StartGame() 
    {
        bossScript.gameObject.SetActive(false);
        Coroutine generation= StartCoroutine(obstacleGenerator.GenerateObjects());
        //obstacleGenerator.StartGeneration();
        while (progress < threshold) 
        {
            progress += 7;
          //Debug.Log("progreso " + progress);
            yield return new WaitForSeconds(7);
        }
        StopCoroutine(generation);
        //----------1da etapa---------//
        bossScript.gameObject.SetActive(true);
        Coroutine bossMove = StartCoroutine(bossScript.HandleMovement());
        yield return new WaitUntil(IsInactive);
        threshold += 35;
        Debug.Log("el jefe se fue. Vuelvo a generar");
        generation = StartCoroutine(obstacleGenerator.GenerateObjects());
        while (progress < threshold)
        {
            progress += 7;
           //Debug.Log("progreso " + progress);
            yield return new WaitForSeconds(7);
        }

        StopCoroutine(generation);
        //----------2da etapa-jefe y 1 sub---------//

        bossScript.gameObject.SetActive(true);
        
        bossMove = StartCoroutine(bossScript.HandleMovement());
        enemySubs[0].gameObject.SetActive(true);
        yield return new WaitUntil(IsInactive);
        threshold += 30;
        Debug.Log("el jefe se fue. Vuelvo a generar");
        StartCoroutine(enemySubs[0].Retreat());
        generation = StartCoroutine(obstacleGenerator.GenerateObjects());
        
        while (progress < threshold)
        {
            progress += 7;
            //Debug.Log("progreso " + progress);
            yield return new WaitForSeconds(7);
        }
        StopCoroutine(generation);
        //------------3era etapa jefe y 2 sub----------//
        bossScript.gameObject.SetActive(true);
        bossScript.SetTurretStats(bossScript.mainTurret, 3, 4, 32);
        bossMove = StartCoroutine(bossScript.HandleMovement());
        enemySubs[0].gameObject.SetActive(true);
        enemySubs[1].gameObject.SetActive(true);
        yield return new WaitUntil(BossIsdead);
        StartCoroutine(enemySubs[0].Retreat());
        StartCoroutine(enemySubs[1].Retreat());
        
    }

    bool BossIsdead() 
    {
        return bossScript.hp <= 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

}
