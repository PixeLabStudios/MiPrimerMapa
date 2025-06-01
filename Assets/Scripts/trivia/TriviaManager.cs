using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TriviaManager : MonoBehaviour
{
    [System.Serializable]
    public class SerializableList<T>
    {
        public List<T> list;
    }
    #region Variables
    int score; // puntaje del jugador
    int errors; // errores del jugador
    int currentRound;
    int maxRounds;
    public bool canClick;
    bool isAnswering; 
    float timeToAnswer; 
    float time;
    string correctAnswer;
    public GameObject QuestionPanel;
    
    #endregion

    #region Panel De Cartas
    public GameObject cardsPanel; // panel que contiene las cartas de trivia en la escena.
    public TriviaCard[] triviaCards;
    public TextMeshProUGUI nametext;
    public Button playButton;
    public TextMeshProUGUI instructionText;
    public GameObject roundPanel;
    public TextMeshProUGUI roundText;
    #endregion

    #region Panel de Preguntas

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI resText;
    public TextMeshProUGUI[] answerTexts; // textos de las respuestas de los botones
    public Button[] answerButtons; // botones de las respuestas
    public GameObject timePanel;
    #endregion

    #region listas
    private SerializableList<CardData> dataList; //Recibe todos los datos de las cartas de trivia desde un json,pierde un dato al final de cada ronda.
    private SerializableList<CardData> dataRandom;//se usa para asignar los datos de las cartas de trivia a las cartas de trivia en el juego cada ronda.

    #endregion



    void Start()
    {
        isAnswering = false;
        canClick = false;
        maxRounds = 3;     
        nametext.gameObject.SetActive(false);
        QuestionPanel.SetActive(false);
        cardsPanel.SetActive(true);
        playButton.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(false);
        dataList = JsonUtility.FromJson<SerializableList<CardData>>(File.ReadAllText(Application.dataPath + "/Resources/JSON/DatosTrivia.json"));
        roundPanel.SetActive(false);
        
    }
    public void HandleText(Button button)
    {
        if (canClick) 
        { 
        TextMeshProUGUI text = button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(CheckAnswer(text.text)); // comprueba la respuesta del jugador al hacer click en un boton de respuesta.
        }
    }
    private void Update()
    {
        if (isAnswering) 
        {
            time -= Time.deltaTime; // resta el tiempo de respuesta al tiempo restante.
            timeText.text =Mathf.Clamp(time,0,timeToAnswer).ToString("F0");
            if (time <= 0 && isAnswering) // si el tiempo se acaba
            {
                
                StartCoroutine(CheckAnswer("")); 
                canClick = false; // permite hacer click en las cartas de trivia.             
                Debug.Log("Tiempo agotado!"); // imprime en la consola que el tiempo se acabo.
            }
        }  
    }
    /// <summary>
    /// Empieza el juego desde el un boton.
    /// </summary>
    public void Play() 
    {
        
        score = 0;
        errors = 0;
        timeToAnswer = 20f; 

        canClick = true; 
        currentRound = 1;
        timeText.text = time.ToString("F0"); 
        instructionText.gameObject.SetActive(true); 
        playButton.gameObject.SetActive(false);
        QuestionPanel.SetActive(false);
        roundPanel.SetActive(true);
        timePanel.SetActive(false);
        roundText.text =  currentRound + "/" + maxRounds;        
        LoadData(); 
    }
    /// <summary>
    /// Carga los datos a las cartas cada ronda.
    /// </summary>
    void LoadData() 
    {
            
            dataRandom = JsonUtility.FromJson<SerializableList<CardData>>(File.ReadAllText(Application.dataPath + "/Resources/JSON/DatosTrivia.json")); // todos los datos    
            Debug.Log(dataRandom.list.Count); // imprime en la consola que los datos de trivia se han cargado correctamente.
            int randomIndex;
            foreach (TriviaCard card in triviaCards) 
            {
              card.id = System.Array.IndexOf(triviaCards, card); // asigna el id de la carta de trivia.
              randomIndex = Random.Range(0, dataRandom.list.Count); // selecciona un indice aleatorio de la lista de datos de trivia.
              card.data = dataRandom.list[randomIndex]; // asigna los datos de la carta de trivia
              card.cardName = card.data.name; // Asignar el nombre 
              card.ChangeImage(Resources.Load<Sprite>(card.data.imagePath)); // Cargar la imagen
              dataRandom.list.RemoveAt(randomIndex); // elimina el dato de la lista de datos de trivia para que no se repita en la siguiente carta.
            }
            Debug.Log(dataRandom.list.Count); // imprime en la consola que los datos de trivia se han cargado correctamente.

    }

    /// <summary>
    ///  Carga los textos de la pregunta y las respuestas de la carta de trivia seleccionada.
    /// </summary>
    /// <param name="card"></param>

    public IEnumerator LoadQuestion(TriviaCard card)
    {
        float cardXpos = card.gameObject.GetComponent<RectTransform>().anchoredPosition.x; // obtiene la posicion x de la carta de trivia.
        nametext.gameObject.SetActive(true); // activa el texto del nombre de la carta de trivia.
        nametext.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(cardXpos, 130); // pone el nombre de la carta sobre la carta elegida.
        nametext.text = card.cardName;
        canClick = false;
        HideAllButtons();
        yield return new WaitForSeconds(2f);
        nametext.gameObject.SetActive(false); 
        cardsPanel.SetActive(false);
        QuestionPanel.SetActive(true);

        int randomIndex = Random.Range(0, card.data.questions.Length); // selecciona una pregunta aleatoria de la carta de trivia.
        LoadPanel(card.data, randomIndex); // carga la primera pregunta de la carta de trivia.
        canClick = true; // permite hacer click en las cartas de trivia.
        correctAnswer = card.data.questions[randomIndex].correctAnswer;
        time = timeToAnswer;
        timePanel.SetActive(true); // activa el panel de tiempo.
        isAnswering = true; 
        resText.gameObject.SetActive(false);
    }
    void LoadPanel(CardData datos, int questionIndex) 
    {
        questionText.text = datos.questions[questionIndex].questionText;
        for (int i = 0; i<datos.questions[questionIndex].answers.Length; i++) 
        {
            answerButtons[i].gameObject.SetActive(true);
            answerTexts[i].text = datos.questions[questionIndex].answers[i];  
        }
    }
    

    IEnumerator CheckAnswer(string answer) 
    {

       
        canClick = false;
        isAnswering = false;
        if (answer == "")

        {
            Debug.Log("Se acabo el tiempo"); 
            resText.gameObject.SetActive(true);
            resText.text = "Se te Acabo el tiempo";
            errors++; 
        }
        else 
        {
            if (answer == correctAnswer)
            {
                Debug.Log("Respuesta correcta!"); 
                resText.gameObject.SetActive(true);
                resText.text = "respuesta correcta!";
                score++; 
            }
            else
            {
                Debug.Log("Respuesta incorrecta!");
                resText.gameObject.SetActive(true);
                resText.text = "respuesta incorrecta!";
                errors++; 
            }
        }
        
        
        yield return new WaitForSeconds(1.0f);

        currentRound++; // aumenta el contador de rondas.     
        if (currentRound > maxRounds)
        {
            Debug.Log("Juego terminado!"); // imprime en la consola que el juego ha terminado.
            //Termina el juego. mostrar el panel final
        }
        else 
        {

            //vuele a elegir otra carta
            roundText.text =  currentRound + "/" + maxRounds;
            cardsPanel.SetActive(true); // activa el panel de cartas de trivia.
            QuestionPanel.SetActive(false); // desactiva el panel de preguntas.
            HideCards();
            yield return new WaitForSeconds(2f); // espera un segundo para ocultar las cartas de trivia.
            LoadData();
            canClick = true; 
        }
    }

    void HideCards() 
    {
        foreach (TriviaCard card in triviaCards) 
        {
           card.StartCoroutine(card.HideCard()); // oculta la carta de trivia.
        }
    }
    void HideAllButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false); // desactiva todos los botones de respuestas.
        }
    }
}
