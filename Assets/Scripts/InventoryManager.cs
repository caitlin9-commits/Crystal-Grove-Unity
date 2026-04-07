using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This controller is linked to the invetory game object
//This controller keeps track of Nomad's inventory and displays it appropriately on screen
public class InventoryManager : MonoBehaviour
{
    private static InventoryManager myInventoryManager;

    private int woodAmount;
    private int fishAmount;
    private int coinsAmount;
    private int pineconeAmount;
    private int trashAmount;
    


    private int axeLevel;
    private int rodLevel;

    private bool hasAxe;
    private bool hasFishingRod;

 //learned how to use UI Game Objects from Christina Creates Games on Youtube, video link: https://www.youtube.com/watch?v=1OwQflHq5kg - no code copied
  
    //Linked game objects for the different text and images in the inventory 
    public Image axeImage;
    public Image rodImage;

    public TMP_Text coinAmountText;

    public TMP_Text woodAmountText;
    public Image woodImage;
    public TMP_Text fishAmountText;
    public Image fishImage;
    public TMP_Text pineconeAmountText;
    public Image pineconeImage;
    public TMP_Text trashAmountText;
    public Image trashImage;

    
    public Sprite copperRod;
    public Sprite goldRod;
    public Sprite crystalRod;

    public Sprite copperAxe;
    public Sprite goldAxe;
    public Sprite crystalAxe;



//creates instance of the class, so it can be used in other classes
    private void Awake()
    {
        if(myInventoryManager == null)
        {
            myInventoryManager = this;
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
        
        //Sets initial inventory values (everyhing starts at 0 or lowest value)

        woodAmount = 0;
        coinsAmount = 0;
        trashAmount = 0;
        pineconeAmount = 0;
        axeLevel = 1;
        rodLevel = 1;

        hasAxe = false;

        axeImage.enabled = false;
        rodImage.enabled = false;

        trashImage.enabled = false;
        trashAmountText.text = "";
        pineconeImage.enabled = false;
        pineconeAmountText.text = "";
        fishImage.enabled = false;
        fishAmountText.text = "";
        woodImage.enabled = false;
        woodAmountText.text = "";//woodAmount.ToString();
        coinAmountText.text = coinsAmount.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Below functions checks for and adds specific items to inventory 
    public static bool checkForAxe()
    {
        return myInventoryManager.hasAxe;
    }
    public static bool checkForRod()
    {
        return myInventoryManager.hasFishingRod;
    }

    public static int getAxeLevel()
    {
        return myInventoryManager.axeLevel;
    }

    public static int getRodLevel()
    {
        return myInventoryManager.rodLevel;
    }


    public static void getBasicAxe()
    {
        myInventoryManager.hasAxe = true;
        myInventoryManager.axeImage.enabled = true;
        myInventoryManager.axeLevel = 1;
    }

    public static void getFishingRod()
    {
        myInventoryManager.hasFishingRod = true;
        myInventoryManager.rodImage.enabled = true;
        myInventoryManager.rodLevel = 1;
    }


    //Upgrades your axe when purchased from store and updates image in inventory
    public static void upgradeAxe()
    {
        Debug.Log ("Upgrading axe " );

        myInventoryManager.axeLevel++;
        if(myInventoryManager.axeLevel == 2){myInventoryManager.axeImage.sprite = myInventoryManager.copperAxe;}
        if(myInventoryManager.axeLevel == 3){myInventoryManager.axeImage.sprite = myInventoryManager.goldAxe;}
        if(myInventoryManager.axeLevel == 4){myInventoryManager.axeImage.sprite = myInventoryManager.crystalAxe;}

        NomadController.setAxe(myInventoryManager.axeLevel);
    }


    //Upgrades your rod when purchased from store and updates image in inventory
    public static void upgradeRod()
    {
        myInventoryManager.rodLevel++;
        if(myInventoryManager.rodLevel == 2){myInventoryManager.rodImage.sprite = myInventoryManager.copperRod;}
        if(myInventoryManager.rodLevel == 3){myInventoryManager.rodImage.sprite = myInventoryManager.goldRod;}
        if(myInventoryManager.rodLevel == 4){myInventoryManager.rodImage.sprite = myInventoryManager.crystalRod;}

        NomadController.setRod(myInventoryManager.rodLevel);
    }


    //Below functions checks for specific item amounts in inventory 
    public static int getWoodAmount()
    {
        return myInventoryManager.woodAmount;
    }
    public static int getFishAmount()
    {
        return myInventoryManager.fishAmount;
    }

    public static int getPineconeAmount()
    {
        return myInventoryManager.pineconeAmount;
    }
    public static int getCoinAmount()
    {
        return myInventoryManager.coinsAmount;
    }
    public static int getTrashAmount()
    {
        return myInventoryManager.trashAmount;
    }



    //Changes the amount of wood you have in inventory
    public static void changeWoodAmount(int change)
    {
        myInventoryManager.woodAmount+=change;
        myInventoryManager.woodAmountText.text = myInventoryManager.woodAmount.ToString();
        myInventoryManager.woodImage.enabled = true;
    }


    //Changes the amount of seeds you have in inventory (can go up or down)
    public static void changePineconeAmount(int change)
    {
        myInventoryManager.pineconeAmount+=change;

        myInventoryManager.pineconeAmountText.text = myInventoryManager.pineconeAmount==0?"":myInventoryManager.pineconeAmount.ToString(); //Hides counter text if you have none
        myInventoryManager.pineconeImage.enabled = myInventoryManager.pineconeAmount==0?false:true; //Hides image if you have none
    }

    //Changes the amount of fish you have in inventory (can go up or down)
    public static void changeFishAmount(int change)
    {
        myInventoryManager.fishAmount+=change;

        myInventoryManager.fishAmountText.text = myInventoryManager.fishAmount==0?"":myInventoryManager.fishAmount.ToString();
        myInventoryManager.fishImage.enabled = myInventoryManager.fishAmount==0?false:true;
    }

    //Changes the amount of rubbish you have in inventory (can go up or down)
    public static void changeTrashAmount(int change)
    {
        myInventoryManager.trashAmount+=change;

        myInventoryManager.trashAmountText.text = myInventoryManager.trashAmount==0?"":myInventoryManager.trashAmount.ToString();
        myInventoryManager.trashImage.enabled = myInventoryManager.trashAmount==0?false:true;
    }
    

    //Changes the amount of coins you have in inventory (can go up or down)
    public static void changeCoinsAmount(int change)
    {
        myInventoryManager.coinsAmount+=change;
        myInventoryManager.coinAmountText.text = myInventoryManager.coinsAmount.ToString();

    }
    

    //Sell wood to Fachtna
    public static void sellWood()
    {
        myInventoryManager.coinsAmount+=myInventoryManager.woodAmount; //converts wood into coins
        myInventoryManager.woodAmount=0; //removes all wood
        myInventoryManager.woodImage.enabled = false; //hides wood image
        myInventoryManager.woodAmountText.text = ""; //clears wood amount text
        myInventoryManager.coinAmountText.text = myInventoryManager.coinsAmount.ToString(); //updates coin amount text
    }


    //Sell fish to Fachtna (similar logic as above, just for fish)
    public static void sellFish()
    {
        myInventoryManager.coinsAmount+=myInventoryManager.fishAmount;
        myInventoryManager.fishAmount=0;
        myInventoryManager.fishImage.enabled = false;
        myInventoryManager.fishAmountText.text = "";
        myInventoryManager.coinAmountText.text = myInventoryManager.coinsAmount.ToString();
    }
   

    //Donate fish to Ruairi (similar to above however you do not get coins, just increases donation counter)
    public static void donateFish()
    {
        GlobalValues.fishDonatedIncrement(myInventoryManager.fishAmount);
        myInventoryManager.fishAmount=0;
        myInventoryManager.fishImage.enabled = false;
        myInventoryManager.fishAmountText.text = "";
    }

    //Donate wood to Bebhinn (similar to above, just for wood)
     public static void donateWood()
    {
        GlobalValues.woodDonatedIncrement(myInventoryManager.woodAmount);
        changePineconeAmount(myInventoryManager.woodAmount/5);
        myInventoryManager.woodAmount=0;
        myInventoryManager.woodImage.enabled = false;
        myInventoryManager.woodAmountText.text = "";
    }
   
 //learned how to use UI Game Objects from Christina Creates Games on Youtube, video link: https://www.youtube.com/watch?v=1OwQflHq5kg


}
