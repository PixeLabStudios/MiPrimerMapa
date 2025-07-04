using UnityEngine;

public class RoutePoint : MonoBehaviour
{
    //public RouteRestorer restorer;
    public RouteRestorer2 restorer;
    private bool occupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (occupied || !other.CompareTag("Pickup")) return;

        RouteObject obj = other.GetComponent<RouteObject>();
        if (obj != null && obj.species == restorer.species)
        {
            occupied = true;
            restorer.RegisterObject(obj);
        }
    }
}
