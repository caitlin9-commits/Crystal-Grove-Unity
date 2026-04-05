using UnityEngine;
using UnityEngine.UI;
using System;

public class TitleScreensController : MonoBehaviour
{

    private static TitleScreensController myTitleScreens;

    public Canvas startCanvas;
    public Image startCanvasImg;

    public Image goodEndCanvas;
    public Image neutralEndCanvas;
    public Image badEndCanvas;

    public Button startGameButton;
    public Button aboutGameButton;

    public Sprite nightStart;

    public Sprite goodEnd;
    public Sprite neutralEnd;
    public Sprite badEnd;

    private float tempSecond;
    private bool showingEnd;

    private void Awake()
    {
        if(myTitleScreens == null)
        {
            myTitleScreens = this;
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

        myTitleScreens = this;
        // startCanvas.enabled = false;
        goodEndCanvas.enabled = false;
        neutralEndCanvas.enabled = false;
        badEndCanvas.enabled = false;
        showingEnd = false;
        

		startGameButton.onClick.AddListener(()=>StartGame());
        aboutGameButton.onClick.AddListener(()=>Debug.Log("TEST BUTTON"));

        int hourNow = DateTime.Now.Hour;
        
        if(hourNow<6 && hourNow>19)
        {
            startCanvasImg.sprite = nightStart;    
        }

        
        // Button btn2 = aboutGameButton.GetComponent<Button>();
        // btn2.onClick.RemoveAllListeners();
		// btn2.onClick.AddListener(()=>AboutGame());
    }

    // Update is called once per frame
    void Update()
    {
        
        if(showingEnd)
        {
            tempSecond += Time.deltaTime;

            if(tempSecond>10)
            {
                show2ndEndScreen();
            }
        }

    }




    void StartGame()
    {
        Debug.Log("START GAME");
        startCanvas.enabled = false;
    }
    
    void AboutGame()
    {
        
    }

    public static void ShowEnding()
    {
        myTitleScreens.showingEnd = true;
        int envScore = GlobalValues.calculateEnvironmentScore();
        if(envScore == 1){myTitleScreens.goodEndCanvas.enabled = true;}
        else if(envScore == 2){myTitleScreens.neutralEndCanvas.enabled = true;}
        else {myTitleScreens.badEndCanvas.enabled = true;}
    }


    public void show2ndEndScreen()
    {
        int envScore = GlobalValues.calculateEnvironmentScore();
        if(envScore == 1){goodEndCanvas.sprite = goodEnd;}
        else if(envScore == 2){neutralEndCanvas.sprite = neutralEnd;}
        else {badEndCanvas.sprite = badEnd;}

        showingEnd = false;
    }
}
