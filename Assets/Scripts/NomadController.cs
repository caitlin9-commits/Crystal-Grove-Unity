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

    public bool inOrbZone;

    public bool fading;

    private int chopSoundCounter;

    private int orbSoundCounter;
    private int walkSoundCounter;


    private float speedMultiplier = 1;


    public Animator myAnim;
    [SerializeField] bool backTurned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        myAnim = gameObject.GetComponent<Animator>();

        chopSoundCounter = 300;
        walkSoundCounter = 300;
        orbSoundCounter = 0;
        canWalk = true;
        canPlant = true;
        canSleep = false;
        canRecycle = false;
        fading = false;
        myNomad = this;
        fainted = false;
        inOrbZone = false;

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

            if ( z > 0) { backTurned = true; }
            else { backTurned = false; }

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
                SoundEffectManager.Play("Footsteps",0.2f);
                walkSoundCounter = 0;
            }
        }


        // CHOPPING
        if (canChop && Input.GetKey(KeyCode.E))
        {
            chopSoundCounter++;
            if (chopSoundCounter > 104)
            {
                SoundEffectManager.Play("Chopping");
                chopSoundCounter = 0;
            }
        }

      
        chopping = canChop && Input.GetKey(KeyCode.E);
        myAnim.SetBool("Chopping", chopping);

        if (chopping)
        {
            GlobalValues.setSeenChopText();
        }

        // PLANTING
        if (canPlant && Input.GetKey(KeyCode.Q))
        {
                canPlant = false;
                PlantPinecone();    
        }

        //FISHING
        if (canFish && Input.GetKey(KeyCode.E))
        {
                canFish = false;
                Fish();    
        }

        //RECYCLING
        if (canRecycle && Input.GetKey(KeyCode.E))
        {
                canRecycle = false;
                Recycle();    
        }


        if (Input.GetKey(KeyCode.N))
        {
            speedMultiplier+=0.1f;
        }


        // SLEEP
        if (canSleep && Input.GetKey(KeyCode.E))
        {
            canSleep = false;
            GoToSleep();
        }

        if(inOrbZone)
        {
            orbSoundCounter++;
            if (orbSoundCounter > 500)
            {

                float NomadX = myNomad.transform.position.x;
                float NomadZ = myNomad.transform.position.z;

                float orbX=3542f;
                float orbY=24f;
                float orbZ=-3119f;

                

                float dist = Vector3.Distance(myNomad.transform.position,  new Vector3(orbX,orbY,orbZ));

                float volume = 10/dist;
                if(volume>1f){volume=1f;}


                Debug.Log("DISTNACE FROM ORB "+ dist);


                SoundEffectManager.Play("Chopping",volume);
                orbSoundCounter = 0;
            }
        }
    }



    void FixedUpdate()
    {
        if (!canWalk)
            return;

        Vector3 velocity = rb.linearVelocity;

        // Only modify X and Z — preserve Y for gravity
        velocity.x = x * speed * speedMultiplier;
        velocity.z = z * speed * speedMultiplier;

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
                if(!GlobalValues.checkSeenChopText())
                {
                    GlobalValues.setInstructionText("Hold","E","chop tree");
                    
                }
                
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
            if(!GlobalValues.checkSeenSleepText())
            {
                GlobalValues.setInstructionText("Press","E","sleep");  
                
            }
            
        }

        if (other.CompareTag("water"))
        {

            canPlant = false;

            bool hasRod = InventoryManager.checkForRod();
            if (hasRod)
            {
                canFish = true;
                if(!GlobalValues.checkSeenFishText())
                {
                    GlobalValues.setInstructionText("Press","E","fish");   
                }
                
            }
            else
            {
                GlobalValues.setInstructionText("Go to","Old Man","get rod for fishing");    
            }

        }
        
        if (other.CompareTag("bin"))
        {
            canRecycle = true;
            canPlant = false;
            if(!GlobalValues.checkSeenRecycleText())
            {
                GlobalValues.setInstructionText("Press","E","recycle");    
            }
            
        }


        if (other.CompareTag("orb"))
        {

                Debug.Log("ENTERIONG ORB ZONE");


            inOrbZone = true;
           SoundEffectManager.Play("Glowing");
        }


        if (other.CompareTag("village"))
        {
            Debug.Log("ENTERING VILLAGE");
            canPlant = false;
            
            if (GlobalValues.getDayStartEnvScore()==1)
            {
                MusicManager.PlayNewSong("VillageGood");
            }
            else if (GlobalValues.getDayStartEnvScore()==2)
            {
                MusicManager.PlayNewSong("VillageNeutral");
            }
            else if (GlobalValues.getDayStartEnvScore()==3 || GlobalValues.getDayStartEnvScore()==4)
            {
                MusicManager.PlayNewSong("VillageBad");
            }
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

        if (other.CompareTag("orb"))
        {
            inOrbZone = false;
        }

        if (other.CompareTag("village"))
        {
            canPlant = true;
            Debug.Log("EXITING VILLAGE");
            playForestMusic();
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
        float x = 3254.98f;
        float y = 34.21f;
        float z = -3259.55f;
        myNomad.transform.position =  new Vector3(x,y,z);
        myNomad.GoToSleep();
    }

    public static void setAxe(int level)
    {
        myNomad.myAnim.SetInteger("AxeLevel", level);
    }

    public static void setRod(int level)
    {
        myNomad.myAnim.SetInteger("RodLevel", level);
    }

    async void GoToSleep()
    {
        GlobalValues.setSeenSleepText();          
        GlobalValues.clearInstructionText();
        canWalk = false;
        Debug.Log("FADING: ");
        fading = true;
        await ScreenFader.Instance.FadeOut();
        playForestMusic();

        GlobalValues.setDayStartEnvScore();
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

            GlobalValues.seedsPlantedIncrement();
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
        GlobalValues.setSeenFishText();             

        int rodLevel = InventoryManager.getRodLevel();

        int fishingWaitTime = 7000;
        int trashLikelihood = 3;
        int nothingLikelihood = 30;

        if(rodLevel == 2)
        {
            fishingWaitTime = 5500;
            trashLikelihood = 5;
        }
        else if(rodLevel == 3)
        {
            fishingWaitTime = 3000;
            trashLikelihood = 7;
        }
        else if(rodLevel == 4)
        {
            fishingWaitTime = 1500;
            trashLikelihood = 10;
        }

        
        int envScore = GlobalValues.calculateEnvironmentScore();


        if(envScore == 2){nothingLikelihood = 20;}
        else if(envScore == 3){nothingLikelihood = 10;}
        else if(envScore == 4){nothingLikelihood = 1;}


        canWalk = false;
        myAnim.SetBool("Casting", true);
        await Task.Delay(1000); 
        SoundEffectManager.Play("Fishing");
        myAnim.SetBool("Fishing", true);
        await Task.Delay(fishingWaitTime); 
        SoundEffectManager.Play("Success");
        myAnim.SetBool("Casting", false);
        myAnim.SetBool("Fishing", false);


        int trashRandom = Random.Range(0,trashLikelihood);
        int nothingRandom = Random.Range(0,nothingLikelihood);

        if(nothingRandom == 0)
        {
            GlobalValues.setInstructionTextString("You didn't catch anything.");
        }
        if (trashRandom == 2)
        {
            InventoryManager.changeTrashAmount(1);
            GlobalValues.setInstructionTextString("You found some trash.");
        }
        else
        {
            GlobalValues.fishCaughtIncrememt();
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
        GlobalValues.setSeenRecycleText();                  
        int trashAmount = InventoryManager.getTrashAmount();

        if (trashAmount == 0)
        {
            GlobalValues.setInstructionTextString("You do not have any trash to recycle.");    
        }
        else
        {
            InventoryManager.changeTrashAmount(-trashAmount);
            GlobalValues.changeRecycledAmount(trashAmount);
            InventoryManager.changeCoinsAmount(trashAmount*5);
            GlobalValues.setInstructionTextString("You recycled your trash.");
            canRecycle = false;
            await Task.Delay(2000); 
            GlobalValues.clearInstructionText();
        }

        
    }

    private void playForestMusic()
    {
        if(GlobalValues.getDayStartEnvScore()==1)
        {
            MusicManager.PlayNewSong("ForestGood");
        }
        else if(GlobalValues.getDayStartEnvScore()==2)
        {
            MusicManager.PlayNewSong("ForestNeutral");
        }
        else if(GlobalValues.getDayStartEnvScore()==3 || GlobalValues.getDayStartEnvScore()==4)
        {
            MusicManager.PlayNewSong("ForestBad");
        }
    }



}
