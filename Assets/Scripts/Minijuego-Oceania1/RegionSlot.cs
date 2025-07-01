using UnityEngine;
using UnityEngine.EventSystems;

public class RegionSlot : MonoBehaviour, IDropHandler
{
    Oceania1Manager manager;
    private void Awake()
    {
        manager = FindFirstObjectByType<Oceania1Manager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        AnimalsDrag script = eventData.pointerDrag.GetComponent<AnimalsDrag>();
        manager.StartCoroutine(manager.CheckAnswer(script, this.gameObject.name, manager.canDrop));

    }
}
