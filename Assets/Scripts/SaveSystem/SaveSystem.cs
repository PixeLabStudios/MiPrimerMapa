using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void SaveProgress(PlayerProgress data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static PlayerProgress LoadProgress()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            Debug.Log("Cargando progreso existente:\n" + json);
            return JsonUtility.FromJson<PlayerProgress>(json);
        }
        else
        {
            TextAsset defaultJson = Resources.Load<TextAsset>("DefaultProgress");
            if (defaultJson != null)
            {
                File.WriteAllText(SavePath, defaultJson.text);
                Debug.Log("Creando progreso por defecto:\n" + defaultJson.text);
                return JsonUtility.FromJson<PlayerProgress>(defaultJson.text);
            }
        }
        return new PlayerProgress();
    }

    public static void DeleteProgress()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Progreso borrado.");
        }
    }

    public static void ResetToDefault()
    {
        TextAsset defaultJson = Resources.Load<TextAsset>("DefaultProgress");
        if (defaultJson != null)
        {
            File.WriteAllText(SavePath, defaultJson.text);
            Debug.Log("Progreso reiniciado al estado por defecto.");
        }
    }
}
