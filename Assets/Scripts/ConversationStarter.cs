using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private NPCConversation day2Conversation;
    [SerializeField] private NPCConversation day3Conversation;
    [SerializeField] private NPCConversation day4Conversation;
    [SerializeField] private NPCConversation day5Conversation;
    
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
            if(!GlobalValues.checkSeenTalkText())
            {
                GlobalValues.setInstructionText("Press","X","talk");  
            }
            
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
        GlobalValues.setSeenTalkText();             
        GlobalValues.clearInstructionText();  
        NomadController.setWalk(false);
        int woodAmount = InventoryManager.getWoodAmount();
        bool agreedToHelp = OldManController.checkIfAgreedToHelp();

        NPCConversation todayConversation;

        int day = TimeManager.getDay();

        if(day == 1){todayConversation=myConversation;}
        else if(day == 2){todayConversation=day2Conversation;}
        else if(day == 3){todayConversation=day3Conversation;}
        else if(day == 4){todayConversation=day4Conversation;}
        else if(day == 5){todayConversation=day5Conversation;}
        else{todayConversation=myConversation;}

        ConversationManager.Instance.StartConversation(todayConversation);
        ConversationManager.OnConversationEnded = ConversationEnd;
        ConversationManager.Instance.SetBool("hasSpoken",hasSpoken);
        ConversationManager.Instance.SetBool("agreedToHelp",agreedToHelp);
        ConversationManager.Instance.SetInt("woodCount",woodAmount);

        ConversationManager.Instance.SetInt("environmentHealth",GlobalValues.calculateEnvironmentScore());

    }

    private void ConversationEnd()
    {
        NomadController.setWalk(true);
        hasSpoken = true;
        Debug.Log("CONVO OVER");
        NomadController.setWalk(true);
    }

}
