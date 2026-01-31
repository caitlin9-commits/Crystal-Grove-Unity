using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tree : MonoBehaviour
{
    public float maxHealth = 100f;
    public float chopDamagePerSecond = 20f;

    private float currentHealth;
    private NomadController nomad;

    [Header("UI")]
    public Image healthFill; // drag the fill image here
    public Animator myAnim;
    [SerializeField] bool isHit;
    [SerializeField] bool isFalling;


    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // If nomad is nearby AND chopping → take damage
        if (nomad != null && nomad.chopping)
        {
            isHit=true;
            currentHealth -= chopDamagePerSecond * Time.deltaTime;
            UpdateHealthBar();

            Debug.Log("Tree is being chopped! Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                DestroyTree();
            }
        }
        else
        {
            isHit = false;
        }

        myAnim.SetBool("IsHit",isHit);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nomad"))
        {
            nomad = other.GetComponent<NomadController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Nomad"))
        {
            nomad = null;
        }
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void DestroyTree()
    {
        // Optional: play animation, spawn particles, drop wood, etc.
        StartCoroutine(DestroyAfterDelay());

    }

    IEnumerator DestroyAfterDelay()
    {
        isFalling = true;
        myAnim.SetBool("IsFalling", isFalling);

        yield return new WaitForSeconds(1f);

        SoundEffectManager.Play("Success");
        Destroy(gameObject);
    }

}
