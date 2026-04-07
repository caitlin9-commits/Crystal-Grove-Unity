using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using System.Threading.Tasks;

using UnityEngine.Events;
using UnityEngine.EventSystems;


//This controller is linked to the shop game object which is a canvas for the shop
// This controller manages what items are avaiable in the shop and manages purchases
public class ShopManager : MonoBehaviour
{

    //Passed in game objects representing the different shop purchase buttons and the stock to appear
     //learned how to use UI Game Objects from Christina Creates Games on Youtube, video link: https://www.youtube.com/watch?v=1OwQflHq5kg

    public Canvas option1;
    public Canvas option2;
    public Canvas option3;
    public Canvas option4;
    public TMP_Text coinAmountText;

    public TMP_Text closeText;
    public TMP_Text mainText;


    public Sprite copperRod;
    public Sprite goldRod;
    public Sprite crystalRod;

    public Sprite copperAxe;
    public Sprite goldAxe;
    public Sprite crystalAxe;

    

    private int coinsAmount;

    //Called each time the shop screen is opened
    void OnEnable()
    {
        //Set up what is available to purchase
        SetupOption(1,option1);
        SetupOption(2,option2);
        SetupOption(3,option3);
        SetupOption(4,option4);

        //Get current amount of coins from inventory
        coinsAmount = InventoryManager.getCoinAmount();
        coinAmountText.text = coinsAmount.ToString();


        //Add on click listenet to close button to close the shop menu and reset the text on it
        Button btn = closeText.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
		btn.onClick.AddListener(()=>{
            GlobalValues.toggleShop(false);
            mainText.text = "\"Welcome to the forge! What can we do ya for?\"";
        });
    }

    //sets up one of the purchase options
    void SetupOption(int number,Canvas option){
	
        //Gets the text and image game objects from the specific option
        TMP_Text itemText = option.transform.Find("ItemText").GetComponent<TMP_Text>();
        Image itemImage = option.transform.Find("ItemImage").GetComponent<Image>();
        TMP_Text costText = option.transform.Find("CostText").GetComponent<TMP_Text>();

        //Gets the amount of wood donated 
        int woodDonated = GlobalValues.getWoodDonated();
        
        string itemName ="";
        int itemCost=0;

        //Based on what option we are setting up and what you have purchases before,
        //it shows the appropriate item and price to buy in each slot on the shop screen
        if(number == 1)
        {

            //Axe option to purchase is based on what axe you currently have
            int axeLevel = InventoryManager.getAxeLevel();
            Debug.Log("AXE LEVEL" + axeLevel);


            if(axeLevel==1)
            {
                itemName = "Next Axe: Copper";
                itemCost = 10;
                itemImage.sprite = copperAxe;
            }

            if(axeLevel==2)
            {
                itemName = "Next Axe: Gold";
                itemCost = 50;
                itemImage.sprite = goldAxe;
            }

            if(axeLevel==3)
            {
                itemName = "Next Axe: Crystal";
                itemCost = 100;
                itemImage.sprite = crystalAxe;
            }

            
        }
        else if(number == 2)
        {

            //Rod option to purchase is based on what rod you currently have
            int rodLevel = InventoryManager.getRodLevel();

            Debug.Log("ROD LEVEL" + rodLevel);

            if(rodLevel<2)
            {
                itemName = "Next Rod: Copper";
                itemCost = 10;
                itemImage.sprite = copperRod;
            }

            if(rodLevel==2)
            {
                itemName = "Next Rod: Gold";
                itemCost = 50;
                itemImage.sprite = goldRod;
            }

            if(rodLevel==3)
            {
                itemName = "Next Rod: Crystal";
                itemCost = 100;
                itemImage.sprite = crystalRod;
            }
        }
        //Wter wheel and windmill are cheaper if you have donated wood
        else if(number == 3)
        {
            itemName = "Water Wheel";
            itemCost = 100-woodDonated;
        }
        else if(number == 4)
        {
            itemName = "Windmill";
            itemCost = 100-woodDonated;
        }

        itemText.text = itemName;
        costText.text = itemCost.ToString();

        //On add click listener to shop option button
        Button btn = option.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
		btn.onClick.AddListener(()=>TaskOnClick(number,itemName,itemCost));

        // HoverEvent hover = option.GetComponent<HoverEvent>();

        // hover.onHover.AddListener(() => {
        //     Debug.Log("Mouse entered");
        // });

        // hover.onHoverExit.AddListener(() => {
        //     Debug.Log("Mouse left");
        // });

	}

    //Triggered when you click on one of the shop purchase options
    async void TaskOnClick(int optionNumber,string item,int cost){
		Debug.Log ("You have clicked the button! " + item);

        //Maxed out on axe upgrades
        if(optionNumber == 1 && InventoryManager.getAxeLevel()==4)
        {
            mainText.text = "\"You have fully upgraded your axe.\"";
    
        }
        //Maxed out on rod upgrades
        else if(optionNumber == 2 && InventoryManager.getRodLevel()==4)
        {
             mainText.text = "\"You have fully upgraded your rod.\"";
        }
        //Already bought waterwheel
        else if(optionNumber == 3 && GlobalValues.checkForWaterwheel())
        {
             mainText.text = "\"You have already purchased the waterwheel.\"";
        }
        //Already bought windmill
        else if(optionNumber == 4 && GlobalValues.checkForWindmill())
        {
             mainText.text = "\"You have already purchased the windmill.\"";
        }
        //Don't have enough money for the item
        else if (cost > coinsAmount)
        {
             mainText.text = "\"You do not have enough money for this item.\"";
        }
        //No issue, purchases items
        else
        {
            buyItem(optionNumber,item,cost);
        }
        
        await Task.Delay(1000); 
        GlobalValues.clearInstructionText();
	}


    //Function to buy item
    async void buyItem(int optionNumber,string item,int cost)
    {

        Debug.Log ("BOUGHT ITEM! " + item);

        //Removes coins from inventory
        InventoryManager.changeCoinsAmount(-cost);

        //Based on what you bought, updates your inventory or environment
        if(optionNumber == 1){InventoryManager.upgradeAxe();}
        else if(optionNumber == 2){InventoryManager.upgradeRod();}
        else if(optionNumber == 3){GlobalValues.buyWaterwheel();}
        else if(optionNumber == 4){GlobalValues.buyWindmill();}

        //Show success message and closes show menu
        GlobalValues.setInstructionTextString("You have just purchased: "+ item);
        GlobalValues.toggleShop(false);
    }
}



//Attempted to create hover listener for shop option buttons, based on unity documentation, however it didn't work, so not used
public class HoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent onHover;
    public UnityEvent onHoverExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHover?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onHoverExit?.Invoke();
    }
}
 //learned how to use UI Game Objects from Christina Creates Games on Youtube, video link: https://www.youtube.com/watch?v=1OwQflHq5kg
