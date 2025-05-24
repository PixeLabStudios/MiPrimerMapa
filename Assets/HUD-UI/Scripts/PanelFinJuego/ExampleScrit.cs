using UnityEngine;

public class ExampleScrit : MonoBehaviour
{

    //en teoria todo esto deberia estar para usar el script de estrellas
    //el puntaje dado deberia ser entre 0 y 120 ya que suman de media estrellas se aplica un limitador que redondea los valores entre estos numeros 
    //por ejemplo
    // primera media estrella 10-30 
    //seginda media estrella 30-50
    //tercera media estrella 50-70
    //cuarta media estrella 70-90
    //quinta media estrella 90-110
    //sexta media estrella 110-120

    //dado esto se me ocurre que podria usarse un calculo del porcentaje de aciertos y fallos 

    public int totalPreguntas; // Total de preguntas = aciertos + fallos
    public int aciertos;

    // Referencia al script StarScoreDisplay
    public StarScoreDisplay starScoreDisplay;
    public float score; // Variable para almacenar el puntaje
    private void Start()
    {
        // Obtener la referencia al script StarScoreDisplay
    }


    public void llamadoEstrellas()

    {
        //el porcentraje se calularia de esta manera
        float porcentaje = (float)aciertos / totalPreguntas * 120f;

        //--------------->obvio esta la opcion de que asignes los valores fijos con la siguiente liunea
        //starScoreDisplay.ShowStars(score);

        // Llamar a la función ShowStars del script StarScoreDisplay
        starScoreDisplay.ShowStars(porcentaje);

    }
}
