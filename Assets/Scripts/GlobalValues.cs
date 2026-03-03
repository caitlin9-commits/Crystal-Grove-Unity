using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GlobalValues : MonoBehaviour
{

    private static GlobalValues myGlobalValues;

    private int treesCut;
    private int woodAmount;
    private int coinsAmount;

    public TMP_Text treeCutText;
    public TMP_Text woodAmountText;
    public TMP_Text coinAmountText;

    private void Awake()
    {
        if(myGlobalValues == null)
        {
            myGlobalValues = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treesCut = 0;
        woodAmount = 0;
        coinsAmount = 0;

        treeCutText.text = treesCut.ToString();
        woodAmountText.text = woodAmount.ToString();
        coinAmountText.text = coinsAmount.ToString();
    }


    public static void treesCutIncrememt()
    {
        myGlobalValues.treesCut++;
        myGlobalValues.treeCutText.text = myGlobalValues.treesCut.ToString();
    }

    public static int getTreesCut()
    {
        return myGlobalValues.treesCut;
    }

    public static int getWoodAmount()
    {
        return myGlobalValues.woodAmount;
    }
    public static int getCoinAmount()
    {
        return myGlobalValues.coinsAmount;
    }


    public static void changeWoodAmount(int change)
    {
        myGlobalValues.woodAmount+=change;
        myGlobalValues.woodAmountText.text = myGlobalValues.woodAmount.ToString();

    }
    public static void changeCoinsmount(int change)
    {
        myGlobalValues.coinsAmount+=change;
        myGlobalValues.coinAmountText.text = myGlobalValues.coinsAmount.ToString();

    }

    public static void sellWood()
    {
        myGlobalValues.coinsAmount+=myGlobalValues.woodAmount;
        myGlobalValues.woodAmount=0;
        myGlobalValues.woodAmountText.text = myGlobalValues.woodAmount.ToString();
        myGlobalValues.coinAmountText.text = myGlobalValues.coinsAmount.ToString();

    }
   
}
