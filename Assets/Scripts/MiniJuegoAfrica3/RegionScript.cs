using UnityEngine;
using UnityEngine.EventSystems;

public class RegionScript : MonoBehaviour, IDropHandler
{
    Africa3Manager manager;
    private void Awake()
    {
       manager = FindFirstObjectByType<Africa3Manager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        MonumentScript monumentScript = eventData.pointerDrag.GetComponent<MonumentScript>();
        manager.StartCoroutine(manager.CheckAnswer(monumentScript,this.gameObject.name,manager.canDrop));
        
    }
}
