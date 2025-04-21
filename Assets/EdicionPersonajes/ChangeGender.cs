using UnityEngine;

public class ChangeGender : MonoBehaviour
{
    public GameObject pjMaculino;
    public GameObject pjFemenino;

    public void ActivarPjMasculino()
    {
        pjMaculino.SetActive(true);
        pjFemenino.SetActive(false);
    }

    public void ActivarPjFemenino()
    {
        pjMaculino.SetActive(false);
        pjFemenino.SetActive(true);
    }
}
