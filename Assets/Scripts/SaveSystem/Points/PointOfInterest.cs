using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public string boardId = "board_1";
    public string miniGameId;
    public int orderIndex;
    public int requiredStarsToUnlock = 0;

    public GameObject lockedIcon;
    public GameObject unlockedIcon;
    public GameObject visualToAnimate;
    public float detectionRadius = 5f;

    private bool isUnlocked = false;
    private Transform player;

    void Start()
    {
        GameProgressManager.Instance.RegisterMinigame(boardId, miniGameId);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        var board = GameProgressManager.Instance.progress.boards.Find(b => b.boardId == boardId);

        if (orderIndex == 0)
        {
            isUnlocked = true;
        }
        else
        {
            string previousMiniId = $"minijuego_{orderIndex - 1}";
            var previous = board?.miniGames.Find(m => m.miniGameId == previousMiniId);
            isUnlocked = previous != null && previous.stars >= requiredStarsToUnlock;
        }

        UpdateIconState();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        Animate(dist < detectionRadius);
    }

    void UpdateIconState()
    {
        if (lockedIcon != null) lockedIcon.SetActive(!isUnlocked);
        if (unlockedIcon != null) unlockedIcon.SetActive(isUnlocked);
    }

    void Animate(bool isNear)
    {
        if (visualToAnimate == null) return;
        float scale = isNear ? 1.2f : 1f;
        visualToAnimate.transform.localScale = Vector3.Lerp(visualToAnimate.transform.localScale,
                                                            Vector3.one * scale,
                                                            Time.deltaTime * 3f);
    }

    public bool IsUnlocked() => isUnlocked;
}