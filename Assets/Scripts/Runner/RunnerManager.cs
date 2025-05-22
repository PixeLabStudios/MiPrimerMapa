using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public RunnerScript1 runnerScript1; 
    float time;
    bool gameOver;
    public void Start()
    {
        gameOver = false;
        time = 0;
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            runnerScript1.ChangeSpeed(0);
            Debug.Log("gano");
        }
    }
}
