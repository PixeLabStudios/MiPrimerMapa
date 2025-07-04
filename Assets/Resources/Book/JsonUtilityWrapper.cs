using System.Collections.Generic;
using UnityEngine;

public static class JsonUtilityWrapper
{
    [System.Serializable]
    private class ChapterList { public List<ChapterData> chapters; }

    public static List<ChapterData> LoadChapters(string json)
    {
        return JsonUtility.FromJson<ChapterList>("{\"chapters\":" + json + "}").chapters;
    }
}
