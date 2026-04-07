using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This controller is linked to the tree object
//This controller manages teh animation of the tree and handles it being cut down
public class Tree : MonoBehaviour
{
    public TreeObject treeData;

    public float maxHealth = 100f; //Sets inital health of tree
    public float chopDamagePerSecond = 20f; //Sets how much health reduces per minute of chopping

    private float currentHealth; //Current health score of the tree
    private NomadController nomad;

    

    [Header("UI")]
    public Image healthFill; // drag the fill image here
    public Animator myAnim;
    [SerializeField] bool isHit;
    [SerializeField] bool isFalling;


    //When tree is created, it sets health to max, animation to idle
    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBar();


        //Based on amount of trees cut, the tree will appear different
        int cutCount = GlobalValues.getTreesCut();
        myAnim.SetInteger("CutCount", cutCount);
    }


    void Update()
    {
        //Axe level determines amount of damage when chopping
        int axeLevel = InventoryManager.getAxeLevel();
        if(axeLevel == 2){chopDamagePerSecond=30f;}
        else if(axeLevel == 3){chopDamagePerSecond=40f;}
        else if(axeLevel == 4){chopDamagePerSecond=50f;}

        // If nomad is nearby AND chopping → take damage
        if (nomad != null && nomad.chopping)
        {
            isHit=true;
            //Updates health of tree as you cause damage
            currentHealth -= chopDamagePerSecond * Time.deltaTime;
            UpdateHealthBar();

            // Debug.Log("Tree is being chopped! Health: " + currentHealth);

            //If no health remain, it destroys the tree
            if (currentHealth <= 0)
            {
                DestroyTree();
            }
        }
        else
        {
            isHit = false;
        }

        //Updates tree animation to the being hit animation
        myAnim.SetBool("IsHit",isHit);

    }

    //Recognises if Nomad has entered tree chopping zone
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nomad"))
        {
            nomad = other.GetComponent<NomadController>();
        }
    }

//Recognises if Nomad has exited tree chopping zone
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Nomad"))
        {
            nomad = null;
        }
    }

    //Sets value for a health bar (we don't actually display health bar)
    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }

    //Called when tree cut
    void DestroyTree()
    {    
        StartCoroutine(DestroyAfterDelay());

    }

    //Called when tree cut
    IEnumerator DestroyAfterDelay()
    {
        //Shows falling tree animation
        isFalling = true;
        myAnim.SetBool("IsFalling", isFalling);

        //Waits for a second
        yield return new WaitForSeconds(1f);

        //Sets tree as chopped for the tree spawner to know
        if (treeData != null)
        {
            treeData.isChopped = true;    
        }
        
        SoundEffectManager.Play("Success"); //Plays sound effect
        GlobalValues.treesCutIncrememt(); //Updates amount of trees cut
        GlobalValues.clearInstructionText(); //Removes instruction text from sreen
        InventoryManager.changeWoodAmount(5); //Adds wood to inventory 
        Destroy(gameObject); //Removes tree from scene

        
        int treesCut = GlobalValues.getTreesCut();
        Debug.Log("Tree cut count: " + treesCut);
        nomad.TreeFell(); //Informs nomad controller that tree has been cut down

    }

}
