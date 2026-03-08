using UnityEngine;

public class PineConeController : MonoBehaviour
{

    private bool canPickUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canPickUp && Input.GetKey(KeyCode.G))
        {
            canPickUp = false;
            PickUp();
        }
    }


    void PickUp()
    {
        Destroy(gameObject);
        GlobalValues.clearInstructionText();
        GlobalValues.changePineconeAmount(1);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nomad"))
        {
            canPickUp = true;
            GlobalValues.setInstructionText("Press","G","pickup");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Nomad"))
        {
            canPickUp = false;
        }
    }

}
