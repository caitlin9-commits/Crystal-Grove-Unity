using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    
    bool hasSpoken = false;

    private void OnTriggerEnter(Collider other)
    {

        
        if(other.CompareTag("Nomad"))
        {
                NomadController.setWalk(false);
            // if(Input.GetKeyDown(KeyCode.F))
            // {

                int woodAmount = GlobalValues.getWoodAmount();

                ConversationManager.Instance.StartConversation(myConversation);
                ConversationManager.OnConversationEnded = ConversationEnd;
                ConversationManager.Instance.SetBool("hasSpoken",hasSpoken);
                ConversationManager.Instance.SetInt("woodCount",woodAmount);
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        
        if(other.CompareTag("Nomad"))
        {
                NomadController.setWalk(true);
           
        }
    }

    private void ConversationEnd()
    {
        hasSpoken = true;
        Debug.Log("CONVO OVER");
        NomadController.setWalk(true);
    }
}
