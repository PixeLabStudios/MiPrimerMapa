using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EscalarElemento : MonoBehaviour
{
    public enum ModoFuncion
    {
        ToggleEscalaExclusiva,
        AnimacionClickUnica
    }

    [Header("Botones múltiples (lista)")]
    public List<Button> botones;

    [Header("Botón individual")]
    public Button botonUnico;

    [Header("Modo de comportamiento")]
    public ModoFuncion modoFuncion;

    [Header("Escalas")]
    public Vector3 escalaActiva = new Vector3(1.2f, 1.2f, 1f);
    public Vector3 escalaNormal = new Vector3(1f, 1f, 1f);
    public float velocidadEscalado = 10f;

    private Dictionary<Button, Vector3> escalaObjetivo = new Dictionary<Button, Vector3>();

    private Button botonActualmenteEscalado = null;

    private void Start()
    {
        if(botonUnico == null)
        {
            botonUnico = this.gameObject.GetComponent<Button>();
        }
        
        // Setup para botones múltiples (modo lista)
        foreach (Button btn in botones)
        {
            escalaObjetivo[btn] = escalaNormal;
            Button current = btn;
            btn.onClick.AddListener(() => EscalarBoton(current));
        }

        // Setup para el botón individual
        if (botonUnico != null)
        {
            if (modoFuncion == ModoFuncion.ToggleEscalaExclusiva)
            {
                botonUnico.onClick.AddListener(() => ToggleEscalaExclusiva(botonUnico));
            }
            else if (modoFuncion == ModoFuncion.AnimacionClickUnica)
            {
                botonUnico.onClick.AddListener(() => StartCoroutine(AnimacionClickUnica(botonUnico)));
            }
        }
    }

    private void Update()
    {
        // Escalado suave para los botones en lista
        foreach (var kvp in escalaObjetivo)
        {
            Button btn = kvp.Key;
            Transform t = btn.transform;
            Vector3 actual = t.localScale;
            Vector3 destino = kvp.Value;
            t.localScale = Vector3.Lerp(actual, destino, Time.deltaTime * velocidadEscalado);
        }
    }

    //  Modo exclusivo — toggle con animación suave
    public void ToggleEscalaExclusiva(Button boton)
    {
        if (botonActualmenteEscalado == boton)
        {
            // Ya estaba escalado → resetear
            boton.transform.localScale = escalaNormal;
            botonActualmenteEscalado = null;
        }
        else
        {
            // Resetear el anterior (si existe)
            if (botonActualmenteEscalado != null)
            {
                botonActualmenteEscalado.transform.localScale = escalaNormal;
            }

            // Escalar el nuevo
            boton.transform.localScale = escalaActiva;
            botonActualmenteEscalado = boton;
        }
    }
    public void cambiaralfa(float nuevoAlfa)
    {
        Color colorActual = botonUnico.image.color;
        colorActual.a = nuevoAlfa;
        botonUnico.image.color = colorActual;
    }

    // Animación rápida (tipo rebote)
    public IEnumerator AnimacionClickUnica(Button boton)
    {
        Transform t = boton.transform;
        Vector3 original = t.localScale;
        Vector3 pico = escalaActiva;

        float duracion = 0.15f;
        float tiempo = 0f;

        // Escala hacia arriba
        while (tiempo < duracion)
        {
            t.localScale = Vector3.Lerp(original, pico, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        t.localScale = pico;

        // Regreso
        tiempo = 0f;
        while (tiempo < duracion)
        {
            t.localScale = Vector3.Lerp(pico, original, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        t.localScale = original;
    }

    // Escalado para lista de botones múltiples (opcional)
    public void EscalarBoton(Button botonSeleccionado)
    {
        foreach (Button btn in botones)
        {
            if (btn == botonSeleccionado)
            {
                escalaObjetivo[btn] = escalaActiva;
                SetAlpha(btn, 1f); // Totalmente visible
            }
            else
            {
                escalaObjetivo[btn] = escalaNormal;
                SetAlpha(btn, 0.5f); // Transparencia al 50%
            }
        }
    }
    private void SetAlpha(Button btn, float alpha)
    {
        if (btn.image != null)
        {
            Color color = btn.image.color;
            color.a = alpha;
            btn.image.color = color;
        }
    }


}