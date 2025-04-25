using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovimientoPengu : MonoBehaviour
{
    public Animator animator;
    public GameObject A;
    public GameObject B;
    public float contador; // Tiempo de espera en cada punto
    public NavMeshAgent pengu;
    public AudioSource audio;

    private Vector3 destinoActual;

    void Start()
    {
        destinoActual = B.transform.position;
        StartCoroutine(RutinaMovimiento());
    }

    IEnumerator RutinaMovimiento()
    {
        while (true)
        {
            pengu.SetDestination(destinoActual);
            animator.SetBool("Camina", true);

            while (Vector3.Distance(transform.position, destinoActual) > 0.5f)
            {
                yield return null;
            }
            animator.SetBool("Camina", false);
            audio.Play();

            yield return new WaitForSeconds(contador);

            if (destinoActual == B.transform.position)
                destinoActual = A.transform.position;
            else
                destinoActual = B.transform.position;
        }
    }
}
