using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RouteRestorer : MonoBehaviour
{
    public string requiredTag;
    public Transform[] pathPoints;
    public AnimalAI[] animals;
    private HashSet<string> objectsPlaced = new HashSet<string>();

    public RouteObject.SpeciesType species;

    public void RegisterObject(/*RouteObject obj*/ string id)
    {
        /*if (objectsPlaced.Contains(obj.objectID)) return;

        objectsPlaced.Add(obj.objectID);
        Destroy(obj.gameObject); // o dejarlo quieto si querés mostrar que fue colocado

        if (objectsPlaced.Count >= 3)
        {
            StartCoroutine(StartMigration());
        }*/
        objectsPlaced.Add(id);
        if (objectsPlaced.Count == 3)
        {
            foreach (var a in animals)
            {
                a.StartCoroutine(FollowPath(a));
            }
        }
    }

    private IEnumerator FollowPath(AnimalAI animal)
    {
        animal.isAggressive = false;
        animal.agent.isStopped = false;
        foreach (var point in pathPoints)
        {
            animal.agent.SetDestination(point.position);
            yield return new WaitUntil(() => Vector3.Distance(animal.transform.position, point.position) < 1f);
        }
        animal.agent.isStopped = true;
        animal.animator.Play("Idle");
        VictoryManager.instance.RegisterArrival();
    }
}
