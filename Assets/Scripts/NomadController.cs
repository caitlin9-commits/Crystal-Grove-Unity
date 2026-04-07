using System.IO;
using UnityEngine;
using System.Threading.Tasks;

//This controller is linked to the Nomad game object
//This controller manages the actions the Nomad can take and their movement
public class NomadController : MonoBehaviour
{

    private static NomadController myNomad;

// start of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8
    public float speed; //Passed in value for movement speed
    public float groundDist; //Passed in value for distance from ground
    // end of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8
    public bool chopping;

    private bool fainted;

// code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    // end of code from ThatOneUnityDev on Youtube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8

    //Different booleans for whether nomad can partake in certain actions or not.
    private bool canChop;
    public bool canWalk;
    public bool canSleep;
    public bool canPlant;
    public bool currentlyPlanting;
    public bool canFish;
    public bool canRecycle;

    public bool inOrbZone;

    public bool fading;

    private int chopSoundCounter;

    private float orbSoundCounter;
    private int walkSoundCounter;


    //Determines how fast nomad moves
    private float speedMultiplier = 1;


    public Animator myAnim;
    [SerializeField] bool backTurned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Sets initial capabilites of Nomad
        // start of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8
        rb = gameObject.GetComponent<Rigidbody>();
        // end of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8
        myAnim = gameObject.GetComponent<Animator>();

        chopSoundCounter = 300;
        walkSoundCounter = 300;
        orbSoundCounter = 0;
        canWalk = true;
        canPlant = true;
        currentlyPlanting = false;
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
        // start of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        // end of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8

        if (canWalk)
        {
            // Animation speed based on current velocity
            //Setting this determines whether to show idle of walking animation
            myAnim.SetFloat("MoveSpeed", rb.linearVelocity.magnitude);

            //Back is turned if walking away from camera, this determines whether to show backwards walking animation or not
            if (rb.linearVelocity.magnitude < 0.1f)
            {
                backTurned = false;
            }

            if ( z > 0) { backTurned = true; }
            else { backTurned = false; }

            myAnim.SetBool("BackTurned", backTurned);

            // Flip sprite depending on what direction they are walking  (left or right)
            Vector3 scale = transform.localScale;

// start of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8 - but altered
            if (x > 0)
                scale.x = -Mathf.Abs(scale.x);
            else if (x < 0)
                scale.x = Mathf.Abs(scale.x);

            transform.localScale = scale;
            // end of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=cqNBA9Pslg8 - but altered
        }
        else
        {
            // Set to idle animation when walking disabled
            myAnim.SetBool("BackTurned", false);
            myAnim.SetFloat("MoveSpeed", 0);
        }

        // Sets walking buttons on keyboard
        if (canWalk && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //Counter to play walking sound every appropriate amount of milliseconds
            walkSoundCounter++;
            if (walkSoundCounter > 90)
            {
                SoundEffectManager.Play("Footsteps",0.2f);
                walkSoundCounter = 0;
            }
        }


        // CHOPPING
        if (canChop && Input.GetKey(KeyCode.E)) //If in chopping zone and pressing action button, start chopping
        {
            //Counter to play chopping sound every appropriate amount of milliseconds
            chopSoundCounter++;
            if (chopSoundCounter > 104)
            {
                SoundEffectManager.Play("Chopping");
                chopSoundCounter = 0;
            }
        }

        //Sets chopping boolean in animation controller to display chopping nomad animation
        chopping = canChop && Input.GetKey(KeyCode.E);
        myAnim.SetBool("Chopping", chopping);

        if (chopping)
        {
            GlobalValues.setSeenChopText();
        }

        // PLANTING
        if (canPlant && Input.GetKey(KeyCode.Q)) //If in plant zone and pressing action button, start planting
        {
            //prevent planting multiple seeds at once
                canPlant = false;
                PlantPinecone();    
        }
        
        if (!canPlant && !currentlyPlanting && Input.GetKeyDown(KeyCode.Q)) //Display error text if tries to plant in inappropriate location
        {
           DisplayPineconeError();
        }


        //FISHING
        if (canFish && Input.GetKey(KeyCode.E)) //If in fishing zone and pressing action button, start fishing
        {
                canFish = false; //prevent fishing multiple times at once
                Fish();    
        }

        //RECYCLING
        if (canRecycle && Input.GetKey(KeyCode.E)) //If in recycle zone and pressing action button, start recycling
        {
                canRecycle = false; //prevent recycling multiple times at once
                Recycle();    
        }


        if (Input.GetKey(KeyCode.LeftShift)) //When holding shift, Nomad moves faster
        {
            speedMultiplier=2f;
        }
        else
        {
            speedMultiplier=1f;
        }


