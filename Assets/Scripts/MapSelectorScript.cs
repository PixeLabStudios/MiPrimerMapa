using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelectorScript : MonoBehaviour
{
    public List<GameObject> mapList = new List<GameObject>();
    public Button changeSceneButton;
    public TextMeshPro nameText;
    int currentMapIndex;
    int mapSize;
    string currentmap;


    void Start()
    {
        currentMapIndex = 0;
        mapSize = mapList.Count;
        mapList[0].SetActive(true);
        currentmap = mapList[0].name;
        nameText.text = currentmap; 
        changeSceneButton.onClick.AddListener(() =>GoToScene(currentmap ));
    }

    /// <summary>
    /// Cambia el mapa a visualizar.
    /// </summary>
   public void ChangeMap(int i)  
    {
        mapList[currentMapIndex].SetActive(false);
        currentMapIndex += i;
        if (currentMapIndex <0) 
        {
            currentMapIndex = mapSize - 1;
            
        }
        else if (currentMapIndex >= mapSize)
        {
            currentMapIndex = 0;
        }
        mapList[currentMapIndex].SetActive(true);
        nameText.text = mapList[currentMapIndex].name;
        currentmap = mapList[currentMapIndex].name;
       // Debug.Log(currentmap);
    }
    public void GoToScene(string name) 
    {
        Debug.Log("Cambiando a la escena: " + name);
        //SceneManager.LoadScene(name);
    }

    
    
}
