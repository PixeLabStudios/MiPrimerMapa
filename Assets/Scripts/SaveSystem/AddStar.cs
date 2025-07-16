using UnityEngine;

public class AddStar : MonoBehaviour
{
    public StarCallExample starCallExample;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onclick() 
    {
        starCallExample.starsAsPoints++;
    }
    public void DebugJson()
    {
        GameProgressManager.Instance.PrintProgressJson();
    }
}
