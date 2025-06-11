using UnityEngine;

public class ObjetoMiraCamara : MonoBehaviour
{
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
