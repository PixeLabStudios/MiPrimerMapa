using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pasword_Imput : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    public GameObject texto_Correcto;
    public GameObject texto_Incorrecto;
    public Button[] botonesNumericos;

    private string contraseñaCorrecta = "14000000";
    private int maxDigitos = 8;

    //Sonidos
    /*public AudioClip sonidoBoton;
    public AudioClip sonidoBorrar;
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
     */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LimpiarEntrada();
    }

    public void AgregarDigito(string digito)
    {
        if (inputText.text.Length >= maxDigitos) return;

        inputText.text += digito;
        //ReproducirSonido(sonidoBoton);

        if (inputText.text.Length >= maxDigitos)
        {
            SetBotonesNumericosInteractables(false);
        }
    }

    public void BorrarUltimoDigito()
    {
        if (inputText.text.Length > 0)
        {
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
            //ReproducirSonido(sonidoBorrar);

            if (inputText.text.Length < maxDigitos)
            {
                SetBotonesNumericosInteractables(true);
            }
        }
    }

    public void VerificarContraseña()
    {
        bool esCorrecta = inputText.text == contraseñaCorrecta;

        texto_Correcto.SetActive(esCorrecta);
        texto_Incorrecto.SetActive(!esCorrecta);

        //Dejoesto para agregar más acciones si la contraseña es correcta o incorrecta
        //ReproducirSonido(esCorrecta ? sonidoCorrecto : sonidoIncorrecto);
        //o tambien
        if (esCorrecta)
        {
            //reproducir un sonido
        }
        else
        {
            // explotar bomba
        }

        StartCoroutine(ReiniciarDespuesDe(3f));
    }

    private void SetBotonesNumericosInteractables(bool estado)
    {
        foreach (Button boton in botonesNumericos)
        {
            boton.interactable = estado;
        }
    }

    private void LimpiarEntrada()
    {
        inputText.text = "";
        SetBotonesNumericosInteractables(true);
        texto_Correcto.SetActive(false);
        texto_Incorrecto.SetActive(false);
    }

    private IEnumerator ReiniciarDespuesDe(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        LimpiarEntrada();
    }

    /*
    private void ReproducirSonido(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }*/
}
