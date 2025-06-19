using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
//
{
//    public GameObject[] animals;
//    public GameObject[] foods;
    public Transform[] spawnpoints;
    public GameObject[] objects;

    
    public IEnumerator Generate(float interval)
    {
        while (true)
        {         
            yield return new WaitForSeconds(interval);

            CreateObject();
            //Generateobjects();
        }
        
       
    }
    public void StopGenerate()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// crea de 1 a 3 objetos en la escena
    /// </summary>
    void CreateObject() 
    {
        int itemsCount = Random.Range(1, 3);
        
        List<int> numbers = new()
            {
            0,
            1,
            2

             };
            for (int i = 0; i < itemsCount; i++)
            {
            int rand = Random.Range(0, numbers.Count); // elige que punto usara para crear el objeto           
            int ind = Random.Range(0, objects.Length); // elige un objeto de la lista de objetos            
            Instantiate(objects[ind], spawnpoints[numbers[rand]].position, objects[ind].transform.rotation);
            numbers.RemoveAt(rand);
        }
    }
        /// <summary>
        /// crea siempre 2 animales y 2 comidas en la escena en orden aleatorio
        /// </summary>
        //void Generateobjects()
        //{
        //    List<int> numbers = new List<int>
        //    {
        //    0,
        //    1,
        //    2,
        //    3
        //    };
        //    for (int i = 0; i < 2; i++)
        //    {
        //        int rand = Random.Range(0, numbers.Count);
        //        int animal = Random.Range(0, animals.Length);
        //        Instantiate(animals[animal], spawnpoints[numbers[rand]].position, Quaternion.identity);
        //        numbers.RemoveAt(rand);
        //    }
        //    for (int i = 0; i < 2; i++)
        //    {
        //        int rand = Random.Range(0, numbers.Count);
        //        int food = Random.Range(0, foods.Length);
        //        Instantiate(foods[food], spawnpoints[numbers[rand]].position, Quaternion.identity);
        //        numbers.RemoveAt(rand);
        //    }
        //}



        

    
}
