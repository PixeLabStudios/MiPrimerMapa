using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEngine;

public class TriviaCard : Card
{
    public CardData data; // guarda todos los datos de la carta, como el nombre, descripcion, imagen y preguntas;
    public string cardName; // el nombre de la carta, que se mostrara en la UI
    TriviaManager manager; // el manager de trivia que contiene la logica del juego y las preguntas y coso

    private void Start()
    {
        manager = FindFirstObjectByType<TriviaManager>();
    }

    

}
