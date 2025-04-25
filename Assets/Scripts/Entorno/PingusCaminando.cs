using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PingusCaminando: MonoBehaviour
{
    public List<Transform> puntos; // Arrastrá aquí los objetos A-J en orden desde el inspector
    public NavMeshAgent pengu;

    private int indiceDestino = 0;

    void Start()
    {
        if (puntos.Count == 0)
        {
            Debug.LogError("No hay puntos asignados al pingüino.");
            return;
        }

        StartCoroutine(MoverPingüino());
    }

    IEnumerator MoverPingüino()
    {
        while (true)
        {
            Vector3 destino = puntos[indiceDestino].position;
            pengu.SetDestination(destino);

            while (Vector3.Distance(transform.position, destino) > 0.5f)
            {
                // Activar animación de caminar si se está moviendo
                yield return null;
            }

            // Avanza al siguiente punto, vuelve a 0 si termina
            indiceDestino = (indiceDestino + 1) % puntos.Count;
        }
    }
}
