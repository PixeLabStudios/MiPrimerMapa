using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PinguAsustado : MonoBehaviour
{
    public NavMeshAgent pengu;
    public GameObject punto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Asustado());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Asustado()
    {
        pengu.SetDestination(punto.transform.position);
        yield return null;
    }
}
