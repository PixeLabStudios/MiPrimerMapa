using UnityEngine;

public class RotarACamara : MonoBehaviour
{
    public Camera camaraObjetivo;
    public Vector3 rotacionAdicional; 

    void Start()
    {
        if (camaraObjetivo == null)
        {
            camaraObjetivo = Camera.main;
        }
    }

    void LateUpdate()
    {

        Vector3 direccion = camaraObjetivo.transform.position - transform.position;
        Quaternion rotacionBase = Quaternion.LookRotation(-direccion.normalized);


        Quaternion rotacionExtra = Quaternion.Euler(rotacionAdicional);

        transform.rotation = rotacionBase * rotacionExtra;

    }

}
