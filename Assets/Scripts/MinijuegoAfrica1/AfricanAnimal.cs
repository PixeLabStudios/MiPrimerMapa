using UnityEngine;

[CreateAssetMenu(fileName = "AfricanAnimal", menuName = "Scriptable Objects/AfricanAnimal")]
public class AfricanAnimal : ScriptableObject
{
    public string animalName;
    public string region;
    public string description;
    public Sprite animalImage;
}
