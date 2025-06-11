using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Scriptable Objects/Animal")]
public class Animal : ScriptableObject
{
    public string animalName;
    public string food;
    public string threats;
    public string classification;
}
