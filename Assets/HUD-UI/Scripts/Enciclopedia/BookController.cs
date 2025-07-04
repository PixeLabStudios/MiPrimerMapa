using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class PageData
{
    public string title;
    public string text;
    public string imagePath;
}

[System.Serializable]
public class ChapterData
{
    public string chapterName;
    public List<PageData> pages;
}

public class BookController : MonoBehaviour
{
    public GameObject pagePrefab;
    public Transform pageContainer;
    public BookPageNavigator navigator;
    public TextAsset jsonFile;
    public Transform chapterButtonContainer;

    private List<ChapterData> chapters;

    void Start()
    {
        chapters = JsonUtilityWrapper.LoadChapters(jsonFile.text);

        // Asignar botones de capítulo desde UI
        for (int i = 0; i < chapterButtonContainer.childCount; i++)
        {
            int chapterIndex = i;
            Button btn = chapterButtonContainer.GetChild(i).GetComponent<Button>();
            btn.onClick.AddListener(() => LoadChapter(chapterIndex));
        }

        LoadChapter(0); // Cargar el primer capítulo por defecto
    }

    void LoadChapter(int index)
    {
        foreach (Transform child in pageContainer)
            Destroy(child.gameObject);

        List<GameObject> chapterPages = new List<GameObject>();
        foreach (var pageData in chapters[index].pages)
        {
            GameObject page = Instantiate(pagePrefab, pageContainer);
            page.transform.Find("Title").GetComponent<Text>().text = pageData.title;
            page.transform.Find("Body").GetComponent<Text>().text = pageData.text;

            Image img = page.transform.Find("Illustration")?.GetComponent<Image>();
            if (!string.IsNullOrEmpty(pageData.imagePath) && img != null)
            {
                Sprite s = Resources.Load<Sprite>(pageData.imagePath);
                img.sprite = s;
                img.gameObject.SetActive(true);
            }

            page.SetActive(false);
            chapterPages.Add(page);
        }

        navigator.SetPages(chapterPages);
    }
}
