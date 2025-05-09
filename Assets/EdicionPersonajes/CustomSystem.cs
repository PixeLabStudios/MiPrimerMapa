using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomSystem : MonoBehaviour
{
    //public Singleton singleton;
    public enum TypeCloth
    {
        HAIR,
        Cloth_COLOR,
        Cloth_COLOR2,
        SKIN_COLOR,
        ACCESSORY,
        CHEST,
        LEGS,
        FEET
    }
    public enum TypeClothF
    {
        HAIR,
        HAIR_COLOR,
        SKIN_COLOR,
        ACCESSORY,
        CHEST,
        LEGS,
        FEET
    }
    #region M
    public List<GameObject> hairStyles = new List<GameObject>();

    public List<Color> Colors = new List<Color>();
    //public List<Color> skinColors = new List<Color>();
    public List<Material> skin = new List<Material>();

    public List<GameObject> accessories = new List<GameObject>();

    public List<GameObject> clothesChest = new List<GameObject>();
    public List<GameObject> clothesLegs = new List<GameObject>();
    public List<GameObject> clothesFeet = new List<GameObject>();

    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;
    [HideInInspector]
    public GameObject currentHair;
    [HideInInspector] public GameObject currentAccessory;
    [HideInInspector] public GameObject currentChest;
    [HideInInspector] public GameObject currentLegs;
    [HideInInspector] public GameObject currentFeet;

    [HideInInspector] public GameObject currentCloth;
    [HideInInspector] public int currentObjeto;
    #endregion

    #region F
    public List<GameObject> hairStylesF = new List<GameObject>();

    //public List<Color> hairColorsF = new List<Color>();
    //public List<Color> skinColorsF = new List<Color>();

    public List<GameObject> accessoriesF = new List<GameObject>();

    public List<GameObject> clothesChestF = new List<GameObject>();
    public List<GameObject> clothesLegsF = new List<GameObject>();
    public List<GameObject> clothesFeetF = new List<GameObject>();

    [SerializeField]
    private MeshRenderer meshRendererF;
    private GameObject currentHairF;
    private GameObject currentAccessoryF;
    private GameObject currentChestF;
    private GameObject currentLegsF;
    private GameObject currentFeetF;

    int hairIndexF = 0;
    int hairColorIndexF = 0;
    int skinColorIndexF = 0;
    int accessoryIndexF = 0;
    int chestIndexF = 0;
    int legsIndexF = 0;
    int feetIndexF = 0;
    #endregion

    int hairIndex = 0;
    int colorIndex = 0;
    int skinColorIndex = 0;
    int accessoryIndex = 0;
    int chestIndex = 0;
    int legsIndex = 0;
    int feetIndex = 0;

    int colorIndexHair = 0;
    int colorIndexChest = 0;
    int colorIndexLegs = 0;
    int colorIndexFeet = 0;

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
        foreach (var cloth in accessories)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        #endregion

        #region F
        foreach (var cloth in hairStylesF)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        foreach (var cloth in clothesChestF)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        foreach (var cloth in clothesFeetF)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        foreach (var cloth in clothesLegsF)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        foreach (var cloth in accessoriesF)
        {
            if (cloth != null) cloth.SetActive(false);
        }
        #endregion

    }

    #region Buttons

    #region Skin
    public void SkinColorRigh()
    {
        if (skinColorIndex < skin.Count - 1)
            skinColorIndex++;
        else
            skinColorIndex=0;
        ApplySelection(CustomSystem.TypeCloth.SKIN_COLOR, skinColorIndex);
        Singleton.Instance.skinColorIndex = skinColorIndex;

        /*if (skinColorIndexF < skinColors.Count - 1)
            skinColorIndexF++;
        else
            skinColorIndexF = 0;
        ApplySelectionF(CustomSystem.TypeClothF.SKIN_COLOR, skinColorIndexF);*/
    }
    public void SkinColorLeft()
    {
        if (skinColorIndex > 0)
            skinColorIndex--;
        else
            skinColorIndex = skin.Count - 1;
        ApplySelection(CustomSystem.TypeCloth.SKIN_COLOR, skinColorIndex);
        Singleton.Instance.skinColorIndex = skinColorIndex;

        /*if (skinColorIndexF > 0)
            skinColorIndexF--;
        else
            skinColorIndexF = skinColors.Count - 1;
        ApplySelectionF(CustomSystem.TypeClothF.SKIN_COLOR, skinColorIndexF);*/
    }
    #endregion

    #region Hair_Color
    public void HairColorRigh()
    {
        if (colorIndex < Colors.Count - 1)
            colorIndex++;
        else
            colorIndex = 0;
        ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR, colorIndex);

        if (hairColorIndexF < Colors.Count - 1)
            hairColorIndexF++;
        else
            hairColorIndexF = 0;
        ApplySelectionF(CustomSystem.TypeClothF.HAIR_COLOR, hairColorIndexF);
    }
    public void HairColorLeft()
    {
        if (colorIndex > 0)
            colorIndex--;
        else
            colorIndex = Colors.Count - 1;
        ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR, colorIndex);

        if (hairColorIndexF > 0)
            hairColorIndexF--;
        else
            hairColorIndexF = Colors.Count - 1;
        ApplySelectionF(CustomSystem.TypeClothF.HAIR_COLOR, hairColorIndexF);
    }

    public void ColorRigh()
    {
        switch (currentObjeto)
        {
            case 0:
                if (colorIndexHair < Colors.Count - 1)
                    colorIndexHair++;
                else
                    colorIndexHair = 0;
                ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR2, colorIndexHair);
                Singleton.Instance.colorIndexHair = colorIndexHair;
                break;
            case 1:
                if (colorIndexChest < Colors.Count - 1)
                    colorIndexChest++;
                else
                    colorIndexChest = 0;
                ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR2, colorIndexChest);
                Singleton.Instance.colorIndexChest = colorIndexChest;
                break;
            case 2:
                if (colorIndexLegs < Colors.Count - 1)
                    colorIndexLegs++;
                else
                    colorIndexLegs = 0;
                ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR2, colorIndexLegs);
                Singleton.Instance.colorIndexLegs = colorIndexLegs;
                break;
            case 3:
                if (colorIndexFeet < Colors.Count - 1)
                    colorIndexFeet++;
                else
                    colorIndexFeet = 0;
                ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR2, colorIndexFeet);
                Singleton.Instance.colorIndexFeet = colorIndexFeet;
                break;
        }
    }
    public void ColorLeft()
    {
        switch (currentObjeto)
        {
            case 0:
                if (colorIndexHair > 0)
                    colorIndexHair--;
                else
                    colorIndexHair = Colors.Count - 1;
                ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR2, colorIndexHair);
                Singleton.Instance.colorIndexHair = colorIndexHair;
                break;
            case 1:
                if (colorIndexChest > 0)
                    colorIndexChest--;
                else
                    colorIndexChest = Colors.Count - 1;
                ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR2, colorIndexChest);
                Singleton.Instance.colorIndexChest = colorIndexChest;
                break;
            case 2:
                if (colorIndexLegs > 0)
                    colorIndexLegs--;
                else
                    colorIndexLegs = Colors.Count - 1;
                ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR2, colorIndexLegs);
                Singleton.Instance.colorIndexLegs = colorIndexLegs;
                break;
            case 3:
                if (colorIndexFeet > 0)
                    colorIndexFeet--;
                else
                    colorIndexFeet = Colors.Count - 1;
                ApplySelection(CustomSystem.TypeCloth.Cloth_COLOR2, colorIndexFeet);
                Singleton.Instance.colorIndexFeet = colorIndexFeet;
                break;
        }
    }
    #endregion

    #region Hair
    public void HairRigh()
    {
        if (hairIndex < hairStyles.Count - 1)
            hairIndex++;
        else
            hairIndex = 0;
        ApplySelection(CustomSystem.TypeCloth.HAIR, hairIndex);
        Singleton.Instance.hairIndex = hairIndex;

        if (hairIndexF < hairStylesF.Count - 1)
            hairIndexF++;
        else
            hairIndexF = 0;
        ApplySelectionF(CustomSystem.TypeClothF.HAIR, hairIndexF);
    }
    public void HairLeft()
    {
        if (hairIndex > 0)
            hairIndex--;
        else
            hairIndex = hairStyles.Count - 1;
        ApplySelection(CustomSystem.TypeCloth.HAIR, hairIndex);
        Singleton.Instance.hairIndex = hairIndex;

        if (hairIndexF > 0)
            hairIndexF--;
        else
            hairIndexF = hairStylesF.Count - 1;
        ApplySelectionF(CustomSystem.TypeClothF.HAIR, hairIndexF);
    }
    #endregion

    #region Chest
    public void ChestRigh()
    {
        if (chestIndex < clothesChest.Count - 1)
            chestIndex++;
        else
            chestIndex = 0;
        ApplySelection(CustomSystem.TypeCloth.CHEST, chestIndex);
        Singleton.Instance.chestIndex = chestIndex;

        if (chestIndexF < clothesChestF.Count - 1)
            chestIndexF++;
        else
            chestIndexF = 0;
        ApplySelectionF(CustomSystem.TypeClothF.CHEST, chestIndexF);
    }
    public void ChestLeft()
    {
        if (chestIndex > 0)
            chestIndex--;
        else
            chestIndex = clothesChest.Count - 1;
        ApplySelection(CustomSystem.TypeCloth.CHEST, chestIndex);
        Singleton.Instance.chestIndex = chestIndex;

        if (chestIndexF > 0)
            chestIndexF--;
        else
            chestIndexF = clothesChestF.Count - 1;
        ApplySelectionF(CustomSystem.TypeClothF.CHEST, chestIndexF);
    }
    #endregion

    #region Legs
    public void LegsRigh()
    {
        if (legsIndex < clothesLegs.Count - 1)
            legsIndex++;
        else
            legsIndex = 0;
        ApplySelection(CustomSystem.TypeCloth.LEGS, legsIndex);
        Singleton.Instance.legsIndex = legsIndex;

        if (legsIndexF < clothesLegsF.Count - 1)
            legsIndexF++;
        else
            legsIndexF = 0;
        ApplySelectionF(CustomSystem.TypeClothF.LEGS, legsIndexF);
    }
    public void LegsLeft()
    {
        if (legsIndex > 0)
            legsIndex--;
        else
            legsIndex = clothesLegs.Count - 1;
        ApplySelection(CustomSystem.TypeCloth.LEGS, legsIndex);
        Singleton.Instance.legsIndex = legsIndex;

        if (legsIndexF > 0)
            legsIndexF--;
        else
            legsIndexF = clothesLegsF.Count - 1;
        ApplySelectionF(CustomSystem.TypeClothF.LEGS, legsIndexF);
    }
    #endregion

    #region Feet
    public void FeetRigh()
    {
        if (feetIndex < clothesFeet.Count - 1)
            feetIndex++;
        else
            feetIndex = 0;
        ApplySelection(CustomSystem.TypeCloth.FEET, feetIndex);
        Singleton.Instance.feetIndex = feetIndex;

        if (feetIndexF < clothesFeetF.Count - 1)
            feetIndexF++;
        else
            feetIndexF = 0;
        ApplySelectionF(CustomSystem.TypeClothF.FEET, feetIndexF);
    }
    public void FeetLeft()
    {
        if (feetIndex > 0)
            feetIndex--;
        else
            feetIndex = clothesFeet.Count - 1;
        ApplySelection(CustomSystem.TypeCloth.FEET, feetIndex);
        Singleton.Instance.feetIndex = feetIndex;

        if (feetIndexF > 0)
            feetIndexF--;
        else
            feetIndexF = clothesFeetF.Count - 1;
        ApplySelectionF(CustomSystem.TypeClothF.FEET, feetIndexF);
    }
    #endregion

    #region Accessory
    public void AccessoryRigh()
    {
        if (accessoryIndex < accessories.Count - 1)
            accessoryIndex++;
        else
            accessoryIndex = 0;
        ApplySelection(CustomSystem.TypeCloth.ACCESSORY, accessoryIndex);

        if (accessoryIndexF < accessoriesF.Count - 1)
            accessoryIndexF++;
        else
            accessoryIndexF = 0;
        ApplySelectionF(CustomSystem.TypeClothF.ACCESSORY, accessoryIndexF);
    }
    public void AccessoryLeft()
    {
        if (accessoryIndex > 0)
            accessoryIndex--;
        else
            accessoryIndex = accessories.Count - 1;
        ApplySelection(CustomSystem.TypeCloth.ACCESSORY, accessoryIndex);

        if (accessoryIndexF > 0)
            accessoryIndexF--;
        else
            accessoryIndexF = accessoriesF.Count - 1;
        ApplySelectionF(CustomSystem.TypeClothF.ACCESSORY, accessoryIndexF);
    }
    #endregion

    #endregion

    void ApplySelection(CustomSystem.TypeCloth type, int index)
    {
        switch (type)
        {
            case CustomSystem.TypeCloth.HAIR:
                if (currentHair != null)
                {
                    currentHair.SetActive(false);
                }
                currentHair = SearchCloth(type, index);
                currentCloth = currentHair;
                //currentObjeto = 0;
                /*if (currentHairF != null)
                {
                    currentHairF.SetActive(false);
                }
                currentHairF = SearchCloth(type, index);*/
                break;

            case CustomSystem.TypeCloth.CHEST:
                if (currentChest != null)
                {
                    currentChest.SetActive(false);
                }
                currentChest = SearchCloth(type, index);
                currentCloth = currentChest;
                //currentObjeto = 1;
                /*if (currentChestF != null)
                {
                    currentChestF.SetActive(false);
                }
                currentChestF = SearchCloth(type, index);*/
                break;

            case CustomSystem.TypeCloth.LEGS:
                if (currentLegs != null)
                {
                    currentLegs.SetActive(false);
                }
                currentLegs = SearchCloth(type, index);
                currentCloth = currentLegs;
                //currentObjeto = 2;
                /*if (currentLegsF != null)
                {
                    currentLegsF.SetActive(false);
                }
                currentLegsF = SearchCloth(type, index);*/
                break;

            case CustomSystem.TypeCloth.ACCESSORY:
                if (currentAccessory != null)
                {
                    currentAccessory.SetActive(false);
                }
                currentAccessory = SearchCloth(type, index);

                /*if (currentAccessoryF != null)
                {
                    currentAccessoryF.SetActive(false);
                }
                currentAccessoryF = SearchCloth(type, index);*/
                break;

            case CustomSystem.TypeCloth.FEET:
                if (currentFeet != null)
                {
                    currentFeet.SetActive(false);
                }
                currentFeet = SearchCloth(type, index);
                currentCloth = currentFeet;
                //currentObjeto = 3;
                /*if (currentFeetF != null)
                {
                    currentFeetF.SetActive(false);
                }
                currentFeetF = SearchCloth(type, index);*/
                break;

            case CustomSystem.TypeCloth.Cloth_COLOR:
                if (currentHair != null)
                {
                    currentHair.GetComponent<MeshRenderer>().material.color = Colors[index];
                }

               /* if (currentHairF != null)
                {
                    currentHairF.GetComponent<MeshRenderer>().material.color = hairColors[index];
                }*/
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

    void ApplySelectionF(CustomSystem.TypeClothF type, int index)
    {
        switch (type)
        {
            case CustomSystem.TypeClothF.HAIR:
                if (currentHairF != null)
                {
                    currentHairF.SetActive(false);
                }
                currentHairF = SearchClothF(type, index);
                break;

            case CustomSystem.TypeClothF.CHEST:
                if (currentChestF != null)
                {
                    currentChestF.SetActive(false);
                }
                currentChestF = SearchClothF(type, index);
                break;

            case CustomSystem.TypeClothF.LEGS:
                if (currentLegsF != null)
                {
                    currentLegsF.SetActive(false);
                }
                currentLegsF = SearchClothF(type, index);
                break;

            case CustomSystem.TypeClothF.ACCESSORY:
                if (currentAccessoryF != null)
                {
                    currentAccessoryF.SetActive(false);
                }
                currentAccessoryF = SearchClothF(type, index);
                break;

            case CustomSystem.TypeClothF.FEET:
                if (currentFeetF != null)
                {
                    currentFeetF.SetActive(false);
                }
                currentFeetF = SearchClothF(type, index);
                break;

            case CustomSystem.TypeClothF.HAIR_COLOR:
                if (currentHairF != null)
                {
                    currentHairF.GetComponent<MeshRenderer>().material.color = Colors[index];
                }
                break;

            case CustomSystem.TypeClothF.SKIN_COLOR:
                //meshRenderer.material.color = skinColors[index];

                //meshRendererF.material.color = skinColors[index];
                break;
        }
    }

    GameObject SearchCloth(CustomSystem.TypeCloth tipo, int indice)
    {
        switch (tipo)
        {
            case CustomSystem.TypeCloth.HAIR:
                for (int i = 0; i < hairStyles.Count; i++)
                {
                    if (i == indice)
                    {
                        if (hairStyles[i] != null)
                        {
                            hairStyles[i].SetActive(true);
                            return hairStyles[i];
                        }
                    }
                }

                /*for (int i = 0; i < hairStylesF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (hairStylesF[i] != null)
                        {
                            hairStylesF[i].SetActive(true);
                            return hairStylesF[i];
                        }
                    }
                }*/
                break;

            case CustomSystem.TypeCloth.CHEST:
                for (int i = 0; i < clothesChest.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesChest[i] != null)
                        {
                            clothesChest[i].SetActive(true);
                            return clothesChest[i];
                        }
                    }
                }

                /*for (int i = 0; i < clothesChestF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesChestF[i] != null)
                        {
                            clothesChestF[i].SetActive(true);
                            return clothesChestF[i];
                        }
                    }
                }*/
                break;

            case CustomSystem.TypeCloth.LEGS:
                for (int i = 0; i < clothesLegs.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesLegs[i] != null)
                        {
                            clothesLegs[i].SetActive(true);
                            return clothesLegs[i];
                        }
                    }
                }

                /*for (int i = 0; i < clothesLegsF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesLegsF[i] != null)
                        {
                            clothesLegsF[i].SetActive(true);
                            return clothesLegsF[i];
                        }
                    }
                }*/
                break;

            case CustomSystem.TypeCloth.ACCESSORY:
                for (int i = 0; i < accessories.Count; i++)
                {
                    if (i == indice)
                    {
                        if (accessories[i] != null)
                        {
                            accessories[i].SetActive(true);
                            return accessories[i];
                        }
                    }
                }

                /*for (int i = 0; i < accessoriesF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (accessoriesF[i] != null)
                        {
                            accessoriesF[i].SetActive(true);
                            return accessoriesF[i];
                        }
                    }
                }*/
                break;

            case CustomSystem.TypeCloth.FEET:
                for (int i = 0; i < clothesFeet.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesFeet[i] != null)
                        {
                            clothesFeet[i].SetActive(true);
                            return clothesFeet[i];
                        }
                    }
                }

               /* for (int i = 0; i < clothesFeetF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesFeetF[i] != null)
                        {
                            clothesFeetF[i].SetActive(true);
                            return clothesFeetF[i];
                        }
                    }
                }*/
                break;
        }
        return null;
    }


    GameObject SearchClothF(CustomSystem.TypeClothF tipo, int indice)
    {
        switch (tipo)
        {
            case CustomSystem.TypeClothF.HAIR:
                for (int i = 0; i < hairStylesF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (hairStylesF[i] != null)
                        {
                            hairStylesF[i].SetActive(true);
                            return hairStylesF[i];
                        }
                    }
                }
                break;

            case CustomSystem.TypeClothF.CHEST:
                for (int i = 0; i < clothesChestF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesChestF[i] != null)
                        {
                            clothesChestF[i].SetActive(true);
                            return clothesChestF[i];
                        }
                    }
                }
                break;

            case CustomSystem.TypeClothF.LEGS:
                for (int i = 0; i < clothesLegsF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesLegsF[i] != null)
                        {
                            clothesLegsF[i].SetActive(true);
                            return clothesLegsF[i];
                        }
                    }
                }
                break;

            case CustomSystem.TypeClothF.ACCESSORY:
                for (int i = 0; i < accessoriesF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (accessoriesF[i] != null)
                        {
                            accessoriesF[i].SetActive(true);
                            return accessoriesF[i];
                        }
                    }
                }
                break;

            case CustomSystem.TypeClothF.FEET:
                for (int i = 0; i < clothesFeetF.Count; i++)
                {
                    if (i == indice)
                    {
                        if (clothesFeetF[i] != null)
                        {
                            clothesFeetF[i].SetActive(true);
                            return clothesFeetF[i];
                        }
                    }
                }
                break;
        }
        return null;
    }

}
