using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Obtener entrada del teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Crear vector de movimiento
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Aplicar movimiento
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        if (Input.GetKeyDown(KeyCode.P))
        {
            //GameProgressManager.Instance.DebugPrintSave();
        }
    }
}
