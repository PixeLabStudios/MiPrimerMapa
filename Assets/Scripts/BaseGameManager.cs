using UnityEngine;

public abstract class BaseGameManager : MonoBehaviour
{
    public virtual void PauseGame()
    {
        Time.timeScale = 0f; // Pausa el juego
        // si hay que hacer algo mas se puede sobreescribir en las clases hijas
    }
    public virtual void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el juego
        // si hay que hacer algo mas se puede sobreescribir en las clases hijas
    }
}
