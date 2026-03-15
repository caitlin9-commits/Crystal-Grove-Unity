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
    public bool canPlant;
    public bool canFish;
    public bool canRecycle;

    public bool fading;

    private int chopSoundCounter;
    private int walkSoundCounter;

    public Animator myAnim;
    [SerializeField] bool backTurned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        myAnim = gameObject.GetComponent<Animator>();

        chopSoundCounter = 300;
        walkSoundCounter = 300;
        canWalk = true;
        canPlant = true;
        canSleep = false;
        canRecycle = false;
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

            if (x > 0)
                scale.x = -Mathf.Abs(scale.x);
            else if (x < 0)
                scale.x = Mathf.Abs(scale.x);

            transform.localScale = scale;
        }
        else
        {
            // Stop animations when walking disabled
            myAnim.SetBool("BackTurned", false);
            myAnim.SetFloat("MoveSpeed", 0);
        }

        // Walking
        if (canWalk && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            walkSoundCounter++;
            if (walkSoundCounter > 90)
            {
                SoundEffectManager.Play("Footsteps");
                walkSoundCounter = 0;
            }
        }


        // CHOPPING
        if (canChop && Input.GetKey(KeyCode.Space))
        {
            chopSoundCounter++;
            if (chopSoundCounter > 104)
            {
                SoundEffectManager.Play("Chopping");
                chopSoundCounter = 0;
            }
        }


      

        chopping = canChop && Input.GetKey(KeyCode.Space);
        myAnim.SetBool("Chopping", chopping);


        // PLANTING
        if (canPlant && Input.GetKey(KeyCode.P))
        {
                canPlant = false;
                PlantPinecone();    
        }

        if (canFish && Input.GetKey(KeyCode.F))
        {
                canFish = false;
                Fish();    
        }

        if (canRecycle && Input.GetKey(KeyCode.R))
        {
                canRecycle = false;
                Recycle();    
        }



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
            canPlant = false;

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
        if (other.CompareTag("Tent") && !fainted && TimeManager.hasSpokenToOldMan())
        {
            canSleep = true;
            canPlant = false;
            GlobalValues.setInstructionText("Press","Z","sleep");
        }

        if (other.CompareTag("water"))
        {

            bool hasRod = InventoryManager.checkForRod();
            if (hasRod)
            {
                canFish = true;
                GlobalValues.setInstructionText("Press","F","fish");  
            }
            else
            {
                GlobalValues.setInstructionText("Go to","Old Man","get rod for fishing");    
            }

        }
        
        if (other.CompareTag("bin"))
        {
            canRecycle = true;
            GlobalValues.setInstructionText("Press","R","recycle");     
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            canChop = false;
            canPlant = true;
            GlobalValues.clearInstructionText();
        }
        if (other.CompareTag("Tent"))
        {
            canSleep = false;
            canPlant = true;
            GlobalValues.clearInstructionText();
        }

        if (other.CompareTag("water"))
        {
            canFish = false;
            GlobalValues.clearInstructionText();
        }

        if (other.CompareTag("bin"))
        {
            canRecycle = false;
            GlobalValues.clearInstructionText();
        }
    }

    public void TreeFell()
    {
        canChop = false;
        canPlant = true;
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
        float x = 682.8f;
        float y = 37f;
        float z = -2.5f;
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

        if(TimeManager.getDay() == 6)
        {
            GlobalValues.setInstructionTextString("GAME OVER");
        }
        else
        {
            
            canWalk = true;
            if(fainted)
            {
                GlobalValues.setInstructionTextString("You fainted.");
                fainted = false;
            }    
        }

        
    }

    async void PlantPinecone()
    {
        int pineconeAmount = InventoryManager.getPineconeAmount();
        if (pineconeAmount > 0)
        {

            canWalk = false;
            myAnim.SetBool("Planting", true);
            await Task.Delay(3000); 
            TreeSpawner.PlantSprout(transform.position.x, transform.position.z);
            myAnim.SetBool("Planting", false);
            GlobalValues.setInstructionTextString("You planted a pinecone.");
            canWalk = true;
            await Task.Delay(2000); 
            canPlant = true;
            GlobalValues.clearInstructionText();
        }
        else
        {
            GlobalValues.setInstructionTextString("No pinecones to plant");
            await Task.Delay(2000); 
            GlobalValues.clearInstructionText();
            canPlant = true;
        }
    }


    async void Fish()
    {
        canWalk = false;
        myAnim.SetBool("Casting", true);
        await Task.Delay(1000); 
        SoundEffectManager.Play("Fishing");
        myAnim.SetBool("Fishing", true);
        await Task.Delay(7000); 
        SoundEffectManager.Play("Success");
        myAnim.SetBool("Casting", false);
        myAnim.SetBool("Fishing", false);

        int random = Random.Range(0,3);

        if (random == 2)
        {
            InventoryManager.changeTrashAmount(1);
            GlobalValues.setInstructionTextString("You found some trash.");
        }
        else
        {
            InventoryManager.changeFishAmount(1);
            GlobalValues.setInstructionTextString("You caught a fish.");    
        }

        
        canFish = true;
        canWalk = true;
        await Task.Delay(2000); 
        GlobalValues.clearInstructionText();
    }

    async void Recycle()
    {
        int trashAmount = InventoryManager.getTrashAmount();

        if (trashAmount == 0)
        {
            GlobalValues.setInstructionTextString("You do not have any trash to recycle.");    
        }
        else
        {
            InventoryManager.changeTrashAmount(-trashAmount);
            InventoryManager.changeRecycledAmount(trashAmount);
            GlobalValues.setInstructionTextString("You recycled your trash.");
            canRecycle = false;
            await Task.Delay(2000); 
            GlobalValues.clearInstructionText();
        }

        
    }

}
