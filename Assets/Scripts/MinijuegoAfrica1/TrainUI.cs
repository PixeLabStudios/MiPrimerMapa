using TMPro;
using UnityEngine;

public class TrainUI : MonoBehaviour
{

    public TextMeshProUGUI textCurrentAnimals;
    public TextMeshProUGUI textMaxAnimals;
    public TextMeshProUGUI textObjective;
    public TextMeshProUGUI textErrors;
   
    public void EnableText(TextMeshProUGUI text,bool op)
    {
        text.gameObject.SetActive(op);
    }
    public void ChangeText(TextMeshProUGUI text, string content) 
    {
        text.text = content;
    }
}
