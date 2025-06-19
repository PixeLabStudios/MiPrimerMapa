using UnityEngine;

public class Base :MonoBehaviour
{
  public GameObject flag;
  public int id;
  public string BaseName;

    private void Start()
    {
        flag.SetActive(false);
    }

    public void ShowFlag()
    {
        flag.SetActive(true);
    }
}
