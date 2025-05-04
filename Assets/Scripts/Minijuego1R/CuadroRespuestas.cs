using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuadroRespuestas : MonoBehaviour, IDropHandler
{
    public string correctText;
    public GameManager gameManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            //Debug.Log("QQQQQQQQ");
            LabelArrastrable dragScript = dropped.GetComponent<LabelArrastrable>();
            TextMeshProUGUI droppedText = dropped.GetComponent<TextMeshProUGUI>();
            if (dragScript != null && droppedText != null)
            {
                //Debug.Log("ppppppp");
                if (droppedText.text == correctText)
                {
                    //Debug.Log("Y");
                    dropped.transform.position = transform.position;
                    //dropped.GetComponent<LabelArrastrable>().enabled = false;
                    dragScript.casillaCorrect = true;
                    dragScript.enabled = false;
                    gameManager.RegisterCorrectDrop();
                }
                else
                {
                    //Debug.Log("Noooo");
                    //dropped.GetComponent<LabelArrastrable>().ResetPosition();
                    gameManager.RegisterError();
                    dragScript.casillaCorrect = false;
                }
            }
            else
            {
                Debug.Log("text null");
            }
        }
    }
}
