using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class RouteRestorer2 : MonoBehaviour
{
    public RouteObject.SpeciesType species;
    public Transform[] pathPoints; // Asignar 3 puntos desde el inspector
    public AnimalAI[] animals;     // Animales que deben recorrer la ruta

    private HashSet<string> objectsPlaced = new HashSet<string>();
    private bool routeActivated = false;

    public void RegisterObject(RouteObject obj)
    {
        if (routeActivated) return; // Ya está activa
        if (objectsPlaced.Contains(obj.objectID)) return;

        objectsPlaced.Add(obj.objectID);
        Destroy(obj.gameObject); // O dejarlo decorativamente

        if (objectsPlaced.Count >= 3)
        {
            routeActivated = true;
            StartCoroutine(MoveAnimalsThroughRoute());
        }
    }

    private IEnumerator MoveAnimalsThroughRoute()
    {
        foreach (AnimalAI animal in animals)
        {
            animal.isAggressive = false;
            animal.agent.isStopped = false;
            StartCoroutine(MoveAnimal(animal));
        }

        yield break;
    }

    private IEnumerator MoveAnimal(AnimalAI animal)
    {
        foreach (Transform point in pathPoints)
        {
            animal.agent.SetDestination(point.position);
            yield return new WaitUntil(() =>
                Vector3.Distance(animal.transform.position, point.position) < 1.5f);
        }

        animal.agent.isStopped = true;
        animal.animator.Play("Idle");
        VictoryManager.instance.RegisterArrival(); // <- Aquí marcamos la llegada
    }
}
