using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Scriptable Objects/QuestionData")]
public class QuestionData : ScriptableObject
{
    public string questionText;
    public string[] answers;
    public Sprite[] sprites;
    public string correctAnswer;
     
}
