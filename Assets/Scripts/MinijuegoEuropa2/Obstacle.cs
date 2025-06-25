using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public Europe2Manager manager;
    
    
    public string obstacleName;
    private void Awake()
    {
        manager = FindFirstObjectByType<Europe2Manager>();
    }
    public virtual void Move() 
    {
        transform.position -= new Vector3(0, 0, manager.objectsSpeed * Time.deltaTime);
    }
    public abstract void Impact(DrakkarScript script);
}
