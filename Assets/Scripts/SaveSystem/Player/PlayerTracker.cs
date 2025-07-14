using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public string currentBoardId = "board_1";

    void Start()
    {
        Vector3? savedPosition = GameProgressManager.Instance.GetSavedPlayerPosition(currentBoardId);
        if (savedPosition.HasValue)
        {
            transform.position = savedPosition.Value;
        }
    }

    void Update()
    {
        GameProgressManager.Instance.SavePlayerPosition(currentBoardId, transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MinigamePoint"))
        {
            string miniId = other.name;
            GameProgressManager.Instance.UpdateMiniGame(currentBoardId, miniId, 0, true);
        }
    }
}
