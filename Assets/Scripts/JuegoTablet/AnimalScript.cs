using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    public Animal animalData;
    Collider animalCollider;

    private void Awake()
    {
        animalCollider = GetComponent<Collider>();
    }

    public void DisableCollider()
    {
       animalCollider.enabled = false;
    }
}
