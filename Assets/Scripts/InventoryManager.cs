using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager myInventoryManager;

    private int woodAmount;
    private int fishAmount;
    private int coinsAmount;
    private int pineconeAmount;
    private int trashAmount;
    private int recycledAmount;


    private int axeLevel;
    private int rodLevel;

    private bool hasAxe;
    private bool hasFishingRod;


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
        recycledAmount=0;
        woodAmount = 0;
        coinsAmount = 1000;
        trashAmount = 0;

        hasAxe = true;

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

    public static void upgradeAxe()
    {
        Debug.Log ("Upgrading axe " );

        myInventoryManager.axeLevel++;
        if(myInventoryManager.axeLevel == 2){myInventoryManager.axeImage.sprite = myInventoryManager.copperAxe;}
        if(myInventoryManager.axeLevel == 3){myInventoryManager.axeImage.sprite = myInventoryManager.goldAxe;}
        if(myInventoryManager.axeLevel == 4){myInventoryManager.axeImage.sprite = myInventoryManager.crystalAxe;}

        NomadController.setAxe(myInventoryManager.axeLevel);
    }

    public static void upgradeRod()
    {
        myInventoryManager.rodLevel++;
        if(myInventoryManager.rodLevel == 2){myInventoryManager.rodImage.sprite = myInventoryManager.copperRod;}
        if(myInventoryManager.rodLevel == 3){myInventoryManager.rodImage.sprite = myInventoryManager.goldRod;}
        if(myInventoryManager.rodLevel == 4){myInventoryManager.rodImage.sprite = myInventoryManager.crystalRod;}

        NomadController.setRod(myInventoryManager.rodLevel);
    }

    public static int getWoodAmount()
    {
        return myInventoryManager.woodAmount;
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



    public static void changeWoodAmount(int change)
    {
        myInventoryManager.woodAmount+=change;
        myInventoryManager.woodAmountText.text = myInventoryManager.woodAmount.ToString();
        myInventoryManager.woodImage.enabled = true;
    }

    public static void changePineconeAmount(int change)
    {
        myInventoryManager.pineconeAmount+=change;

        myInventoryManager.pineconeAmountText.text = myInventoryManager.pineconeAmount==0?"":myInventoryManager.pineconeAmount.ToString();
        myInventoryManager.pineconeImage.enabled = myInventoryManager.pineconeAmount==0?false:true;
    }

    public static void changeFishAmount(int change)
    {
        myInventoryManager.fishAmount+=change;

        myInventoryManager.fishAmountText.text = myInventoryManager.fishAmount==0?"":myInventoryManager.fishAmount.ToString();
        myInventoryManager.fishImage.enabled = myInventoryManager.fishAmount==0?false:true;
    }

    public static void changeTrashAmount(int change)
    {
        myInventoryManager.trashAmount+=change;

        myInventoryManager.trashAmountText.text = myInventoryManager.trashAmount==0?"":myInventoryManager.trashAmount.ToString();
        myInventoryManager.trashImage.enabled = myInventoryManager.trashAmount==0?false:true;
    }
    public static void changeRecycledAmount(int change)
    {
        myInventoryManager.recycledAmount+=change;
    }

    public static void changeCoinsAmount(int change)
    {
        myInventoryManager.coinsAmount+=change;
        myInventoryManager.coinAmountText.text = myInventoryManager.coinsAmount.ToString();

    }
    

    public static void sellWood()
    {
        myInventoryManager.coinsAmount+=myInventoryManager.woodAmount;
        myInventoryManager.woodAmount=0;
        myInventoryManager.woodImage.enabled = false;
        myInventoryManager.woodAmountText.text = "";
        myInventoryManager.coinAmountText.text = myInventoryManager.coinsAmount.ToString();
    }
   

    public static void donateFish()
    {
        GlobalValues.fishDonatedIncrement(myInventoryManager.fishAmount);
        myInventoryManager.fishAmount=0;
        myInventoryManager.fishImage.enabled = false;
        myInventoryManager.fishAmountText.text = "";
    }

     public static void donateWood()
    {
        GlobalValues.woodDonatedIncrement(myInventoryManager.woodAmount);
        myInventoryManager.woodAmount=0;
        myInventoryManager.woodImage.enabled = false;
        myInventoryManager.woodAmountText.text = "";
    }
   


}
