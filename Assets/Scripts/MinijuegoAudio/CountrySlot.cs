using UnityEngine;
using UnityEngine.EventSystems;

public class CountrySlot : MonoBehaviour, IDropHandler
{

    AudioGameManager audioGameManager;
    private void Awake()
    {
        audioGameManager= FindFirstObjectByType<AudioGameManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        
        string flagName = eventData.pointerDrag.GetComponent<DragAndDrop>().flagName;
        audioGameManager.StartCoroutine(audioGameManager.CheckAnswer(gameObject.name,flagName));
        Debug.Log(flagName +" dejo "+ gameObject.name);
    }

    
}
