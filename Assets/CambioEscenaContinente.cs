using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaContinente : MonoBehaviour
{
    public SelectLevel currentCont;
    
    public void CambiarSceneCont()
    {
        Invoke("CambioConRetraso", 0.5f);
    }

    public void CambioConRetraso()
    {
        Debug.Log(currentCont.namesContinentes[currentCont.currentContinente]);
        SceneManager.LoadScene(currentCont.namesContinentes[currentCont.currentContinente]);


    }
}
