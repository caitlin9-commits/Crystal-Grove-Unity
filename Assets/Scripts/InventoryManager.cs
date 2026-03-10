using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager myInventoryManager;


    private bool hasAxe;
    private bool hasFishingRod;


    public Image axeImage;
    public Image rodImage;
    public Image fishImage;
    public TMP_Text fishAmountText;

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
        hasAxe = false;
        axeImage.enabled = false;
        rodImage.enabled = false;
        fishImage.enabled = false;
        fishAmountText.text = "";
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
    public static void getBasicAxe()
    {
        myInventoryManager.hasAxe = true;
        myInventoryManager.axeImage.enabled = true;
    }

    public static void getFishingRod()
    {
        myInventoryManager.hasFishingRod = true;
        myInventoryManager.rodImage.enabled = true;
    }

}
