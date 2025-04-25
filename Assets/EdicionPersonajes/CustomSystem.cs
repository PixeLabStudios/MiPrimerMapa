using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomSystem : MonoBehaviour
{
    public enum TypeCloth
    {
        HAIR,
        HAIR_COLOR,
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

    public List<Color> hairColors = new List<Color>();
    //public List<Color> skinColors = new List<Color>();
    public List<Material> skin = new List<Material>();

    public List<GameObject> accessories = new List<GameObject>();

    public List<GameObject> clothesChest = new List<GameObject>();
    public List<GameObject> clothesLegs = new List<GameObject>();
    public List<GameObject> clothesFeet = new List<GameObject>();

    [SerializeField]
    private MeshRenderer meshRenderer;
    private GameObject currentHair;
    private GameObject currentAccessory;
    private GameObject currentChest;
    private GameObject currentLegs;
    private GameObject currentFeet;
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
    int hairColorIndex = 0;
    int skinColorIndex = 0;
    int accessoryIndex = 0;
    int chestIndex = 0;
    int legsIndex = 0;
    int feetIndex = 0;

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
        if (hairColorIndex < hairColors.Count - 1)
            hairColorIndex++;
        else
            hairColorIndex = 0;
        ApplySelection(CustomSystem.TypeCloth.HAIR_COLOR, hairColorIndex);

        if (hairColorIndexF < hairColors.Count - 1)
            hairColorIndexF++;
        else
            hairColorIndexF = 0;
        ApplySelectionF(CustomSystem.TypeClothF.HAIR_COLOR, hairColorIndexF);
    }
    public void HairColorLeft()
    {
        if (hairColorIndex > 0)
            hairColorIndex--;
        else
            hairColorIndex = hairColors.Count - 1;
        ApplySelection(CustomSystem.TypeCloth.HAIR_COLOR, hairColorIndex);

        if (hairColorIndexF > 0)
            hairColorIndexF--;
        else
            hairColorIndexF = hairColors.Count - 1;
        ApplySelectionF(CustomSystem.TypeClothF.HAIR_COLOR, hairColorIndexF);
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

                /*if (currentFeetF != null)
                {
                    currentFeetF.SetActive(false);
                }
                currentFeetF = SearchCloth(type, index);*/
                break;

            case CustomSystem.TypeCloth.HAIR_COLOR:
                if (currentHair != null)
                {
                    currentHair.GetComponent<MeshRenderer>().material.color = hairColors[index];
                }

               /* if (currentHairF != null)
                {
                    currentHairF.GetComponent<MeshRenderer>().material.color = hairColors[index];
                }*/
                break;

            case CustomSystem.TypeCloth.SKIN_COLOR:
                //meshRenderer.material.color = skinColors[index];

                meshRendererF.material = skin[index];
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
                    currentHairF.GetComponent<MeshRenderer>().material.color = hairColors[index];
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
