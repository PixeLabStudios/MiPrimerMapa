using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class America2Manager : MonoBehaviour
{
    public America2UI ui;
    public AudioSource audioSource;
    public int hp;
    public int correct;
    public int errors;
    int currentLevel;
    public Room[] rooms;

    public TextMeshProUGUI questionText;
    private void Awake()
    {
        ui =GetComponent< America2UI>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        hp = 5;
        currentLevel = 0;
        correct = 0;
        NextQuestion();
    }
    void LoadQuestion(Room currentRoom) 
    { 
        questionText.text = currentRoom.data.questionText;
    }
    public void NextQuestion() 
    {
        currentLevel++;
        if (currentLevel == rooms.Length +1) 
        {
            Debug.Log("Termino el juego");
            questionText.text = " Has completado el juego.";
            //Panel de estrellas
            return;
        }
        else
        {

            LoadQuestion(rooms[currentLevel -1]);
        }
    }
}
