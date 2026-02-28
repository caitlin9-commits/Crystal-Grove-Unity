using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    
    bool hasSpoken = false;
    bool canSpeak = false;

    void Update()
    {
        if(canSpeak && Input.GetKey(KeyCode.X))
        {
            canSpeak = false;
            ConversationStart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        
        if(other.CompareTag("Nomad"))
        {
                canSpeak = true;
                GlobalValues.setInstructionText("Press","X","talk");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        
        if(other.CompareTag("Nomad"))
        {
                NomadController.setWalk(true);
                canSpeak = false;
                GlobalValues.clearInstructionText();
        }
    }

    private void ConversationStart()
    {
        NomadController.setWalk(false);
        int woodAmount = GlobalValues.getWoodAmount();

        ConversationManager.Instance.StartConversation(myConversation);
        ConversationManager.OnConversationEnded = ConversationEnd;
        ConversationManager.Instance.SetBool("hasSpoken",hasSpoken);
        ConversationManager.Instance.SetInt("woodCount",woodAmount);
    }

    private void ConversationEnd()
    {
        NomadController.setWalk(true);
        hasSpoken = true;
        Debug.Log("CONVO OVER");
        NomadController.setWalk(true);
    }
}
