using UnityEngine;
//used this library for managing conversations following intergration from this video, from SpeedTutor on YouTube, video link: https://www.youtube.com/watch?v=QPJHY6MPag4
//expanded on this intergration by following documentation for this library, Grass Hopper Dev documentation pdf: https://grasshopdev.github.io/docs/DialogueEditorDocumentation.pdf

using DialogueEditor;
//this class is linked to the conversation managers for each character
//it controls the starting and ending of conversations, as well as managing the logic for the conversations
public class ConversationStarter : MonoBehaviour
{ 
    //allows to pass in dialogue file for each day
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private NPCConversation day2Conversation;
    [SerializeField] private NPCConversation day3Conversation;
    [SerializeField] private NPCConversation day4Conversation;
    [SerializeField] private NPCConversation day5Conversation;
    
    bool hasSpoken = false; //boolean for if you have spoken to old man
   
    bool canSpeak = false; //boolean for whether you are in correct position to start conversation

    void Update()
    {
        if(canSpeak && Input.GetKey(KeyCode.X)) //allows you to speak after pressing X if in correct location
        {
            canSpeak = false; //prevents starting conversation while you are mid conversation
            ConversationStart(); //starts a conversation
        }
    }

    private void OnTriggerEnter(Collider other) //this function recognises if game object has entered trigger zone for conversation
    {

        
        if(other.CompareTag("Nomad")) //recognises if this was triggered by Nomad 
        {
            canSpeak = true; //allows Nomad to start conversation
            // if(!GlobalValues.checkSeenTalkText()) code removed : only showed instructional text the first time
            // {
                GlobalValues.setInstructionText("Press","X","talk");  // display instruction text to speak
            // }
            
        }
    }

    private void OnTriggerExit(Collider other) //this function recognises if game object has exited trigger zone for conversation
    {

        
        if(other.CompareTag("Nomad")) //recognises if this was triggered by Nomad 
        {
                NomadController.setWalk(true); //allows nomad to move, dont think this code is necessary, too scared to remove
                canSpeak = false; //can no longer speak, not in conversation zone
                GlobalValues.clearInstructionText(); //removes instruction text from screen
        }
    }

    private void ConversationStart() //starts conversation with character
    {
        GlobalValues.setSeenTalkText();          // this sets if you have seen the press x to talk, we no longer check this code   
        GlobalValues.clearInstructionText();  //removes the instruction text from screen
        NomadController.setWalk(false); //stops nomad from moving mid conversation
        int woodAmount = InventoryManager.getWoodAmount(); //gets how much wood the nomad has
        int fishAmount = InventoryManager.getFishAmount(); //gets how much fish the nomad has
        bool agreedToHelp = OldManController.checkIfAgreedToHelp(); //checks if you agreed to help fachtna

//used this library for managing conversations following intergration from this video, from SpeedTutor on YouTube, video link: https://www.youtube.com/watch?v=QPJHY6MPag4
//expanded on this intergration by following documentation for this library, Grass Hopper Dev documentation pdf: https://grasshopdev.github.io/docs/DialogueEditorDocumentation.pdf
        NPCConversation todayConversation; //Creates variable for conversation instance (uses imported DialogEditor library)

        int day = TimeManager.getDay(); //gets current in game day

        //Based on day, chooses appropriate passed in DialogEditor file
        if(day == 1){todayConversation=myConversation;}
        else if(day == 2){todayConversation=day2Conversation;}
        else if(day == 3){todayConversation=day3Conversation;}
        else if(day == 4){todayConversation=day4Conversation;}
        else if(day == 5){todayConversation=day5Conversation;}
        else{todayConversation=myConversation;}

        bool hasSpokenToFairy = GlobalValues.getHasSpokenToFairy(); //checks if you have spoken to the fairy (Blaithin)
        bool hasSpokenToBebhinnAboutFairy = GlobalValues.getHasSpokenToBebhinnAboutFairy(); // checks if you have spoked to Bebhinn about Blaithin
        bool hasWaterwheel = GlobalValues.checkForWaterwheel(); //checks if you have purchased the waterwheel

//used this library for managing conversations following intergration from this video, from SpeedTutor on YouTube, video link: https://www.youtube.com/watch?v=QPJHY6MPag4
//expanded on this intergration by following documentation for this library, Grass Hopper Dev documentation pdf: https://grasshopdev.github.io/docs/DialogueEditorDocumentation.pdf

        ConversationManager.Instance.StartConversation(todayConversation); //Uses dialog editor library to create new conversation manager
        ConversationManager.OnConversationEnded = ConversationEnd; //Adds hook to call once conversation has ended

        //Passes all the booleans into the conversation manager to dictates the appropriate converstion path 
        ConversationManager.Instance.SetBool("hasSpoken",hasSpoken); 
        ConversationManager.Instance.SetBool("agreedToHelp",agreedToHelp);
        ConversationManager.Instance.SetInt("woodCount",woodAmount);
        ConversationManager.Instance.SetInt("fishCount",fishAmount);
        ConversationManager.Instance.SetBool("hasWaterwheel",hasWaterwheel);
        ConversationManager.Instance.SetBool("hasSpokenFairy",hasSpokenToFairy);
        ConversationManager.Instance.SetBool("toldBebhinnAboutFairy",hasSpokenToBebhinnAboutFairy);
        ConversationManager.Instance.SetInt("environmentHealth",GlobalValues.calculateEnvironmentScore());

        //Console logs to check what data we are passing into conversation manager
        Debug.Log("NEW CONVERSTION STARTED");
        Debug.Log("Convo Details - hasSpoken "+hasSpoken);
        Debug.Log("Convo Details - agreedToHelp "+agreedToHelp);
        Debug.Log("Convo Details - woodCount "+woodAmount);
        Debug.Log("Convo Details - fishCount "+fishAmount);
        Debug.Log("Convo Details - hasWaterwheel "+hasWaterwheel);
        Debug.Log("Convo Details - hasSpokenFairy "+hasSpokenToFairy);
        Debug.Log("Convo Details - toldBebhinnAboutFairy "+hasSpokenToBebhinnAboutFairy);
        Debug.Log("Convo Details - environmentHealth "+GlobalValues.calculateEnvironmentScore());

    }


    private void ConversationEnd() //Called when a conversation ends
    {
        NomadController.setWalk(true); // allows Nomad to walk again
        hasSpoken = true; // sets has spoken to true, alters conversation path next time you speak to them
        Debug.Log("CONVO OVER");
    }

}
//used this library for managing conversations following intergration from this video, from SpeedTutor on YouTube, video link: https://www.youtube.com/watch?v=QPJHY6MPag4
//expanded on this intergration by following documentation for this library, Grass Hopper Dev documentation pdf: https://grasshopdev.github.io/docs/DialogueEditorDocumentation.pdf
