using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//This class is linked the GlobalValues game object (this is an invisible game object)
//This class keeps track of lots of different metrics in the game
public class GlobalValues : MonoBehaviour
{

    //Creating different variables to represent different metrics we want to track in the game
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
    
    

    //Passed in game objects that this controller controls whether they appear or not

    public GameObject menuCanvas;
    public GameObject shopCanvas;

    public GameObject windmill;
    public GameObject waterwheel;
   
    public TMP_Text instructionText;

//creates instance of the class, so it can be used in other classes
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
        //Sets initial values for all our metrics

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
        if(Input.GetKeyDown(KeyCode.Tab)) //open and close the main menu by clicking TAB
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            GlobalValues.clearInstructionText(); // removes instructional text from screen
            Debug.Log("MENU CLICKED");
        }

       
    }

    public static void toggleShop(bool showShop) //Function to show/hide shop interface
    {
        myGlobalValues.shopCanvas.SetActive(showShop);
    }

    public static void treesCutIncrememt() //Increments how many trees you have cut
    {
        myGlobalValues.treesCut++;
    }

    public static void fishCaughtIncrememt() //Increments how many fish you have caught
    {
        myGlobalValues.fishCaught++;
    }

    public static void seedsPlantedIncrement() //Increments how many seeds you have planted
    {
        myGlobalValues.seedsPlanted++;
    }
    public static void woodDonatedIncrement(int amount) //Increments how much wood you have donated
    {
        myGlobalValues.woodDonated+=amount;
    }
    public static int getWoodDonated()  //Gets how much wood you have donated
    {
        return myGlobalValues.woodDonated;
    }
    public static void fishDonatedIncrement(int amount)  //Increments how much fish you have donated
    {
        myGlobalValues.fishDonated+=amount;
    }

    public static void changeRecycledAmount(int change)  //Increments how much rubbish you have recycled
    {
        myGlobalValues.recycledAmount+=change;
    }

    public static int getTreesCut()  //Gets how many trees you have cut
    {
        return myGlobalValues.treesCut;
    }

    public static bool checkForWaterwheel() //Checks if you have purchased waterwheel
    {
        return myGlobalValues.hasWaterWheel;
    }

    public static bool checkForWindmill() //Checks if you have purchased windmill
    {
        return myGlobalValues.hasWindmill;
    }

    public static void buyWaterwheel()  //Sets waterwheel as purchased and displays game object in game
    {
        myGlobalValues.waterwheel.SetActive(true);
        myGlobalValues.hasWaterWheel = true;
    }
    public static void buyWindmill() //Sets windmill as purchased and displays game object in game
    {
        myGlobalValues.windmill.GetComponent<Renderer>().enabled = true;
        myGlobalValues.hasWindmill = true;
    }

    public static bool getHasSpokenToFairy() //Checks if you have spoken to Blaithin
    {
        return myGlobalValues.hasSpokenToFairy;
    }

    public static void setHasSpokenToFairy()  //Sets that you have spoken to Blaithin
    {
         myGlobalValues.hasSpokenToFairy = true;
    }

    public static bool getHasSpokenToBebhinnAboutFairy() //Checks if you have spoken to Bebhinn about Blaithin
    {
        return myGlobalValues.hasSpokenToBebhinnAboutFairy;
    }

    public static void setHasSpokenToBebhinnAboutFairy() //Sets that you have spoken to Bebhinn about Blaithin
    {
         myGlobalValues.hasSpokenToBebhinnAboutFairy = true;
    }

    
    public static void setInstructionText(string keyAction,string key, string action) //Shows instructional text on screen
    {
        myGlobalValues.instructionText.text = keyAction+" '"+key+"' to "+action;
    }
    public static void setInstructionTextString(string text) //Shows instructional text on screen
    {
        myGlobalValues.instructionText.text = text;
    }

    public static void clearInstructionText() //Clears instructional text from screen
    {
        myGlobalValues.instructionText.text = "";
    }



    public static void setSeenChopText() //Sets if you have seen chop text and acted on it (to prevent showing again)
    {
        myGlobalValues.seenChopText = true;
    }
    public static bool checkSeenChopText() //Checks if you have seen chop text and acted on it (to prevent showing again)
    {
        return myGlobalValues.seenChopText;
    }

    public static void setSeenTalkText()  //Sets if you have seen talk text and acted on it (not checked anymore)
    {
        myGlobalValues.seenTalkText = true;
    }
    public static bool checkSeenTalkText() //Checks if you have seen talk text and acted on it (not called anymore)
    {
        return myGlobalValues.seenTalkText;
    }

    public static void setSeenFishText()  //Sets if you have seen fish text and acted on it (not checked anymore)
    {
        myGlobalValues.seenFishText = true;
    }
    public static bool checkSeenFishText() //Checks if you have seen fish text and acted on it (not called anymore)
    {
        return myGlobalValues.seenFishText;
    }

    public static void setSeenRecycleText() //Sets if you have seen recycle text and acted on it (not checked anymore)
    {
        myGlobalValues.seenRecycleText = true;
    }
    public static bool checkSeenRecycleText() //Checks if you have seen recycle text and acted on it (not called anymore)
    {
        return myGlobalValues.seenRecycleText;
    }

    public static void setSeenSleepText() //Sets if you have seen sleep text and acted on it (not checked anymore)
    {
        myGlobalValues.seenSleepText = true;
    }
    public static bool checkSeenSleepText() //Checks if you have seen sleep text and acted on it (not called anymore)
    {
        return myGlobalValues.seenSleepText;
    }

    public static void setDayStartEnvScore() //Sets the environment score at start of each day
    {
        myGlobalValues.dayStartEnvScore = calculateEnvironmentScore();
    }
    public static int getDayStartEnvScore() //Get the environment score at start of each day
    {
        return myGlobalValues.dayStartEnvScore;
    }



    public static int calculateEnvironmentScore() //Algorithm we have created to calculate a score for environmental degredation
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

        int day = TimeManager.getDay();

        //1. Healthy environment
        //2. Wilting environment
        //3. Dead environment
        //4. Crystal infected environment

        if(netScore>=0){return 1;}
        else if(netScore>=-20 && netScore<0){return 2;}
        else if(netScore>=-40 && netScore<-20){return day==2?2:3;} //Ensures maximum degradation on day 2 is wilting
        else if(netScore<-40){return day==2?2:4;} //Ensures maximum degradation on day 2 is wilting
        return 1;
    }

}
