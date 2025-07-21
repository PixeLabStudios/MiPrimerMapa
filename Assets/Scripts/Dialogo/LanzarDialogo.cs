using UnityEngine;

public class LanzarDialogo : MonoBehaviour
{
    public DialogoManager manager;
    public string nombreArchivo;

    void Start()
    {
        //TextAsset archivo = Resources.Load<TextAsset>(nombreArchivo);
        //manager.CargarDialogoDesdeJSON(archivo);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            TextAsset archivo = Resources.Load<TextAsset>(nombreArchivo);
            manager.CargarDialogoDesdeJSON(archivo);
        }
    }
}
