using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public class NomadController : MonoBehaviour
{

    private static NomadController myNomad;

    public float speed;
    public float groundDist;
    public bool chopping;


    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    private bool canChop;
    public bool canWalk;
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
        fading = false;
        myNomad = this;

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
                    movePos.y = hit.point.y + groundDist;
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
            Vector3 moveDir = new Vector3(x,0,y);
            rb.linearVelocity = moveDir*0;

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
            canChop = true;
        }
        if (other.CompareTag("Tent"))
        {
            RunFade();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            canChop = false;
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

    async void RunFade()
    {
        Debug.Log("FADING: ");
        fading = true;
        await ScreenFader.Instance.FadeOut();
        await Task.Delay(1000);
        await ScreenFader.Instance.FadeIn();
        fading = false;
    }

}
