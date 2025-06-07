using UnityEngine;

[System.Serializable]
public class AudioData
{
   public string audioPath; //ruta del audio que se usara en el minijuego de audio.
   public string name; //nombre del audio que se usara en el minijuego de audio.
   public string imagePath; //ruta de la imagen que se usara en el minijuego de audio.

    public AudioData(string audiopath, string name, string imagePath)
    {
        this.audioPath = audiopath;
        this.name = name;
        this.imagePath = imagePath;
    }
}
