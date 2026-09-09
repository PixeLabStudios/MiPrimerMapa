using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestProgressUI : MonoBehaviour
{
    public Button btnAvanzar;
    public Button btnCompletar;
    public Button btnMostrar;
    public TMP_Text logText;

    private int pos = 0;

    void Start()
    {
        btnAvanzar.onClick.AddListener(Avanzar);
        btnCompletar.onClick.AddListener(CompletarMinijuego);
        btnMostrar.onClick.AddListener(MostrarProgreso);
    }

    void Avanzar()
    {
        pos++;
        GameProgressManager.Instance.SetBoardPosition("board_1", pos);
        logText.text = $"Avanzaste a la posición {pos}";
    }

    void CompletarMinijuego()
    {
        GameProgressManager.Instance.UpdateMiniGame("board_1", "minijuego_demo", 4, true); // 2 estrellas = 4
        logText.text = "Completaste minijuego con 2 estrellas";
    }

    void MostrarProgreso()
    {
        var board = GameProgressManager.Instance.progress.boards.Find(b => b.boardId == "board_1");
        if (board != null)
        {
            string info = $"Posición actual: {board.currentPosition}\n";
            foreach (var mini in board.miniGames)
            {
                info += $"Minijuego: {mini.miniGameId}, estrellas: {mini.stars / 2.0f}, unlocked: {mini.unlocked}\n";
            }
            logText.text = info;
        }
    }
}
