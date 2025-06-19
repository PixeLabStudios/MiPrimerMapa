using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;
    #region M
    public int hairIndex = 0;
    public int colorIndex = 0;
    public int skinColorIndex = 0;
    //public int accessoryIndex = 0;
    public int chestIndex = 0;
    public int legsIndex = 0;
    public int feetIndex = 0;

    public int colorIndexHair = 0;
    public int colorIndexChest = 0;
    public int colorIndexLegs = 0;
    public int colorIndexFeet = 0;
    #endregion

    #region F
    public int hairIndexF = 0;
    public int colorIndexF = 0;
    public int skinColorIndexF = 0;
    //public int accessoryIndexF = 0;
    public int chestIndexF = 0;
    public int legsIndexF = 0;
    public int feetIndexF = 0;

    public int colorIndexHairF = 0;
    public int colorIndexChestF = 0;
    public int colorIndexLegsF = 0;
    public int colorIndexFeetF = 0;
    #endregion

    public bool isMan;

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
