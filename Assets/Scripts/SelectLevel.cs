using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectLevel : MonoBehaviour
{
    public InputController Controller;
    public List<string> continentes = new List<string>();
    private List<string> namesContinentes = new List<string>();
    private int currentContinente = 7;
    public TextMeshProUGUI nameContinente;
    public GameObject bloqueado;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        namesContinentes = new List<string>{ "ANTÁRTIDA", "AMÉRICA CENTRAL", "AMÉRICA DEL NORTE", "AMÉRICA DEL SUR", "ASIA", "ÁFRICA", "EUROPA", "OCEANÍA"};
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("esta en "+continentes[currentContinente]);
    }

    public void ContinenteLeft()
    {
        if (currentContinente > 0)
            currentContinente++;
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
        AnalizarBloqueado (currentContinente);
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
        //PROVISORIO; SIN LOGICA
        if (id == 0)
        {
            bloqueado.SetActive(false);
        }
        else bloqueado.SetActive(true);
    }
}
