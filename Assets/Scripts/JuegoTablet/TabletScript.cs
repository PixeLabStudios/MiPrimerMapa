using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabletScript : MonoBehaviour
{
    public GameObject tabletPanel;
    public GameObject tabletObject;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI resultText;
    public Button herbivoreButton;
    public Button carnivoreButton;
    float yvalue;
    float targetYvalue;
    TabletGameManager manager;
    Vector3 position;
    
    private void Start()
    {
        nameText.gameObject.SetActive(false);
        herbivoreButton.gameObject.SetActive(false);
        carnivoreButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        manager = FindFirstObjectByType<TabletGameManager>();
        herbivoreButton.onClick.AddListener(() => manager.Choose("Herbivoro"));
        carnivoreButton.onClick.AddListener(() => manager.Choose("Carnivoro"));
        yvalue = -.47f;
        targetYvalue = yvalue;
        position =tabletObject.transform.localPosition;
    }

    private void Update()
    {
       yvalue = Mathf.Lerp(yvalue, targetYvalue, Time.deltaTime * 5f);
       position.y = yvalue;
       tabletObject.transform.localPosition = position;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))           
        {
            ChangeText(other.GetComponent<AnimalScript>().animalData);
            manager.SetAnswer(other.GetComponent<AnimalScript>());
            herbivoreButton.interactable = true;
            carnivoreButton.interactable = true;
            targetYvalue =-0.17f; 
        }
    }
   void ChangeText(Animal data) 
    {
        nameText.gameObject.SetActive(true);
        nameText.text = data.animalName;
        objectiveText.text = "¿Que tipo de animal es?";
        herbivoreButton.gameObject.SetActive(true);
        carnivoreButton.gameObject.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
           HideAnimal();
        }
    }
    
    public void HideAnimal()
    {
        nameText.gameObject.SetActive(false);
        objectiveText.text = "acercate un animal";
        herbivoreButton.gameObject.SetActive(false);
        carnivoreButton.gameObject.SetActive(false);
        targetYvalue = -0.47f; 
    }
}
