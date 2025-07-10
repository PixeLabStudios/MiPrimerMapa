using UnityEngine;

public class ChangeGender : MonoBehaviour
{
    public GameObject pjMaculino;
    public GameObject pjFemenino;

    public void ActivarPjMasculino()
    {
        pjMaculino.SetActive(true);
        pjFemenino.SetActive(false);
        Singleton.Instance.isMan = true;
    }

    public void ActivarPjFemenino()
    {
        pjMaculino.SetActive(false);
        pjFemenino.SetActive(true);
        Singleton.Instance.isMan = false;
    }
}
