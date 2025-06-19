using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ChangeColorBack : MonoBehaviour
{
    public List<Color> colores = new List<Color>();

    public void cambiarcolor(int a) 
    {
        Camera.main.backgroundColor = colores[a];
    }

}
