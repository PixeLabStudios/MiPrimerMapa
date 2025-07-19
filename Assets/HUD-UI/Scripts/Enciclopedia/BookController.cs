using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

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
    public string iconPath;
    public string audioPath;
    public List<PageData> pages;
}

public class BookController : MonoBehaviour
{
    [Header("UI de contenido")]
    public GameObject pagePrefab;
    public Transform pageContainer;
    public BookPageNavigator navigator;

    [Header("UI de botones de capítulo")]
    public Transform chapterButtonContainer;

    [Header("Datos")]
    public TextAsset jsonFile;

    [Header("Audio")]
    public AudioSource narradorAudioSource;

    private List<ChapterData> chapters;

    void Start()
    {
        // Cargar los datos del JSON
        Debug.Log("JSON Raw: " + jsonFile.text);
        chapters = JsonUtilityWrapper.LoadChapters(jsonFile.text);
        Debug.Log("Capítulos cargados: " + chapters.Count);
        Debug.Log("Primer título: " + chapters[0].pages[0].title);

        // Configurar los botones de capítulo
        for (int i = 0; i < chapterButtonContainer.childCount && i < chapters.Count; i++)
        {
            int chapterIndex = i;
            Button btn = chapterButtonContainer.GetChild(i).GetComponent<Button>();

            // Configurar contenido visual del botón
            var label = btn.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
            if (label != null)
                label.text = chapters[i].chapterName;

            var icon = btn.transform.Find("Icon")?.GetComponent<Image>();
            if (icon != null && !string.IsNullOrEmpty(chapters[i].iconPath))
            {
                Sprite s = Resources.Load<Sprite>(chapters[i].iconPath);
                icon.sprite = s;
                icon.gameObject.SetActive(true);
            }

            // Configurar comportamiento del botón
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                LoadChapter(chapterIndex);
                ReproducirNarracion(chapterIndex);
            });
        }

        // Cargar el primer capítulo por defecto (sin reproducir audio)
        LoadChapter(0);
    }

    void LoadChapter(int index)
    {
        // Eliminar páginas anteriores
        foreach (Transform child in pageContainer)
            Destroy(child.gameObject);

        List<GameObject> chapterPages = new List<GameObject>();

        foreach (var pageData in chapters[index].pages)
        {
            GameObject page = Instantiate(pagePrefab, pageContainer);

            var titleT = page.transform.Find("Title");
            var bodyT = page.transform.Find("Body");
            var imgT = page.transform.Find("Illustration");

            if (titleT == null) Debug.LogError("Falta 'Title' en el prefab");
            if (bodyT == null) Debug.LogError("Falta 'Body' en el prefab");
            if (imgT == null) Debug.LogError("Falta 'Illustration' en el prefab");

            titleT?.GetComponent<TextMeshProUGUI>().SetText(pageData.title);
            bodyT?.GetComponent<TextMeshProUGUI>().SetText(pageData.text);

            if (!string.IsNullOrEmpty(pageData.imagePath))
            {
                Sprite sprite = Resources.Load<Sprite>(pageData.imagePath);
                Image img = imgT?.GetComponent<Image>();
                if (sprite != null && img != null)
                {
                    img.sprite = sprite;
                    img.gameObject.SetActive(true);
                }
            }

            page.SetActive(false);
            chapterPages.Add(page);
        }

        navigator.SetPages(chapterPages);
    }

    public void ReproducirNarracion(int index)
    {
        if (!string.IsNullOrEmpty(chapters[index].audioPath) && narradorAudioSource != null)
        {
            AudioClip narracion = Resources.Load<AudioClip>(chapters[index].audioPath);
            if (narracion != null)
            {
                narradorAudioSource.Stop();
                narradorAudioSource.clip = narracion;
                narradorAudioSource.Play();
            }
            else
            {
                Debug.LogWarning("No se encontró el audio en: " + chapters[index].audioPath);
            }
        }
    }
}
