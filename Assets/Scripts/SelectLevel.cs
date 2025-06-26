using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public InputController Controller;

    [Header("Continentes disponibles")]
    public List<string> continentes = new List<string>();

    [Header("Nombres de Continentes")]
    private List<string> namesContinentes = new List<string>();

    [Header("ID de Continente Actual")]
    private int currentContinente;

    [Header("UI Referencias")]
    public TextMeshProUGUI nameContinente;
    public GameObject bloqueado; // Icono de candado
    public GameObject botonPlay; // Botón de jugar

    [Header("Continentes desbloqueados")]
    public List<int> continentesDesbloqueados = new List<int> { 0 }; // Puedes expandir desde el Inspector

    void Start()
    {
        namesContinentes = new List<string>
        {
            "ANTÁRTIDA", "AMÉRICA CENTRAL", "AMÉRICA DEL NORTE", "AMÉRICA DEL SUR",
            "ASIA", "ÁFRICA", "EUROPA", "OCEANÍA"
        };

        // Establecer nombre e icono inicial
        ChangeContinente(currentContinente);
        ChangeName(currentContinente);
        AnalizarBloqueado(currentContinente);
    }

    public void ContinenteLeft()
    {
        if (currentContinente > 0)
            currentContinente--;
        else
            currentContinente = continentes.Count - 1;

        ChangeContinente(currentContinente);
        ChangeName(currentContinente);
        AnalizarBloqueado(currentContinente);
    }

    public void ContinenteRight()
    {
        if (currentContinente < continentes.Count - 1)
            currentContinente++;
        else
            currentContinente = 0;

        ChangeContinente(currentContinente);
        ChangeName(currentContinente);
        AnalizarBloqueado(currentContinente);
    }

    void ChangeContinente(int continente)
    {
        if (Controller.positions.ContainsKey(continentes[continente]))
        {
            Controller.xDeg = Controller.positions[continentes[continente]].x;
            Controller.yDeg = Controller.positions[continentes[continente]].y;
        }
    }

    void ChangeName(int name)
    {
        nameContinente.text = namesContinentes[name];
    }

    void AnalizarBloqueado(int id)
    {
        bool estaDesbloqueado = continentesDesbloqueados.Contains(id);

        // Mostrar icono de candado si está bloqueado
        bloqueado.SetActive(!estaDesbloqueado);

        // Activar o desactivar botón Play
        Button boton = botonPlay.GetComponent<Button>();
        if (boton != null)
            boton.interactable = estaDesbloqueado;
    }

    // Método opcional para desbloquear continentes desde otros scripts
    public void DesbloquearContinente(int id)
    {
        if (!continentesDesbloqueados.Contains(id))
            continentesDesbloqueados.Add(id);
    }
}
