using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;
    public PlayerProgress progress;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            progress = SaveSystem.LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBoardPosition(string boardId, int position)
    {
        var board = GetOrCreateBoard(boardId);
        board.currentPosition = position;
        SaveSystem.SaveProgress(progress);
    }

    public void UpdateMiniGame(string boardId, string miniGameId, int stars, bool unlocked)
    {
        var board = GetOrCreateBoard(boardId);
        var mini = board.miniGames.Find(m => m.miniGameId == miniGameId);
        if (mini == null)
        {
            mini = new MiniGameProgress { miniGameId = miniGameId };
            board.miniGames.Add(mini);
        }
        mini.stars = Mathf.Max(mini.stars, stars);
        mini.unlocked = mini.unlocked || unlocked;
        SaveSystem.SaveProgress(progress);
    }

    public void SavePlayerPosition(string boardId, Vector3 position)
    {
        var board = GetOrCreateBoard(boardId);
        board.lastPlayerPosition = new Vector3Data(position);
        SaveSystem.SaveProgress(progress);
    }

    public Vector3? GetSavedPlayerPosition(string boardId)
    {
        var board = GetOrCreateBoard(boardId);
        return board.lastPlayerPosition?.ToVector3();
    }

    public void RegisterMinigame(string boardId, string miniGameId)
    {
        var board = GetOrCreateBoard(boardId);
        if (!board.miniGames.Exists(m => m.miniGameId == miniGameId))
        {
            board.miniGames.Add(new MiniGameProgress { miniGameId = miniGameId, stars = 0, unlocked = false });
        }
    }

    public void PrintProgressJson()
    {
        string json = JsonUtility.ToJson(progress, true);
        Debug.Log("Estado actual del progreso:\n" + json);
    }

    private BoardProgress GetOrCreateBoard(string boardId)
    {
        var board = progress.boards.Find(b => b.boardId == boardId);
        if (board == null)
        {
            board = new BoardProgress { boardId = boardId };
            progress.boards.Add(board);
        }
        return board;
    }
}