using UnityEngine;
using System.Collections.Generic;
using System;

public class DataCustom : MonoBehaviour
{
    public enum TypeCloth
    {
        HAIR,
        Cloth_COLOR,
        Cloth_COLOR2,
        SKIN_COLOR,
        //ACCESSORY,
        CHEST,
        LEGS,
        FEET
    }
    #region M
    public List<GameObject> hairStyles = new List<GameObject>();

    public List<Color> Colors = new List<Color>();
    public List<Material> skin = new List<Material>();

    public List<GameObject> clothesChest = new List<GameObject>();
    public List<GameObject> clothesLegs = new List<GameObject>();
    public List<GameObject> clothesFeet = new List<GameObject>();

    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;
    [HideInInspector]
    /*public GameObject currentHair;
    [HideInInspector] public GameObject currentAccessory;
    [HideInInspector] public GameObject currentChest;
    [HideInInspector] public GameObject currentLegs;
    [HideInInspector] public GameObject currentFeet;

    [HideInInspector] public GameObject currentCloth;
    [HideInInspector] public int currentObjeto;*/
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #region M
        foreach (var cloth in hairStyles)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        foreach (var cloth in clothesChest)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        foreach (var cloth in clothesFeet)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        foreach (var cloth in clothesLegs)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        #endregion
        SearchCloth();
    }

   /* void ApplySelection(CustomSystem.TypeCloth type, int index)
    {
        switch (type)
        {
            case CustomSystem.TypeCloth.HAIR:
                if (Singleton.Instance.currentHair != null)
                {
                    currentHair.SetActive(false);
                }
                currentHair = SearchCloth(type, index);
                currentCloth = currentHair;
                break;

            case CustomSystem.TypeCloth.CHEST:
                if (currentChest != null)
                {
                    currentChest.SetActive(false);
                }
                currentChest = SearchCloth(type, index);
                currentCloth = currentChest;
                break;

            case CustomSystem.TypeCloth.LEGS:
                if (currentLegs != null)
                {
                    currentLegs.SetActive(false);
                }
                currentLegs = SearchCloth(type, index);
                currentCloth = currentLegs;
                //currentObjeto = 2;
                break;

            case CustomSystem.TypeCloth.ACCESSORY:
                if (currentAccessory != null)
                {
                    currentAccessory.SetActive(false);
                }
                currentAccessory = SearchCloth(type, index);

                break;

            case CustomSystem.TypeCloth.FEET:
                if (currentFeet != null)
                {
                    currentFeet.SetActive(false);
                }
                currentFeet = SearchCloth(type, index);
                currentCloth = currentFeet;
                //currentObjeto = 3;
                break;

            case CustomSystem.TypeCloth.Cloth_COLOR:
                if (currentHair != null)
                {
                    currentHair.GetComponent<MeshRenderer>().material.color = Colors[index];
                }

                break;

            case CustomSystem.TypeCloth.Cloth_COLOR2:
                if (currentCloth != null)
                {
                    //currentCloth.GetComponent<MeshRenderer>().material.color = Colors[index];
                    currentCloth.GetComponent<SkinnedMeshRenderer>().material.color = Colors[index];
                }

                break;

            case CustomSystem.TypeCloth.SKIN_COLOR:
                meshRenderer.material = skin[index];

                //meshRendererF.material = skin[index];
                break;
        }
    }
    */
    public void SearchCloth()
    {
        for (int i = 0; i < hairStyles.Count; i++)
        {
            if (i == Singleton.Instance.hairIndex)
            {
                if (hairStyles[i] != null)
                {
                    hairStyles[i].SetActive(true);
                }
                hairStyles[i].GetComponent<SkinnedMeshRenderer>().material.color = Colors[Singleton.Instance.colorIndexHair];
            }
        }

        for (int i = 0; i < clothesChest.Count; i++)
        {
            if (i == Singleton.Instance.chestIndex)
            {
                if (clothesChest[i] != null)
                {
                    clothesChest[i].SetActive(true);
                }
                clothesChest[i].GetComponent<SkinnedMeshRenderer>().material.color = Colors[Singleton.Instance.colorIndexChest];
            }
        }

        for (int i = 0; i < clothesLegs.Count; i++)
        {
            if (i == Singleton.Instance.legsIndex)
            {
                if (clothesLegs[i] != null)
                {
                    clothesLegs[i].SetActive(true);
                }
                clothesLegs[i].GetComponent<SkinnedMeshRenderer>().material.color = Colors[Singleton.Instance.colorIndexLegs];
            }
        }

        for (int i = 0; i < clothesFeet.Count; i++)
        {
            if (i == Singleton.Instance.feetIndex)
            {
                if (clothesFeet[i] != null)
                {
                    clothesFeet[i].SetActive(true);
                }
                clothesFeet[i].GetComponent<SkinnedMeshRenderer>().material.color = Colors[Singleton.Instance.colorIndexFeet];
            }

        }

        meshRenderer.material = skin[Singleton.Instance.skinColorIndex];
    }
}
