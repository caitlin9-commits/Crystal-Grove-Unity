using UnityEngine;

//This controller links to the Facthna character
//This controller was originally created before Global Values, and probably could have all its functionality added there instead
public class OldManController : MonoBehaviour
{
    private static OldManController myOldMan;

    public Animator myAnim;
    private int counter;

    private bool agreedToHelp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agreedToHelp = false;
        myOldMan = this;

        myAnim = gameObject.GetComponent<Animator>();  
        counter = 0;
    }
 


    //Sets and checks if you have agreed to help Fachtna
    public static void agreeToHelp()
    {
        myOldMan.agreedToHelp = true;
    }

    public static bool checkIfAgreedToHelp()
    {
        return myOldMan.agreedToHelp;
    }
}
