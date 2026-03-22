using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{

    public Image playerTabBtn;
    public Image controlsTabBtn;
    public Image journalTabBtn;
    public Image mapTabBtn;

    
    public Image playerTab;
    public Image controlsTab;
    public Image journalTab;
    public Image mapTab;

    

    private int selectedTab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    void OnEnable()
    {
        selectedTab = 1;
        playerTabBtn.color = Color.white;
        controlsTabBtn.color = Color.grey;
        journalTabBtn.color = Color.grey;
        mapTabBtn.color = Color.grey;

        playerTab.enabled = true;
        controlsTab.enabled = false;
        journalTab.enabled = false;
        mapTab.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectTab(int tabNum)
    {
        playerTabBtn.color = Color.grey;
        controlsTabBtn.color = Color.grey;
        journalTabBtn.color = Color.grey;
        mapTabBtn.color = Color.grey;

        playerTab.enabled = true;
        controlsTab.enabled = false;
        journalTab.enabled = false;
        mapTab.enabled = false;

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
