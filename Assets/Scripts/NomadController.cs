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

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if(canWalk)
        {

            RaycastHit hit;
            Vector3 castPos = transform.position;
            castPos.y += 1;

            if(Physics.Raycast(castPos,-transform.up,out hit,Mathf.Infinity,terrainLayer))
            {
                if(hit.collider != null)
                {
                    Vector3 movePos = transform.position;
                   // movePos.y = hit.point.y + groundDist;
                    transform.position = movePos;
                }
            }

            

            myAnim.SetFloat("MoveSpeed",rb.linearVelocity.magnitude);

            if (rb.linearVelocity.magnitude < 0.1)
            {
                backTurned = false;
            }

            if(!backTurned && y>0){backTurned = true;}
            else if(backTurned && y<0){backTurned = false;}

            myAnim.SetBool("BackTurned",backTurned);

            Vector3 moveDir = new Vector3(x,0,y);
            rb.linearVelocity = moveDir*speed;
            

            Vector3 scale = transform.localScale;

            if (x < 0)
                scale.x = -Mathf.Abs(scale.x);
            else if (x > 0)
                scale.x = Mathf.Abs(scale.x);

            transform.localScale = scale;
        }
        else
        {
            //Vector3 moveDir = new Vector3(x,0,y);
           // rb.linearVelocity = moveDir*0;
           Vector3 velocity = rb.linearVelocity;
            velocity.x = x * speed;
            rb.linearVelocity = velocity;   

            myAnim.SetBool("BackTurned",false);
            myAnim.SetFloat("MoveSpeed",0);

        }




        if(canChop && Input.GetKey(KeyCode.Space))
        {
            chopSoundCounter++;
            if (chopSoundCounter > 300)
            {
                SoundEffectManager.Play("Chopping");
                chopSoundCounter = 0;    
            }
            
        }

        if(canSleep && Input.GetKey(KeyCode.Z))
        {
            canSleep = false;
            GoToSleep();
        }

        

        //CHOPPING
        chopping = canChop && Input.GetKey(KeyCode.Space);
        myAnim.SetBool("Chopping",chopping);

        



        // if(x != 0 && x <0)
        // {
        //     sr.flipX = true;
            
        // }
        // else if(x != 0 && x > 0)
        // {
        //     sr.flipX = false;
        // }

        


        // Console.WriteLine("Test log");

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
        float x = 5.8f;
        float y = 0.53f;
        float z = 23.8f;
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
