using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager myCharacterManager;


    public GameObject Fachtna;
    public GameObject Bebhinn;


    private void Awake()
    {
        if(myCharacterManager == null)
        {
            myCharacterManager = this;
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

    //Day 2 character positions
    public static void setCharacterPositionsDay2()
    {
        Vector3 FachtnaCoords = new Vector3(3036.1f, 44.73f, -3405.6f);
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;

        Vector3 BebhinnCoords = new Vector3(3558f, 30.1f, -3202.5f);
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;
    }

    //Day 3 character positions
    public static void setCharacterPositionsDay3()
    {
        Vector3 FachtnaCoords = new Vector3(3036.1f, 44.73f, -3405.6f);
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;

        Vector3 BebhinnCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;
    }

    //Day 4 character positions
    public static void setCharacterPositionsDay4()
    {
        Vector3 FachtnaCoords = new Vector3(3036.1f, 44.73f, -3405.6f);
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;
    }

    //Day 5 character positions
    public static void setCharacterPositionsDay5()
    {
        Vector3 FachtnaCoords = new Vector3(3036.1f, 44.73f, -3405.6f);
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;
    }
}
