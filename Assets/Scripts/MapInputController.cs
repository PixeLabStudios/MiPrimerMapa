
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapInputController : MonoBehaviour
{
    public LayerMask pointMask;
    public GameObject character;
    public List<Transform> checkpointsPosition = new List<Transform>();
    MoveCharacter moveCharacter; 
    List<Transform> checkpointsCleared = new List<Transform>();   
    Transform nextPoint;
    int index;
    private void Start()
    {
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
    bool CheckIfPointIsCleared(Transform point) 
    {
        foreach (Transform checkpoint in checkpointsCleared)
        {
            if (checkpoint == point)
            {
                return true;
            }
        }
        return false;
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch firstTouch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(firstTouch.position);
            if (Physics.Raycast(ray,out RaycastHit hit,1000,pointMask)) 
            {

                if (CheckIfPointIsCleared(hit.transform) || nextPoint == hit.transform)
                {

                    if (nextPoint == hit.transform)
                    {
                        checkpointsCleared.Add(hit.transform);
                        index++;
                        index = Mathf.Clamp(index, 0, checkpointsPosition.Count - 1);
                        Debug.Log(index);
                        nextPoint = checkpointsPosition[index];
                    }
                    moveCharacter.MoveTo(hit.collider.transform.position);


                }
                else 
                { 
                    Debug.Log("This point is not cleared yet");
                }
                    
               
            }
            

        }
     }
    
    
}

