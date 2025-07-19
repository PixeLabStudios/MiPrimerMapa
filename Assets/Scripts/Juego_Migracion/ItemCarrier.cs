using UnityEngine;

public class ItemCarrier : MonoBehaviour
{
    public Transform carryPoint;
    //public GameObject carryButton;
    private GameObject carriedObject;

    public bool IsCarrying => carriedObject != null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // o desde botón UI
        {
            if (carriedObject)
            {
                DropItem();
            }
            else
            {
                TryPickup();
            }
        }
    }

    void TryPickup()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, 2f);
        foreach (var col in nearby)
        {
            if (col.CompareTag("Pickup") && carriedObject == null)
            {
                carriedObject = col.gameObject;
                carriedObject.GetComponent<Rigidbody>().isKinematic = true;
                carriedObject.transform.SetParent(carryPoint);
                carriedObject.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }

    void DropItem()
    {
        carriedObject.transform.SetParent(null);
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }

    public void BotonAgarrarObjeto()
    {
        if (carriedObject)
        {
            DropItem();
        }
        else
        {
            TryPickup();
        }
    }
}
