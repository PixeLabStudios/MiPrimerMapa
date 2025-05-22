using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    public GameObject[] animals;
    public GameObject[] foods;
    public Transform[] spawnpoints;
           

    private void Start()
    {
        StartCoroutine(Generate());
        
    }
    public IEnumerator Generate()
    {
        while (true)
        {         
            yield return new WaitForSeconds(4f);
           
            Generateobjects();
        }
        
        // Code to generate an object
    }

    void Generateobjects() 
    {
        List<int> numbers = new List<int>
        {
            0,
            1,
            2,
            3
        };
        for (int i = 0; i < 2; i++)
        {
            int rand = Random.Range(0, numbers.Count);
            int animal = Random.Range(0, animals.Length);
            Instantiate(animals[animal], spawnpoints[numbers[rand]].position, Quaternion.identity);
            numbers.RemoveAt(rand);
        }
        for (int i = 0; i < 2; i++) 
        {
            int rand = Random.Range(0, numbers.Count);
            int food = Random.Range(0, foods.Length);
            Instantiate(foods[food], spawnpoints[numbers[rand]].position, Quaternion.identity);
            numbers.RemoveAt(rand);
        }
            



    }

    
}
