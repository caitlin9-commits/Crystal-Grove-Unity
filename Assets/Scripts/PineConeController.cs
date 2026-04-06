using UnityEngine;


//This controller is linked to the pinecone game object
//This controller manages the collection of pinecones
public class PineConeController : MonoBehaviour
{

    private bool canPickUp = false; //Whether a pinecone can be picked up or not


    // Update is called once per frame
    void Update()
    {
        
        if (canPickUp && Input.GetKey(KeyCode.E)) //If pinecone can be picked up and action button clicked, pick it up
        {
            canPickUp = false;
            PickUp();
        }
    }


    void PickUp() //Function to pick up pinecone
    {
        Destroy(gameObject); //Removes pinecone
        GlobalValues.clearInstructionText(); //Removes text from screen
        InventoryManager.changePineconeAmount(1); //Add pinecone to inventory
    }

    
    void OnTriggerEnter(Collider other) //If nomad enters trigger zone for pinecone, allow to pick up and show message
    {
        if (other.CompareTag("Nomad"))
        {
            canPickUp = true;
            GlobalValues.setInstructionText("Press","E","pickup");
        }
    }

    void OnTriggerExit(Collider other) //If nomad exits trigger zone for pinecone, do not allow to pick up and hide message
    {
        if (other.CompareTag("Nomad"))
        {
            canPickUp = false;
            GlobalValues.clearInstructionText();
        }
    }

}
