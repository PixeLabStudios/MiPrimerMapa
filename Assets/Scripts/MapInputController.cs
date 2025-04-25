
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapInputController : MonoBehaviour
{
    
    public GameObject character;
    public List<GameObject> checkpointsPosition = new List<GameObject>();
    List<GameObject> checkpointsCleared;
    MoveCharacter moveCharacter;
    
    GameObject nextPoint;
    int index;
    
    private void Start()
    {
        checkpointsCleared = new List<GameObject>();
        moveCharacter = character.GetComponent<MoveCharacter>();
        int index = 0;
        if (checkpointsPosition.Count > 0)
        {
            nextPoint = checkpointsPosition[index];
        }
        else
        {
            Debug.LogError("No checkpoints set in the inspector");
        }
       
    }
    public void SetDestination(GameObject position) 
    {
        if (CheckIfPointIsCleared(position) || nextPoint == position) 
        {
            if (nextPoint == position)
            {    
                checkpointsCleared.Add(position);
                index++;
                index = Mathf.Clamp(index, 0, checkpointsPosition.Count - 1);
                nextPoint = checkpointsPosition[index];
            }                   
            moveCharacter.MoveTo(position.transform.position);
        }
    }
    /// <summary>
    ///  Revisa si un punto ya fue desbloqueado
    /// </summary>
    /// <param name="point"></param>
    /// <returns> un booleano </returns>
    bool CheckIfPointIsCleared(GameObject point)
    {
        foreach (GameObject checkpoint in checkpointsCleared)
        {
            if (checkpoint == point)
            {
                return true;
            }
        }
        return false;

    }
    
    //void Update()
    //{

    //    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        Touch firstTouch = Input.GetTouch(0);
    //        Ray ray = Camera.main.ScreenPointToRay(firstTouch.position);
    //        if (Physics.Raycast(ray,out RaycastHit hit,1000,pointMask)) 
    //        {

    //            if (CheckIfPointIsCleared(hit.transform) || nextPoint == hit.transform)
    //            {

    //                if (nextPoint == hit.transform)
    //                {
    //                    checkpointsCleared.Add(hit.transform);
    //                    index++;
    //                    index = Mathf.Clamp(index, 0, checkpointsPosition.Count - 1);                     
    //                    nextPoint = checkpointsPosition[index];
    //                }
    //                moveCharacter.MoveTo(hit.collider.transform.position);
    //                currentPoint = checkpointsPosition.IndexOf(hit.collider.gameObject);
    //                Debug.Log(currentPoint);
    //            }
    //            else 
    //            { 
    //                Debug.Log("This point is not cleared yet");
    //            }
                    
               
    //        }
            

    //    }
     //}
    
    
}

