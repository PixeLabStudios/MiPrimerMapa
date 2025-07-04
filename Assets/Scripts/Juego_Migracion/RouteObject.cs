using UnityEngine;

public class RouteObject : MonoBehaviour
{
    public enum SpeciesType { Bison, Caribou, Moose }
    public SpeciesType species;
    public string objectID; // único para evitar duplicados

    private void Awake()
    {
        if (string.IsNullOrEmpty(objectID))
        {
            objectID = System.Guid.NewGuid().ToString();
        }
    }
}
