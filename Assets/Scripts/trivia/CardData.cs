using System;
using UnityEngine;

[System.Serializable]
public class CardData
{
   public string name; //nombre de la carta "la gran muralla de china" ,El taj Mahal, etc.
   public string description; //descripción de la carta "es una muralla que se construyó en china" , "es un monumento que se construyó en la india", blablabla.
   public string imagePath; //ruta de la imagen de la carta que se usara en la carta.
   public Question[] questions; //preguntas que se le harán al jugador sobre la carta,

    public CardData(string name, string description, string imagePath,Question[] questions)
    {
        this.name = name;
        this.description = description;
        this.imagePath = imagePath;
        this.questions = questions;
    }
}
