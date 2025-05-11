using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    //public Transform puntoInicial;
    //public GameObject personaje;
    //public GameObject personajeInicial;
    //public Transform escala;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //personajeInicial.SetActive(false);
        /*if (personaje == null)
            personaje = GameObject.FindGameObjectWithTag("Player");
        puntoInicial = GameObject.FindGameObjectWithTag("PosInicial").transform;
        MoverAPuntoInicial();*/
    }

    // Update is called once per frame
    void Update()
    {
        //CambioDeEscena();
    }

    public void MoverAPuntoInicial()
    {
        //personaje.transform.position = puntoInicial.position;
    }

    public void CambioDeEscena(int idEscena)
    {
       /* if (idEscena == 0)
        {
            personaje.SetActive(false);
        }
        else 
            personaje.SetActive(true);

        if (idEscena == 2)
        {
            //personajeInicial = GameObject.FindGameObjectWithTag("player");
            //personajeInicial.SetActive(false);
            personaje.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }
        else
            personaje.transform.localScale = Vector3.one;/*/
        SceneManager.LoadScene(idEscena);
    }
}
