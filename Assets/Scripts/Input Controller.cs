using System.Buffers;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class InputController : MonoBehaviour
{
    
    public LayerMask mask;
    public Transform target;  
    public float maxDistance;
    public float minDistance;
    public float panSpeed;
    public float zoomDampening = 5.0f;
    private float rotationSpeed;
    float distance;
    float min, max;
    public float xDeg, yDeg;
    private GameObject activePanel;
    private float currentDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
            Vector3 position;
    public Dictionary<string, Vector2> positions;

    void Start()
    {
         
        positions = new Dictionary<string, Vector2>
    {
        { "AmericaCentral", new Vector2(53, 352) },
        { "AmericaNorte", new Vector2(51, 21) },
        { "AmericaSur", new Vector2(39, 317) },
        { "Antartida", new Vector2(106, 274) },
        { "Asia", new Vector2(266, 28) },
        { "ContinenteAfricano", new Vector2(314, 352) },
        { "ContinenteEuropeo", new Vector2(326, 25) },
        { "Oceania", new Vector2(199, 329) }
    };

        max = 20f;
        min = 10f;
        rotationSpeed = 7f;
        GetInitialValues();
        activePanel = null;
        maxDistance = 20;
        minDistance = .6f;
        panSpeed = 0.3f;
        zoomDampening = 5.0f;

    }
    /// <summ
    public void GetInitialValues()
    {
       
        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;


        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }

    void Update()
    {
        
        if (activePanel != null) 
        {

            activePanel.transform.LookAt(Camera.main.transform);
        }

       if (Input.touchCount == 1  && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Input.GetTouch(0).tapCount == 1 && activePanel != null) 
            {
                //desaparece el panel
                activePanel.SetActive(false);
                activePanel= null;
            }
            GoToPosition();
        }
        
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            

            Vector2 touchposition = Input.GetTouch(0).deltaPosition;
            xDeg += touchposition.x * rotationSpeed * 0.02f;
            yDeg -= touchposition.y * rotationSpeed * 0.02f;
            yDeg = ResetAngle(yDeg);
            xDeg = ResetAngle(xDeg);
        }
        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
        currentRotation = transform.rotation;
        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = rotation;

        // currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);
        position = target.position - (rotation * Vector3.forward * currentDistance);
        transform.position = position;


        if (Input.touchCount == 2)
        {
            Zoom();
        }
    }
    /// <summary>
    /// Se encarga del zoom de la camara.
    /// </summary>
    void Zoom()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);
        Vector2 firstTouchPos = firstTouch.position - firstTouch.deltaPosition;
        Vector2 secondTouchPos = secondTouch.position - secondTouch.deltaPosition;

        float prevMagnitude = (firstTouchPos - secondTouchPos).magnitude;
        float currentMagnitude = (firstTouch.position - secondTouch.position).magnitude;
        float difference = currentMagnitude - prevMagnitude;



        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - difference * 0.01f, min, max);
    }
    /// <summary>
    /// Mantiene el angulo de la camara entre 0 y 360.
    /// </summary>

    private float ResetAngle(float angle)
    {
        if (angle > 360)
            angle = 0;
        if (angle < 0)
            angle = 360;
        return angle;
    }
    /// <summary>
    /// LLeva la camara a la posicion de un continente.
    /// </summary>
    void GoToPosition()
    {
        Touch firstTouch = Input.GetTouch(0);
        Ray ray = Camera.main.ScreenPointToRay(firstTouch.position);
        if (Physics.Raycast(ray, out RaycastHit hit,mask))
        {
            Debug.Log(hit.collider.name);
            if (positions.ContainsKey(hit.collider.name))
            {
                xDeg = positions[hit.collider.name].x;
                yDeg = positions[hit.collider.name].y;
                //activePanel?.SetActive(false);
                //activePanel = hit.collider.transform.GetChild(0).gameObject;
                //activePanel.SetActive(true);
            }

        }
        //else 
        //{
        //    if(activePanel != null)
        //    {
        //        activePanel.SetActive(false);
        //        activePanel = null;
        //    }
        //}
    }
    //GameObject checkPanel() 
    //{
    //    return null;
    //}
}
