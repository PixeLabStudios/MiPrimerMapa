using UnityEngine;
using UnityEngine.UI;

public class BookPageNavigator : MonoBehaviour
{
    public GameObject[] pages;           // Asigná las páginas desde el Inspector
    public Button leftButton;            // Botón para ir a la izquierda
    public Button rightButton;           // Botón para ir a la derecha

    private int currentPage = 0;

    void Start()
    {
        ShowPage(currentPage);

        // Asignar listeners a los botones
        leftButton.onClick.AddListener(PreviousPage);
        rightButton.onClick.AddListener(NextPage);
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }

    void PreviousPage()
    {
        currentPage--;
        if (currentPage < 0)
            currentPage = pages.Length - 1;

        ShowPage(currentPage);
    }

    void NextPage()
    {
        currentPage++;
        if (currentPage >= pages.Length)
            currentPage = 0;

        ShowPage(currentPage);
    }
}
