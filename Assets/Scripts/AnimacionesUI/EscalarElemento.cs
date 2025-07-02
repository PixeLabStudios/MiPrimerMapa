using System.Collections;
using System.Collections.Generic;
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
        if (botonUnico == null)
        {
            botonUnico = GetComponent<Button>();
        }

        foreach (Button btn in botones)
        {
            escalaObjetivo[btn] = escalaNormal;
            Button current = btn;
            btn.onClick.AddListener(() => EscalarBoton(current));
        }

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
        foreach (var kvp in escalaObjetivo)
        {
            Button btn = kvp.Key;
            Transform t = btn.transform;
            Vector3 actual = t.localScale;
            Vector3 destino = kvp.Value;
            t.localScale = Vector3.Lerp(actual, destino, Time.deltaTime * velocidadEscalado);
        }
    }

    public void ToggleEscalaExclusiva(Button boton)
    {
        if (botonActualmenteEscalado == boton)
        {
            boton.transform.localScale = escalaNormal;
            botonActualmenteEscalado = null;
        }
        else
        {
            if (botonActualmenteEscalado != null)
            {
                botonActualmenteEscalado.transform.localScale = escalaNormal;
            }

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

    public IEnumerator AnimacionClickUnica(Button boton)
    {
        yield return UIAnimator.ScaleBounce(boton.transform, escalaActiva, 0.15f);
        yield return UIAnimator.ScaleBounce(boton.transform, escalaNormal, 0.15f);
    }

    public void ParpadearBoton(Button boton)
    {
        if (boton.image != null)
        {
            StartCoroutine(UIAnimator.Flash(boton.image, Color.white, 0.5f));
        }
    }

    public void EscalarBoton(Button botonSeleccionado)
    {
        foreach (Button btn in botones)
        {
            if (btn == botonSeleccionado)
            {
                escalaObjetivo[btn] = escalaActiva;
                SetAlpha(btn, 1f);
            }
            else
            {
                escalaObjetivo[btn] = escalaNormal;
                SetAlpha(btn, 0.5f);
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
