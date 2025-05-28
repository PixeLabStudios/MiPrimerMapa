using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TriviaManager : MonoBehaviour
{
    public TriviaCard[] triviaCards;
    private SerializableList<CardData> dataList; //Recibe todos los datos de las cartas de trivia desde un json. Hay que asignales a cada carta,
    
    [System.Serializable]
    public class SerializableList<T>
    {
        public List<T> list;
    }
    void Start()
    {
        
        //string[] answers = new string[4] { "respuesta1", "respuesta2", "boeeeeeeeeeeeeeeee", "respuesta4" };
        //Question a= new Question("pregunta?",answers, "boeeeeeeeeeeeeeeee");      
        //Question b= new Question("pregunta2?",answers, "boeeeeeeeeeeeeeeee");
        //Question c = new Question("pregunta2?", answers, "boeeeeeeeeeeeeeeee");      
        //Question[] que = new Question[3] { a, b, c };
        //CardData nueva = new CardData("la gran muralla de china", "es una muralla que se construyó en china", "a",que);
        //CardData otra = new CardData("El Taj Mahal", "es un monumento que se construyó en la india", "a",que);
        //dataList.list.Add(nueva);
        //dataList.list.Add(otra);
        //string json = JsonUtility.ToJson(dataList,true);  
        //Debug.Log(json);
        //File.WriteAllText(Application.dataPath + "/Resources/JSON/DatosTrivia.json", json);


        //-----------//

        
        dataList =JsonUtility.FromJson<SerializableList<CardData>>(File.ReadAllText(Application.dataPath + "/Resources/JSON/DatosTrivia.json"));    
        CardData datos = dataList.list[0]; // Acceder al primer elemento de la lista

        LoadData();

    }
    void LoadData() 
    {

        //prueba con uno solo

        triviaCards[0].data = dataList.list[0]; // Asignar el primer elemento de la lista a la primera carta de trivia
        triviaCards[0].cardName = triviaCards[0].data.name; // Asignar el nombre de la carta a la variable cardName de TriviaCard
        Sprite sprite = Resources.Load<Sprite>(triviaCards[0].data.imagePath);
        Debug.Log(triviaCards[0].data.imagePath);
        triviaCards[0].ChangeImage(sprite); // Cargar la imagen de la carta desde Resources y asignarla a la carta de trivia
    }

    
}
