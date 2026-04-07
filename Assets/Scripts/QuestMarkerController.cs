using UnityEngine;

//This controller is linked to the QuestMarker game object. (This is an invisible game object)
//This controllet controls whether specific quest markers are shown or not.
public class QuestMarkerController : MonoBehaviour
{

    private static QuestMarkerController myQuestMarkerController;

    //These are the quest markers for specific characters, passed in in the scene.
    public GameObject FachtnaMarker;
    public GameObject BebhinnMarker;
    public GameObject BlaithinMarker;

    //creates instance of the class, so it can be used in other classes
    private void Awake()
    {
        if(myQuestMarkerController == null)
        {
            myQuestMarkerController = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Functions to remove specific question markers from the scene
    public static void removeFachtnaMarker()
    {
        myQuestMarkerController.FachtnaMarker.SetActive(false);
    }

    public static void removeBebhinnMarker()
    {
        myQuestMarkerController.BebhinnMarker.SetActive(false);
    }

    public static void removeBlaithinMarker()
    {
        myQuestMarkerController.BlaithinMarker.SetActive(false);
    }
}
