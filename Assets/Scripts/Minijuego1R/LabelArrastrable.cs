using UnityEngine;
using UnityEngine.EventSystems;

public class LabelArrastrable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;
    public bool casillaCorrect = false;
    void Start()
    {
        originalPosition = transform.position;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        casillaCorrect = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!casillaCorrect)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
    }
}
