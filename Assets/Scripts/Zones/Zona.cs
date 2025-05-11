using System.Collections.Generic;

[System.Serializable]
public class Zona
{
    public int id;
    public string nombre;
    public string descripcion;
    public List<string> imagenes;
    public string video;
    public string narracion;
}

[System.Serializable]
public class ZonaData
{
    public List<Zona> zonas;
}
