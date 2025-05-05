
using System.Collections.Generic;

[System.Serializable]
public class Zona
{
    public int id;
    public string nombre;
    public string descripcion;
}

[System.Serializable]
public class ZonaData
{
    public List<Zona> zonas;
}