        // SLEEP
        if (canSleep && Input.GetKey(KeyCode.E)) //If in sleeping zone and pressing action button, start sleeping
        {
            canSleep = false;
            GoToSleep();
        }

        if(inOrbZone) //If in orb zone, play orb music based on distance from orb
        {
            //Counter to play orb sound every 4 seconds to prevent audio files overlapping
            orbSoundCounter += Time.deltaTime;
            if (orbSoundCounter > 4)
            {

                float NomadX = myNomad.transform.position.x;
                float NomadZ = myNomad.transform.position.z;

                //Orb coords
                float orbX=3541.65f;
                float orbY=27.84f;
                float orbZ=-3120.48f;

                
                //Calculate distance from orb
                float dist = Vector3.Distance(myNomad.transform.position,  new Vector3(orbX,orbY,orbZ));

                //Calculate volume based on distance
                float volume = 10/dist;
                if(volume>1f){volume=1f;}


                Debug.Log("DISTNACE FROM ORB "+ dist);
                Debug.Log("VOLUME "+ volume);

                //Play orb sound at speific volumee
                SoundEffectManager.Play("Glowing",volume);
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

    //As Nomad enters certain trigger zones, it allows them to perform specific actions and displays instructions on screen
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
            // if(!GlobalValues.checkSeenSleepText())
            // {
                GlobalValues.setInstructionText("Press","E","sleep");  
                
            // }
            
        }

        if (other.CompareTag("water"))
        {

            canPlant = false;

            bool hasRod = InventoryManager.checkForRod();
            if (hasRod)
            {
                canFish = true;
                // if(!GlobalValues.checkSeenFishText())
                // {
                    GlobalValues.setInstructionText("Press","E","fish");   
                // }
                
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
            // if(!GlobalValues.checkSeenRecycleText())
            // {
                GlobalValues.setInstructionText("Press","E","recycle");    
            // }
            
        }


        if (other.CompareTag("orb"))
        {

                Debug.Log("ENTERIONG ORB ZONE");


            inOrbZone = true;
        //    SoundEffectManager.Play("Glowing");
        }


        if (other.CompareTag("NoPlantZone"))
        {
            canPlant = false;   
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

    //As Nomad exits certain trigger zones, it prevents them from performing specific actions and remnoves instructions from screen
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

        if (other.CompareTag("NoPlantZone"))
        {
            canPlant = true;   
        }

        if (other.CompareTag("village"))
        {
            canPlant = true;
            Debug.Log("EXITING VILLAGE");
            playForestMusic();
        }
    }

    //When a tree falls, stops nomad chopping and allows them to plant seed in this area
    public void TreeFell()
    {
        canChop = false;
        canPlant = true;
        Debug.Log("TREE FELL");
    }

    
    //Set whether nomad can walk or not
    public static void setWalk(bool walkingEnabled)
    {
        myNomad.canWalk = walkingEnabled;
    }


    //When nomad faints, it moves him to the tent and sends him to sleep 
    public static void sendToTent()
    {
        myNomad.fainted = true;
        Debug.Log("Sending to tent: ");
        float x = 3261.91f;
        float y = 34.21f;
        float z = -3285.19f;
        myNomad.transform.position =  new Vector3(x,y,z);
        myNomad.GoToSleep();
    }

    //Sets axe and rod level to animation controller to display appropriate animation for chopping/fishing (holding version of correct tool)
    public static void setAxe(int level)
    {
        myNomad.myAnim.SetInteger("AxeLevel", level);
    }

    public static void setRod(int level)
    {
        myNomad.myAnim.SetInteger("RodLevel", level);
    }

    //This function is called when Nomad goes to sleep or faints
    async void GoToSleep()
    {
        GlobalValues.setSeenSleepText(); //Not used anymore         
        GlobalValues.clearInstructionText(); //Removes instruction text from screen
        canWalk = false; //Prevents nomad from moving
        //Fades in black screen
        Debug.Log("FADING: ");
        fading = true;
        await ScreenFader.Instance.FadeOut();

        //Play approriate forest music for degradation level
        playForestMusic();

        GlobalValues.setDayStartEnvScore(); //Set degredation level for start of the day
        TreeSpawner.SpawnTrees(); //Spawns trees again with correct degredation level 
        
        //waits a second
        await Task.Delay(1000); 

        TimeManager.sleep(); //Runs function to change day

        //Fades out black screen
        await ScreenFader.Instance.FadeIn();
        fading = false;

        // if(TimeManager.getDay() == 6)
        // {
        //     GlobalValues.setInstructionTextString("GAME OVER");
        // }

        if(TimeManager.getDay()<6) //If game not over, Nomad can walk again
        {
            
            canWalk = true;
            if(fainted) //If fainted, display this on screen
            {
                GlobalValues.setInstructionTextString("You fainted.");
                fainted = false;
            }    
        }

        
    }

    async void PlantPinecone() // function to plant a pinecone (seed)
    {
        int pineconeAmount = InventoryManager.getPineconeAmount(); 
        //First checks in you have any seeds
        if (pineconeAmount > 0)
        {
            currentlyPlanting = true;
            canWalk = false; //prevent nomad walking whilst planting
            myAnim.SetBool("Planting", true); //display planting animation
            await Task.Delay(3000);  //wait 3 seconds as you plant
            TreeSpawner.PlantSprout(transform.position.x, transform.position.z); //display sprout in planted location
            myAnim.SetBool("Planting", false); // stop planting animtion
            GlobalValues.setInstructionTextString("You planted a pinecone."); //display success message
            canWalk = true; //enable walking again
            await Task.Delay(2000); 
            canPlant = true; //enable the ability to plant again
            GlobalValues.clearInstructionText(); //Remove success message from screen

            GlobalValues.seedsPlantedIncrement(); //Increase tracker on amount of seeds planted
            currentlyPlanting = false;
        }
        else //If no seeds, display error text
        {
            GlobalValues.setInstructionTextString("No pinecones to plant");
            await Task.Delay(2000); 
            GlobalValues.clearInstructionText();
            canPlant = true;
        }
    }

    async void DisplayPineconeError()
    {
        GlobalValues.setInstructionTextString("You cannot plant here");
        await Task.Delay(2000); 
        GlobalValues.clearInstructionText(); //Remove error message from screen

    }

    async void Fish() //function to go fishing
    {
        GlobalValues.setSeenFishText();  //Not used anymore            

        int rodLevel = InventoryManager.getRodLevel(); //Gets what level of rod you have

        //Variables for how long it takes to catch something, how likely it is to catch rubbish, or catch anything at all
        int fishingWaitTime = 7000;
        int trashLikelihood = 3;
        int nothingLikelihood = 30;

        //These variables change depending on rod level and degradation score
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




        canWalk = false; //Prevents nomad walking whilst fishing
        myAnim.SetBool("Casting", true); //dispays casting animation
        await Task.Delay(1000); //waits a seconds
        SoundEffectManager.Play("Fishing"); //plays fishing sound
        myAnim.SetBool("Fishing", true); // displays fishing animation
        await Task.Delay(fishingWaitTime); //wait appropriate amount of time to catch something
        
        //Stops fishing animations
        myAnim.SetBool("Casting", false);
        myAnim.SetBool("Fishing", false);


        int trashRandom = Random.Range(0,trashLikelihood); //Use random number to calculate if you caught fish or rubbish
        int nothingRandom = Random.Range(0,nothingLikelihood); //Use random number to calculate if you caught anything

        if(nothingRandom == 0) //Caught nothing
        {
            GlobalValues.setInstructionTextString("You didn't catch anything.");  //Display erros message
        }
        else if (trashRandom == 2) //Caught rubbish
        {
            SoundEffectManager.Play("Success"); //plays success sound effect
            InventoryManager.changeTrashAmount(1); //Updates inventory
            GlobalValues.setInstructionTextString("You found some trash."); //Display success message
        }
        else //Caught fish
        {
            SoundEffectManager.Play("Success"); //plays success sound effect
            GlobalValues.fishCaughtIncrememt(); //Increments fish caught total
            InventoryManager.changeFishAmount(1); //Updates inventory
            GlobalValues.setInstructionTextString("You caught a fish.");  //Display success message   
        }

        
        canFish = true;
        canWalk = true;
        await Task.Delay(2000); 
        GlobalValues.clearInstructionText(); //Remove message from the screen
    }

    async void Recycle() //function called when recycling rubbish
    {
        GlobalValues.setSeenRecycleText(); //No longer used                 
        int trashAmount = InventoryManager.getTrashAmount(); //Check if you have any rubbish

        if (trashAmount == 0) //If no rubbish, display error message
        {
            GlobalValues.setInstructionTextString("You do not have any trash to recycle.");    
        }
        else
        {
            canRecycle = false; 
            InventoryManager.changeTrashAmount(-trashAmount); //Remove all rubbish from inventory
            GlobalValues.changeRecycledAmount(trashAmount); //Update recycled amount
            InventoryManager.changeCoinsAmount(trashAmount*5); //Get coins for each piece rubbish
            GlobalValues.setInstructionTextString("You recycled your trash."); //Show success message
            canRecycle = true; 
            await Task.Delay(2000); 
            GlobalValues.clearInstructionText(); //Remove message from screen
        }

        
    }

    private void playForestMusic() //Depending on degredation value, play appropriate version of forest song
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
