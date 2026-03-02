using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public class NomadController : MonoBehaviour
{

    private static NomadController myNomad;

    public float speed;
    public float groundDist;
    public bool chopping;

    private bool fainted;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    private bool canChop;
    public bool canWalk;
    public bool canSleep;
    public bool fading;

    private int chopSoundCounter;

    public Animator myAnim;
    [SerializeField] bool backTurned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        myAnim = gameObject.GetComponent<Animator>();

        chopSoundCounter = 300;
        canWalk = true;
        canSleep = false;
        fading = false;
        myNomad = this;
        fainted = false;

    }
    float x;
    float z;

    // Update is called once per frame
   void Update()
    {
        // Read input ONLY
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (canWalk)
        {
            // Animation speed based on current velocity
            myAnim.SetFloat("MoveSpeed", rb.linearVelocity.magnitude);

            if (rb.linearVelocity.magnitude < 0.1f)
            {
                backTurned = false;
            }

            if (!backTurned && z > 0) { backTurned = true; }
            else if (backTurned && z < 0) { backTurned = false; }

            myAnim.SetBool("BackTurned", backTurned);

            // Flip sprite
            Vector3 scale = transform.localScale;

            if (x < 0)
                scale.x = -Mathf.Abs(scale.x);
            else if (x > 0)
                scale.x = Mathf.Abs(scale.x);

            transform.localScale = scale;
        }
        else
        {
            // Stop animations when walking disabled
            myAnim.SetBool("BackTurned", false);
            myAnim.SetFloat("MoveSpeed", 0);
        }

        // CHOPPING
        if (canChop && Input.GetKey(KeyCode.Space))
        {
            chopSoundCounter++;
            if (chopSoundCounter > 300)
            {
                SoundEffectManager.Play("Chopping");
                chopSoundCounter = 0;
            }
        }

        chopping = canChop && Input.GetKey(KeyCode.Space);
        myAnim.SetBool("Chopping", chopping);

        // SLEEP
        if (canSleep && Input.GetKey(KeyCode.Z))
        {
            canSleep = false;
            GoToSleep();
        }
    }



    void FixedUpdate()
    {
        if (!canWalk)
            return;

        Vector3 velocity = rb.linearVelocity;

        // Only modify X and Z — preserve Y for gravity
        velocity.x = x * speed;
        velocity.z = z * speed;

        rb.linearVelocity = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            bool hasAxe = InventoryManager.checkForAxe();
            if (hasAxe)
            {
                canChop = true;
                GlobalValues.setInstructionText("Hold","Spacebar","chop tree");    
            }
            else
            {
                GlobalValues.setInstructionText("Go to","Old Man","get axe for chopping trees");    
            }
            
        }
        if (other.CompareTag("Tent") && !fainted)
        {
            canSleep = true;
            GlobalValues.setInstructionText("Press","Z","sleep");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            canChop = false;
            GlobalValues.clearInstructionText();
        }
        if (other.CompareTag("Tent"))
        {
            canSleep = false;
            GlobalValues.clearInstructionText();
        }
    }

    public void TreeFell()
    {
        canChop = false;
        Debug.Log("TREE FELL");
    }

    
    public static void setWalk(bool walkingEnabled)
    {
        myNomad.canWalk = walkingEnabled;
    }

    public static void sendToTent()
    {
        myNomad.fainted = true;
        Debug.Log("Sending to tent: ");
        float x = 3406.5f;
        float y = 31.2f;
        float z = -3203.7f;
        myNomad.transform.position =  new Vector3(x,y,z);
        myNomad.GoToSleep();
    }

    async void GoToSleep()
    {
        GlobalValues.clearInstructionText();
        canWalk = false;
        Debug.Log("FADING: ");
        fading = true;
        await ScreenFader.Instance.FadeOut();
        TreeSpawner.SpawnTrees();
        await Task.Delay(1000); 
        TimeManager.sleep();
        await ScreenFader.Instance.FadeIn();
        fading = false;
        canWalk = true;
        if(fainted)
        {
            GlobalValues.setInstructionTextString("You fainted.");
            fainted = false;
        }
    }

}
