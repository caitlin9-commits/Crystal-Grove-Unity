using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This controller is linked to the main menu game object
//This controller manages what screen of menu you are looking at
public class MenuController : MonoBehaviour
{

    //Game objects for the menu tab buttons
    public Image playerTabBtn;
    public Image controlsTabBtn;
    public Image journalTabBtn;
    public Image mapTabBtn;

    
    //Game objects for the menu screens
    public Image playerTab;
    public Image controlsTab;
    public Image journalTab;
    public Image mapTab;

    public TMP_Text coinAmountText;

    

    private int selectedTab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Add on click listeners to each tab buttons, so it sets appropriate screen selected

        Button btn1 = playerTabBtn.GetComponent<Button>();
        btn1.onClick.RemoveAllListeners();
		btn1.onClick.AddListener(()=>SelectTab(1));

        Button btn2 = controlsTabBtn.GetComponent<Button>();
        btn2.onClick.RemoveAllListeners();
		btn2.onClick.AddListener(()=>SelectTab(2));

        Button btn3 = journalTabBtn.GetComponent<Button>();
        btn3.onClick.RemoveAllListeners();
		btn3.onClick.AddListener(()=>SelectTab(3));

        Button btn4 = mapTabBtn.GetComponent<Button>();
        btn4.onClick.RemoveAllListeners();
		btn4.onClick.AddListener(()=>SelectTab(4));
        
    }

    //Called each time the menu is opened
    void OnEnable()
    {
        //Sets the initial selected tab to the Player tab
        selectedTab = 1;
        playerTabBtn.color = Color.white;
        controlsTabBtn.color = Color.grey;
        journalTabBtn.color = Color.grey;
        mapTabBtn.color = Color.grey;

        playerTab.enabled = true;
        controlsTab.enabled = false;
        journalTab.enabled = false;
        mapTab.enabled = false;

        //Gets coin amount and shows on screen
        int coinsAmount = InventoryManager.getCoinAmount();
        coinAmountText.text = coinsAmount.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called when a tab is clicked
    void SelectTab(int tabNum)
    {
        //Intiially greys out all the tabs
        playerTabBtn.color = Color.grey;
        controlsTabBtn.color = Color.grey;
        journalTabBtn.color = Color.grey;
        mapTabBtn.color = Color.grey;

        playerTab.enabled = false;
        controlsTab.enabled = false;
        journalTab.enabled = false;
        mapTab.enabled = false;

        //Based on which tab selected, it makes that button highlighted and shows respective screen
        if(tabNum == 1)
        {
            playerTabBtn.color = Color.white;
            playerTab.enabled = true;
        }
        else if(tabNum == 2)
        {
            controlsTabBtn.color = Color.white;
            controlsTab.enabled = true;
        }
        else if(tabNum == 3)
        {
            journalTabBtn.color = Color.white;
            journalTab.enabled = true;
        }
        else if(tabNum == 4)
        {
            mapTabBtn.color = Color.white;
            mapTab.enabled = true;
        }

    }
}
