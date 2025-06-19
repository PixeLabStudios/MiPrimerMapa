using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    AudioGameManager audioGameManager; // Reference to the AudioGameManager script
    public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    public Image  image;

    public Sprite flagImage;
    public string flagName;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        audioGameManager = FindFirstObjectByType<AudioGameManager>(); // Find the AudioGameManager in the scene

    }
    private void Start()
    {
        startPosition = rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (audioGameManager.candrag)
        {
            Debug.Log("Begin Drag on: " + gameObject.name);
            canvasGroup.alpha = 0.6f; // Make the object semi-transparent while dragging
            canvasGroup.blocksRaycasts = false; // Allow raycasts to pass through the object while dragging
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(!audioGameManager.candrag) return; // If dragging is not allowed, do nothing
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
       rectTransform.anchoredPosition = startPosition;
       canvasGroup.alpha = 1f; // Reset the transparency
       canvasGroup.blocksRaycasts = true; // Re-enable raycasts
    }
    public void ChangeImage(Sprite sprite)
    {
      image.sprite = sprite;
        flagImage = sprite; 
    }

}
