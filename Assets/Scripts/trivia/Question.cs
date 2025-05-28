using System;
using UnityEngine;

[Serializable]

//una clase que representa una pregunta de trivia
public class Question
{
  public string questionText; // texto de la pregunta
  public string[] answers = new string[4]; // texto de las respuestas
  public string correctAnswer; //la respuesta correcta
  

    ///contructor de la clase
   public Question(string questionText, string[] answers, string correctAnswerIndex)
    {
        this.questionText = questionText;
        this.answers = answers;
        this.correctAnswer = correctAnswerIndex;
       
    }
   
}
