using UnityEngine;

[CreateAssetMenu(fileName = "Monument", menuName = "Scriptable Objects/Monument")]
public class Monument : ScriptableObject
{
    public string monumentName;
    public Sprite monumentImage;
    public string monumentDescription;
    public string monumentRegion;
}
