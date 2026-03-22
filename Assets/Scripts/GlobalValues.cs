using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GlobalValues : MonoBehaviour
{

    private static GlobalValues myGlobalValues;
    private int treesCut;
    private int woodDonated;
    private int fishDonated;
    private int seedsPlanted;

    private bool hasWaterWheel;
    private bool hasWindmill;

    
    private bool spokeToOldMan;

    private bool seenTalkText;
    private bool seenChopText;
    private bool seenFishText;
    private bool seenRecycleText;
    private bool seenSleepText;
    
    


    public GameObject menuCanvas;
    public GameObject shopCanvas;

    public GameObject windmill;
    public GameObject waterwheel;
   
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
        // treeCutText.text = treesCut.ToString();
        treesCut = 0;
        
        hasWaterWheel = false;
        hasWindmill = false;

        instructionText.text = "";
        menuCanvas.SetActive(false);
        shopCanvas.SetActive(false);

        windmill.GetComponent<Renderer>().enabled = false;
        waterwheel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            Debug.Log("MENU CLICKED");
        }

        // if(Input.GetKeyDown(KeyCode.M))
        // {
        //     shopCanvas.SetActive(!shopCanvas.activeSelf);
        //     Debug.Log("MENU CLICKED");
        // }
    }

    public static void toggleShop(bool showShop)
    {
        myGlobalValues.shopCanvas.SetActive(showShop);
    }

    public static void treesCutIncrememt()
    {
        myGlobalValues.treesCut++;
        // myGlobalValues.treeCutText.text = myGlobalValues.treesCut.ToString();
    }

    public static void seedsPlantedIncrement()
    {
        myGlobalValues.seedsPlanted++;
    }
    public static void woodDonatedIncrement(int amount)
    {
        myGlobalValues.woodDonated+=amount;
    }
    public static void fishDonatedIncrement(int amount)
    {
        myGlobalValues.fishDonated+=amount;
    }

    public static int getTreesCut()
    {
        return myGlobalValues.treesCut;
    }

    public static bool checkForWaterwheel()
    {
        return myGlobalValues.hasWaterWheel;
    }

    public static bool checkForWindmill()
    {
        return myGlobalValues.hasWindmill;
    }

    public static void buyWaterwheel()
    {
        myGlobalValues.waterwheel.SetActive(true);
        myGlobalValues.hasWaterWheel = true;
    }
    public static void buyWindmill()
    {
        myGlobalValues.windmill.GetComponent<Renderer>().enabled = true;
        myGlobalValues.hasWindmill = true;
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



    public static void setSeenChopText()
    {
        myGlobalValues.seenChopText = true;
    }
    public static bool checkSeenChopText()
    {
        return myGlobalValues.seenChopText;
    }

    public static void setSeenTalkText()
    {
        myGlobalValues.seenTalkText = true;
    }
    public static bool checkSeenTalkText()
    {
        return myGlobalValues.seenTalkText;
    }

    public static void setSeenFishText()
    {
        myGlobalValues.seenFishText = true;
    }
    public static bool checkSeenFishText()
    {
        return myGlobalValues.seenFishText;
    }

    public static void setSeenRecycleText()
    {
        myGlobalValues.seenRecycleText = true;
    }
    public static bool checkSeenRecycleText()
    {
        return myGlobalValues.seenRecycleText;
    }

    public static void setSeenSleepText()
    {
        myGlobalValues.seenSleepText = true;
    }
    public static bool checkSeenSleepText()
    {
        return myGlobalValues.seenSleepText;
    }



    public static int calculateEnvironmentScore()
    {
        return 0;
    }

}
