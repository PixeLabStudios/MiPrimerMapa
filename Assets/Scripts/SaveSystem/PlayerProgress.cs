using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vector3Data
{
    public float x, y, z;
    public Vector3Data(Vector3 v) { x = v.x; y = v.y; z = v.z; }
    public Vector3 ToVector3() => new Vector3(x, y, z);
}

[Serializable]
public class MiniGameProgress
{
    public string miniGameId;
    public int stars;       // 0 to 6 (0.5 stars = 1 point)
    public bool unlocked;
}

[Serializable]
public class BoardProgress
{
    public string boardId;
    public int currentPosition;
    public Vector3Data lastPlayerPosition;
    public List<MiniGameProgress> miniGames = new();
}

[Serializable]
public class PlayerProgress
{
    public List<BoardProgress> boards = new();
}

