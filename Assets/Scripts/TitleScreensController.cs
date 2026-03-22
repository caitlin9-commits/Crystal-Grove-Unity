using UnityEngine;
using UnityEngine.UI;

public class TitleScreensController : MonoBehaviour
{

    private static TitleScreensController myTitleScreens;

    public Canvas startCanvas;
    public Image goodEndCanvas;
    public Image neutralEndCanvas;
    public Image badEndCanvas;

    public Button startGameButton;
    public Button aboutGameButton;


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
        // startCanvas.enabled = false;
        goodEndCanvas.enabled = false;
        neutralEndCanvas.enabled = false;
        badEndCanvas.enabled = false;

        

		startGameButton.onClick.AddListener(()=>StartGame());
        aboutGameButton.onClick.AddListener(()=>Debug.Log("TEST BUTTON"));

        // Button btn2 = aboutGameButton.GetComponent<Button>();
        // btn2.onClick.RemoveAllListeners();
		// btn2.onClick.AddListener(()=>AboutGame());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        int envScore = GlobalValues.calculateEnvironmentScore();
        if(envScore == 1){myTitleScreens.goodEndCanvas.enabled = true;}
        else if(envScore == 2){myTitleScreens.neutralEndCanvas.enabled = true;}
        else {myTitleScreens.badEndCanvas.enabled = true;}
    }

}
