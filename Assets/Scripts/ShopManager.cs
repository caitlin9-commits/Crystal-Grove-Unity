using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using System.Threading.Tasks;

using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        

        // Button btn = option1.GetComponent<Button>();
		// btn.onClick.AddListener(()=>TaskOnClick(1,option1));

        // Button btn2 = option2.GetComponent<Button>();
		// btn2.onClick.AddListener(()=>TaskOnClick(2,option2));

        // Button btn3 = option3.GetComponent<Button>();
		// btn3.onClick.AddListener(()=>TaskOnClick(3,option3));

        // Button btn4 = option4.GetComponent<Button>();
		// btn4.onClick.AddListener(()=>TaskOnClick(4,option4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        SetupOption(1,option1);
        SetupOption(2,option2);
        SetupOption(3,option3);
        SetupOption(4,option4);

        coinsAmount = InventoryManager.getCoinAmount();
        coinAmountText.text = coinsAmount.ToString();


        Button btn = closeText.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
		btn.onClick.AddListener(()=>GlobalValues.toggleShop(false));
    }

    void SetupOption(int number,Canvas option){
	
        TMP_Text itemText = option.transform.Find("ItemText").GetComponent<TMP_Text>();
        Image itemImage = option.transform.Find("ItemImage").GetComponent<Image>();
        TMP_Text costText = option.transform.Find("CostText").GetComponent<TMP_Text>();

        
        string itemName ="";
        int itemCost=0;

        if(number == 1)
        {

            int axeLevel = InventoryManager.getAxeLevel();
            Debug.Log("AXE LEVEL" + axeLevel);


            if(axeLevel==1)
            {
                itemName = "Next Axe: Copper";
                itemCost = 0;
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

            int rodLevel = InventoryManager.getRodLevel();

            Debug.Log("ROD LEVEL" + rodLevel);

            if(rodLevel==1)
            {
                itemName = "Next Rod: Copper";
                itemCost = 0;
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
        else if(number == 3)
        {
            itemName = "Water Wheel";
            itemCost = 50;
        }
        else if(number == 4)
        {
            itemName = "Windmill";
            itemCost = 100;
        }

        itemText.text = itemName;
        costText.text = itemCost.ToString();

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

    async void TaskOnClick(int optionNumber,string item,int cost){
		Debug.Log ("You have clicked the button! " + item);

        
        if(optionNumber == 1 && InventoryManager.getAxeLevel()==4)
        {
            mainText.text = "\"You have fully upgraded your axe.\"";
    
        }
        else if(optionNumber == 2 && InventoryManager.getRodLevel()==4)
        {
             mainText.text = "\"You have fully upgraded your rod.\"";
        }
        else if(optionNumber == 4 && GlobalValues.checkForWindmill())
        {
             mainText.text = "\"You have already purchased the windmill.\"";
        }
        else if (cost > coinsAmount)
        {
             mainText.text = "\"You do not have enough money for this item.\"";
        }
        else
        {
            buyItem(optionNumber,item,cost);
        }
        
        await Task.Delay(1000); 
        GlobalValues.clearInstructionText();
	}

    async void buyItem(int optionNumber,string item,int cost)
    {

        Debug.Log ("BOUGHT ITEM! " + item);


        InventoryManager.changeCoinsAmount(-cost);

        if(optionNumber == 1){InventoryManager.upgradeAxe();}
        else if(optionNumber == 2){InventoryManager.upgradeRod();}
        else if(optionNumber == 3){GlobalValues.buyWaterwheel();}
        else if(optionNumber == 4){GlobalValues.buyWindmill();}

        GlobalValues.setInstructionTextString("You have just purchased: "+ item);
        GlobalValues.toggleShop(false);
    }
}



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