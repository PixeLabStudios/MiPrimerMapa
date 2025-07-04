using UnityEngine;

public class America2UI : MonoBehaviour
{
    public GameObject[] hearts;


    public void ShowAllHearts() 
    {
        foreach (GameObject heart in hearts)
        {
            heart.SetActive(true);
        }
    }
    public void HideHearts(int i)
    {
        hearts[i].SetActive(false);
    }
    public void ShowHeart(int i)
    {
        hearts[i - 1].SetActive(true);
    }
    
}
