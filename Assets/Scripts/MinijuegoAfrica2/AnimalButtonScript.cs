
using UnityEngine;
using UnityEngine.UI;

public class AnimalButtonScript : MonoBehaviour
{
    Africa2Manager manager;
    Button button;
    public AfricanAnimal data;

    private void Awake()
    {
        manager = FindFirstObjectByType<Africa2Manager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    public void LoadData(AfricanAnimal newData)
    {
        
        data = newData;
        button.image.sprite = data.animalImage;
        button.interactable = true;

        
    }
    public void Delete() 
    {
        this.data = null;
        button.image.sprite = null;
    }
    public void LoadFake(Sprite fake) 
    {
        button.image.sprite = fake;
        button.interactable = true; //reactivo el boton;
    }

   public void onClick() 
    {
        if (data == null)
        {
            Debug.Log("Animal Falso");
            manager.errors += 2; 
        }
        else 
        {
            //sino reviso si es de esa region
            if (data.region == manager.GetCurrentRegion())
            {
                Debug.Log("Correcto");
                button.interactable = false;
                manager.correct++;
                button.image.sprite =null;
                manager.RemoveAnimal(data);
                data = null;
                if (manager.correct >= 3) 
                {
                    manager.currentRound++;
                    manager.NextRound(manager.currentRound);
                }
            }
            else {
                Debug.Log("Incorrecto");
                manager.errors++;
            }
        }
        
    }
}
