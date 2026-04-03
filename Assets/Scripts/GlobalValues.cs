using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GlobalValues : MonoBehaviour
{

    private static GlobalValues myGlobalValues;
    private int treesCut;
    private int fishCaught;
    private int woodDonated;
    private int fishDonated;
    private int seedsPlanted;
    private int recycledAmount;

    private bool hasWaterWheel;
    private bool hasWindmill;

    private bool hasSpokenToFairy;
    private bool hasSpokenToBebhinnAboutFairy;

    private int dayStartEnvScore;

    
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
        fishCaught = 0;
        recycledAmount=0;
        woodDonated=0;
        dayStartEnvScore=1;
        
        hasWaterWheel = false;
        hasWindmill = false;
        hasSpokenToFairy=false;

        instructionText.text = "";
        menuCanvas.SetActive(false);
        shopCanvas.SetActive(false);

        windmill.GetComponent<Renderer>().enabled = false;
        waterwheel.SetActive(false);

        setInstructionText("Press","Tab","View Controls");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            GlobalValues.clearInstructionText();
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
    }

    public static void fishCaughtIncrememt()
    {
        myGlobalValues.fishCaught++;
    }

    public static void seedsPlantedIncrement()
    {
        myGlobalValues.seedsPlanted++;
    }
    public static void woodDonatedIncrement(int amount)
    {
        myGlobalValues.woodDonated+=amount;
    }
    public static int getWoodDonated()
    {
        return myGlobalValues.woodDonated;
    }
    public static void fishDonatedIncrement(int amount)
    {
        myGlobalValues.fishDonated+=amount;
    }

    public static void changeRecycledAmount(int change)
    {
        myGlobalValues.recycledAmount+=change;
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

    public static bool getHasSpokenToFairy()
    {
        return myGlobalValues.hasSpokenToFairy;
    }

    public static void setHasSpokenToFairy()
    {
         myGlobalValues.hasSpokenToFairy = true;
    }

    public static bool getHasSpokenToBebhinnAboutFairy()
    {
        return myGlobalValues.hasSpokenToBebhinnAboutFairy;
    }

    public static void setHasSpokenToBebhinnAboutFairy()
    {
         myGlobalValues.hasSpokenToBebhinnAboutFairy = true;
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

    public static void setDayStartEnvScore()
    {
        myGlobalValues.dayStartEnvScore = calculateEnvironmentScore();
    }
    public static int getDayStartEnvScore()
    {
        return myGlobalValues.dayStartEnvScore;
    }



    public static int calculateEnvironmentScore()
    {
        double treeCutScore = myGlobalValues.treesCut *-3;
        double fishCaughtScore = myGlobalValues.fishCaught *-2;
        double recycledScore = myGlobalValues.recycledAmount *2;
        double seedsScore = myGlobalValues.seedsPlanted *3;
        double woodDonatedScore = myGlobalValues.woodDonated *0.3;
        double fishdDonatedScore = myGlobalValues.fishDonated;

        double waterwheelScore = myGlobalValues.hasWaterWheel?50:0;
        double windmillScore = myGlobalValues.hasWindmill?50:0;

        double netScore = treeCutScore+fishCaughtScore+recycledScore
        +seedsScore+woodDonatedScore+fishdDonatedScore
        +windmillScore+waterwheelScore;


        if(netScore>=0){return 1;}
        else if(netScore>=-20 && netScore<0){return 2;}
        else if(netScore>=-40 && netScore<-20){return 3;}
        else if(netScore<-40){return 4;}
        return 0;
    }

}
