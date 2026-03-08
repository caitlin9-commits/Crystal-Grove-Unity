using UnityEngine;

public class OldManController : MonoBehaviour
{
    private static OldManController myOldMan;

    public Animator myAnim;
    private int counter;

    private bool agreedToHelp;

    private void Awake()
    {
        if(myOldMan == null)
        {
            myOldMan = this;
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
        agreedToHelp = false;

        myAnim = gameObject.GetComponent<Animator>();  
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(counter < 1000)
        {
            myAnim.SetBool("isTree",false);
        }
        else
        {
            // myAnim.SetBool("isTree",true);
        }

        if(counter > 2000)
        {
            counter = 0;
        }

        counter++;
    }

    public static void agreeToHelp()
    {
        myOldMan.agreedToHelp = true;
    }

    public static bool checkIfAgreedToHelp()
    {
        return myOldMan.agreedToHelp;
    }
}
