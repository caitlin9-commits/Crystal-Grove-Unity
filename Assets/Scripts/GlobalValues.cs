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
    private int pineconeAmount;
    private bool spokeToOldMan;



    // public TMP_Text treeCutText;
    public TMP_Text woodAmountText;
    public Image woodImage;

    public TMP_Text pineconeAmountText;
    public Image pineconeImage;

    public TMP_Text coinAmountText;
    public TMP_Text instructionText;

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

        // treeCutText.text = treesCut.ToString();
        pineconeImage.enabled = false;
        pineconeAmountText.text = "";
        woodImage.enabled = false;
        woodAmountText.text = "";//woodAmount.ToString();
        coinAmountText.text = coinsAmount.ToString();
        instructionText.text = "";
    }


    public static void treesCutIncrememt()
    {
        myGlobalValues.treesCut++;
        // myGlobalValues.treeCutText.text = myGlobalValues.treesCut.ToString();
    }

    public static int getTreesCut()
    {
        return myGlobalValues.treesCut;
    }

    public static int getWoodAmount()
    {
        return myGlobalValues.woodAmount;
    }
    public static int getPineconeAmount()
    {
        return myGlobalValues.pineconeAmount;
    }
    public static int getCoinAmount()
    {
        return myGlobalValues.coinsAmount;
    }


    public static void changeWoodAmount(int change)
    {
        myGlobalValues.woodAmount+=change;
        myGlobalValues.woodAmountText.text = myGlobalValues.woodAmount.ToString();
        myGlobalValues.woodImage.enabled = true;

    }

    public static void changePineconeAmount(int change)
    {
        myGlobalValues.pineconeAmount+=change;

        myGlobalValues.pineconeAmountText.text = myGlobalValues.pineconeAmount==0?"":myGlobalValues.pineconeAmount.ToString();
        myGlobalValues.pineconeImage.enabled = myGlobalValues.pineconeAmount==0?false:true;

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
        myGlobalValues.woodImage.enabled = false;
        myGlobalValues.woodAmountText.text = "";
        myGlobalValues.coinAmountText.text = myGlobalValues.coinsAmount.ToString();
    }
   
    public static void setInstructionText(string keyAction,string key, string action)
    {
        myGlobalValues.instructionText.text = keyAction+" '"+key+"' to "+action;
    }
    public static void setInstructionTextString(string text)
    {
        myGlobalValues.instructionText.text = text;
    }

    public static void clearInstructionText()
    {
        myGlobalValues.instructionText.text = "";
    }

}
