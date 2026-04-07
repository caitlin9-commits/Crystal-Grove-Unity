using UnityEngine;
using UnityEngine.UI;
using System;

//This controller is linked to the TitleScreen game object which contains all the title screen canvases as children
//This controller manages what title screen appears at the start and end 
public class TitleScreensController : MonoBehaviour
{

    private static TitleScreensController myTitleScreens;


    //Different canvases for the different title screens and the buttons on them
    public Canvas startCanvas;
    public Image startCanvasImg;

    public Canvas aboutCanvas;
    public Image aboutCanvasImg;
    public Button startGameButtonAbout;

    public Image goodEndCanvas;
    public Image neutralEndCanvas;
    public Image badEndCanvas;

    public Button startGameButton;
    public Button aboutGameButton;

    public Sprite nightStart;
    public Sprite nightAbout;

    public Sprite goodEnd;
    public Sprite neutralEnd;
    public Sprite badEnd;

    private float tempSecond;
    private bool showingEnd;

//creates instance of the class, so it can be used in other classes
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
        //Sets which title screen to originally show (Start screen), hides the others
        myTitleScreens = this;
        // startCanvas.enabled = false;
        aboutCanvas.enabled = false;
        goodEndCanvas.enabled = false;
        neutralEndCanvas.enabled = false;
        badEndCanvas.enabled = false;
        showingEnd = false;
        

        //Adds on click listeners to the buttons on the start and about screens
		startGameButton.onClick.AddListener(()=>StartGame());
        aboutGameButton.onClick.AddListener(()=>AboutGame());
        startGameButtonAbout.onClick.AddListener(()=>StartGame());


        //Gets current time of device
        int hourNow = DateTime.Now.Hour;
        
        //If nighttime, shows night time versions of start and about screen background images
        if(hourNow<6 || hourNow>19)
        {
            startCanvasImg.sprite = nightStart;    
            aboutCanvasImg.sprite = nightAbout;    
        }

        
        // Button btn2 = aboutGameButton.GetComponent<Button>();
        // btn2.onClick.RemoveAllListeners();
		// btn2.onClick.AddListener(()=>AboutGame());
    }

    // Update is called once per frame
    void Update()
    {
        //If showing end screen, changes to 2nd end screen after 10 seconds
        if(showingEnd)
        {
            tempSecond += Time.deltaTime;

            if(tempSecond>10)
            {
                show2ndEndScreen();
            }
        }

    }



    //Called when start game button is clicked, hides start and about screens
    void StartGame()
    {
        Debug.Log("START GAME");
        startCanvas.enabled = false;
        aboutCanvas.enabled = false;
    }
    
    //Called when about game button is clicked, hides start screen and shows about screen
    void AboutGame()
    {
        startCanvas.enabled = false;
        aboutCanvas.enabled = true;
    }

    //Called when game is ended, shows appropriate end screen based on environment score
    public static void ShowEnding()
    {
        myTitleScreens.showingEnd = true;
        int envScore = GlobalValues.calculateEnvironmentScore();
        if(envScore == 1){myTitleScreens.goodEndCanvas.enabled = true;}
        else if(envScore == 2){myTitleScreens.neutralEndCanvas.enabled = true;}
        else {myTitleScreens.badEndCanvas.enabled = true;}
    }


    //Called 10 seconds after when game hass ended, shows appropriate final end screen based on environment score
    public void show2ndEndScreen()
    {
        int envScore = GlobalValues.calculateEnvironmentScore();
        if(envScore == 1){goodEndCanvas.sprite = goodEnd;}
        else if(envScore == 2){neutralEndCanvas.sprite = neutralEnd;}
        else {badEndCanvas.sprite = badEnd;}

        showingEnd = false;
    }
}
