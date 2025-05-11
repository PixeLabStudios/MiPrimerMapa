using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;
    public int hairIndex = 0;
    public int colorIndex = 0;
    public int skinColorIndex = 0;
    public int accessoryIndex = 0;
    public int chestIndex = 0;
    public int legsIndex = 0;
    public int feetIndex = 0;

    public int colorIndexHair = 0;
    public int colorIndexChest = 0;
    public int colorIndexLegs = 0;
    public int colorIndexFeet = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
