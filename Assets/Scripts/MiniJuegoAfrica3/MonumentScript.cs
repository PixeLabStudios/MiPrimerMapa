using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonumentScript : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    public Canvas canvas;
    public Image image;
    public TextMeshProUGUI nameText;
    public Monument monument;
    bool candrag;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        candrag = true;
        startPosition = rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (candrag)
        {
            canvasGroup.alpha = 0.6f; // Make the object semi-transparent while dragging
            canvasGroup.blocksRaycasts = false; // Allow raycasts to pass through the object while dragging
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (candrag) 
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        rectTransform.anchoredPosition = startPosition;
        canvasGroup.alpha = 1f; // Reset the transparency
        canvasGroup.blocksRaycasts = true; // Re-enable raycasts
    }
    public void LoadData()
    {
        image.sprite = monument.monumentImage;
        nameText.text = monument.monumentName;


    }
    public void SetCanDrag(bool b) 
    {
        candrag = b;
    }

    
}
