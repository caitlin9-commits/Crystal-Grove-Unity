using System.IO;
using UnityEngine;

public class NomadController : MonoBehaviour
{

    public float speed;
    public float groundDist;
    public bool chopping;


    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    private bool canChop;


    public Animator myAnim;
    [SerializeField] bool backTurned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        myAnim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
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

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

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

        Vector3 scale = transform.localScale;

        if (x < 0)
            scale.x = -Mathf.Abs(scale.x);
        else if (x > 0)
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;


    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            canChop = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            canChop = false;
        }
    }



}
