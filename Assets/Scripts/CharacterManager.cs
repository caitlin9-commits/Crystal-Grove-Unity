using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager myCharacterManager;


    public GameObject Fachtna;
    public GameObject Bebhinn;
    public GameObject Bláithín;
    public GameObject Brian;
    public GameObject Odhrán;
    public GameObject Dearbhla;
    public GameObject Sadbh;
    public GameObject Ruairí;
    public GameObject Senán;
    public GameObject Aonghus;
    public GameObject Aoibheann;
    public GameObject Fergus;
    public GameObject Aoife;
    public GameObject Caitilín;
    public GameObject Cóem;
    public GameObject Brigid;




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



    //Day 1 character positions
    public static void setCharacterPositionsDay1()
    {
        Vector3 FachtnaCoords = new Vector3(3168.08f, 45.77f, -3392.9f); //beside house in forest
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;


        Vector3 BebhinnCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;


        Vector3 BrianCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Brian.transform.position = BrianCoords;


        Vector3 OdhránCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Odhrán.transform.position = OdhránCoords;


        Vector3 DearbhlaCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Dearbhla.transform.position = DearbhlaCoords;


        Vector3 SadbhCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Sadbh.transform.position = SadbhCoords;


        Vector3 SenánCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Senán.transform.position = SenánCoords;


        Vector3 AonghusCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Aonghus.transform.position = AonghusCoords;


        Vector3 AoibheannCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Aoibheann.transform.position = AoibheannCoords;


        Vector3 FergusCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Fergus.transform.position = FergusCoords;


        Vector3 AoifeCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Aoife.transform.position = AoifeCoords;


        Vector3 CaitilínCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Caitilín.transform.position = CaitilínCoords;


        Vector3 CóemCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Cóem.transform.position = CóemCoords;


        Vector3 BrigidCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Brigid.transform.position = BrigidCoords;



    }




    //Day 2 character positions
    public static void setCharacterPositionsDay2()
    {
        Vector3 FachtnaCoords = new Vector3(2794.88f, 45.77f, -3438.43f); //beside house in forest
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;

        Vector3 BebhinnCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;
    }



    //Day 3 character positions
    public static void setCharacterPositionsDay3()
    {
        Vector3 FachtnaCoords = new Vector3(2794.88f, 45.77f, -3438.43f); //beside house in forest
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;

        Vector3 BebhinnCoords = new Vector3(3560.92f, 33.14f, -3357.12f);
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;
    }



    //Day 4 character positions
    public static void setCharacterPositionsDay4()
    {
        Vector3 FachtnaCoords = new Vector3(2794.88f, 45.77f, -3438.43f); //beside house in forest
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;
    }



    //Day 5 character positions
    public static void setCharacterPositionsDay5()
    {
        Vector3 FachtnaCoords = new Vector3(2794.88f, 45.77f, -3438.43f);
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;
    }
}
