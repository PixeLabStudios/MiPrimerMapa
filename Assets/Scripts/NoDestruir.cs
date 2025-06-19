using UnityEngine;
using System.Collections;
using System;

public class NoDestruir : MonoBehaviour
{
    private void Awake()
    {
        var noDestruirEntreEscenas = FindObjectsOfType<NoDestruir>();
        //var noDestruirEntreEscenas = FindObjectsByType<NoDestruir>();
        if (noDestruirEntreEscenas.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

}
