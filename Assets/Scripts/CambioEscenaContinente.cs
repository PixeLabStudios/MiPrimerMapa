using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaContinente : MonoBehaviour
{
    public SelectLevel currentCont;
     
    public void CambiarSceneCont()
    {
        if (currentCont.namesContinentes[currentCont.currentContinente]=="Antartida")
        {
            Invoke("CambioConRetraso", 0.5f);
        }
        else
        {
            Invoke("CambioProvisorio", 0.5f);
        }
        
        
    }

    public void CambioConRetraso()
    {
        Debug.Log(currentCont.namesContinentes[currentCont.currentContinente]);
        SceneManager.LoadScene(currentCont.namesContinentes[currentCont.currentContinente]);
    }

    public void CambioProvisorio()
    {
        //Debug.Log(currentCont.namesContinentes[currentCont.currentContinente]);
        SceneManager.LoadScene(2);
    }

}
