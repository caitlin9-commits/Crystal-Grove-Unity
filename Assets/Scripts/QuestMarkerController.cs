using UnityEngine;

public class QuestMarkerController : MonoBehaviour
{

    private static QuestMarkerController myQuestMarkerController;

    public GameObject FachtnaMarker;
    public GameObject BebhinnMarker;
    public GameObject BlaithinMarker;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
