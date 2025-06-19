using System.Collections.Generic;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    public Flag[] flags;
    public Base[] bases;
    int    correctFlags;

    void Start()
    {
        correctFlags = 0; 
        AsociateFlags();
    }



    void AsociateFlags() 
    {
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i].id = i;
            bases[i].id = i;
        }
    }

    public bool CheckFlag(Flag a ,Base bases)
    {
        if (a.id == bases.id)
        {
            correctFlags++;
            bases.ShowFlag();
            Debug.Log("Bandera Correcta");
            if (correctFlags == flags.Length) 
            {
                Debug.Log("Ganaste");
            }
            return true;
        }
        else
        {
            Debug.Log("Bandera Incorrecta");
            return false;
        }
    }




}
