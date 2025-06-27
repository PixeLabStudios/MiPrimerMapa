using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    
    public List <GameObject> prefabs;
    public List<Transform> spawnLocations;
    public Material[] materials;
    float genCooldown;
  
    
    void Start()
    {
       
        genCooldown = 2;
          
    }
   
    
   public IEnumerator GenerateObjects() 
    {
        yield return new WaitForSeconds(1);
        int randomobject;
        int randomSpot;
        while (true)
        {
            
            List<int> indexes = new List<int>();
            for (int i = 0; i < spawnLocations.Count; i++)
            {
                indexes.Add(i);
            }          
            
           
            
            for (int i = 0; i < 3; i++) 
            {
                
                randomobject = Random.Range(0, prefabs.Count);
                randomSpot = Random.Range(0, indexes.Count);
                switch (randomobject)
                {
                    case 0:
                        CreateContainer(randomSpot,indexes);
                        break;
                    case 1:
                        CreateCurrent(randomSpot, indexes);
                        break;
                    case 2:
                        CreateMine(randomSpot, indexes);
                        break;
                }
                indexes.RemoveAt(randomSpot);
            }

            yield return new WaitForSeconds(genCooldown);
        }
    }

    void CreateContainer(int spot,List<int> indexes) 
    {
        GameObject a  =  Instantiate(prefabs[0], spawnLocations[indexes[spot]].position, prefabs[0].transform.rotation);
        int randomMaterial = Random.Range(0, materials.Length);
        a.GetComponent<Renderer>().material = materials[randomMaterial];
        a.transform.Rotate(new Vector3(0, 0, Random.Range(-15, 15)));
    }
    void CreateCurrent(int spot, List<int> indexes)
    {
        GameObject a = Instantiate(prefabs[1], spawnLocations[indexes[spot]].position,prefabs[1].transform.rotation);
    }
    void CreateMine(int spot, List<int> indexes)
    {
        Vector3 pos = spawnLocations[indexes[spot]].position;
        pos.x += Random.Range(-5,5);
        pos.z += Random.Range(-5,5);
        GameObject a = Instantiate(prefabs[2], pos, prefabs[2].transform.rotation);
    }

    public void StopGeneration() { StopCoroutine(GenerateObjects()); }
    public void StartGeneration() { StartCoroutine(GenerateObjects()); }
}
