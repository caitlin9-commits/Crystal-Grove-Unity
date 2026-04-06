using UnityEngine;
//this class is linked to the characters game object
// this game object contains all the game objects for the characters in the game
//this class sets the position for characters each day
public class CharacterManager : MonoBehaviour
{
    private static CharacterManager myCharacterManager; 

// all game objects for the characters
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



//creates instance of the class, so it can be used in other classes
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



    //Day 1 character positions
    public static void setCharacterPositionsDay1()
    { 
        Vector3 FachtnaCoords = new Vector3(3090.9f, 34.63f, -3327.7f);//beside bridge in forest
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;


        Vector3 BebhinnCoords = new Vector3(3536.74f, 20.8f, -3125.11f); //beside crystal
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;


        Vector3 BrianCoords = new Vector3(3493.01f, 21.4f, -3214.62f); //at the blacksmith
        myCharacterManager.Brian.transform.position = BrianCoords;


        Vector3 OdhránCoords = new Vector3(3549.3f, 21.24f, -3205.1f); //in village, beside bridge
        myCharacterManager.Odhrán.transform.position = OdhránCoords;


        Vector3 DearbhlaCoords = new Vector3(3549.91f, 21.2f, -3143.3f); //near crystal
        myCharacterManager.Dearbhla.transform.position = DearbhlaCoords;


        Vector3 SadbhCoords = new Vector3(3576.1f, 21.2f, -3242.2f); //in village
        myCharacterManager.Sadbh.transform.position = SadbhCoords;


        Vector3 SenánCoords = new Vector3(3417.9f, 21.2f, -3147.3f); //beside Caoimhe
        myCharacterManager.Senán.transform.position = SenánCoords;


        Vector3 AonghusCoords = new Vector3(3513.8f, 21.2f, -3116.6f); //in village, near crystal
        myCharacterManager.Aonghus.transform.position = AonghusCoords;


        Vector3 AoibheannCoords = new Vector3(3526.1f, 24.5f, -3247.5f); //in village
        myCharacterManager.Aoibheann.transform.position = AoibheannCoords;


        Vector3 FergusCoords = new Vector3(3415.23f, 21.2f, -3120.55f); //in village, at bell tower
        myCharacterManager.Fergus.transform.position = FergusCoords;


        Vector3 AoifeCoords = new Vector3(3405.73f, 21.2f, -3124.77f); //in village, at house
        myCharacterManager.Aoife.transform.position = AoifeCoords;


        Vector3 CaitilínCoords = new Vector3(3422.8f, 21.2f, -3140.3f); //beside Senan
        myCharacterManager.Caitilín.transform.position = CaitilínCoords;


        Vector3 CóemCoords = new Vector3(3401.92f, 27.7f, -3285f); // at house
        myCharacterManager.Cóem.transform.position = CóemCoords;


        Vector3 BrigidCoords = new Vector3(3407.78f, 27.7f, -3263.1f); //at house
        myCharacterManager.Brigid.transform.position = BrigidCoords;

        Vector3 RuairíCoords = new Vector3(3572.6f, 20.5f, -3226.6f); //at house
        myCharacterManager.Ruairí.transform.position = RuairíCoords;

        
    }








    //Day 2 character positions
    public static void setCharacterPositionsDay2()
    {
        Vector3 FachtnaCoords = new Vector3(3018.5f, 34.63f, -3340.5f); //beside house in forest
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;


        Vector3 BebhinnCoords = new Vector3(3536.74f, 20.8f, -3125.11f); //beside crystal
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;


        Vector3 BláithínCoords = new Vector3(3254.2f, 34.39f, -3281.92f); //in the forest
        myCharacterManager.Bláithín.transform.position = BláithínCoords;


        Vector3 BrianCoords = new Vector3(3493.01f, 21.4f, -3214.62f); //at the blacksmith
        myCharacterManager.Brian.transform.position = BrianCoords;


        Vector3 OdhránCoords = new Vector3(3476.7f, 21.24f, -3288.5f); //in village
        myCharacterManager.Odhrán.transform.position = OdhránCoords;


        Vector3 DearbhlaCoords = new Vector3(3500.6f, 21.2f, -3159.5f); //in village, at house
        myCharacterManager.Dearbhla.transform.position = DearbhlaCoords;


        Vector3 SadbhCoords = new Vector3(3526.1f, 24.5f, -3247.5f); //in village, at house
        myCharacterManager.Sadbh.transform.position = SadbhCoords;


        Vector3 SenánCoords = new Vector3(3557.2f, 21.2f, -3246.8f); //near house
        myCharacterManager.Senán.transform.position = SenánCoords;


        Vector3 AonghusCoords = new Vector3(3552.6f, 21.2f, -3209.4f); //in village
        myCharacterManager.Aonghus.transform.position = AonghusCoords;


        Vector3 AoibheannCoords = new Vector3(3502.36f, 24.79f, -3299.92f); //in village
        myCharacterManager.Aoibheann.transform.position = AoibheannCoords;


        Vector3 FergusCoords = new Vector3(3415.23f, 21.2f, -3120.55f); //in village, at bell tower
        myCharacterManager.Fergus.transform.position = FergusCoords;


        Vector3 AoifeCoords = new Vector3(3405.73f, 21.2f, -3124.77f); //in village, at house
        myCharacterManager.Aoife.transform.position = AoifeCoords;


        Vector3 CaitilínCoords = new Vector3(3422.8f, 21.2f, -3140.3f); //beside Senan
        myCharacterManager.Caitilín.transform.position = CaitilínCoords;


        Vector3 CóemCoords = new Vector3(3208.57f, 38.52f, -3272.76f); // in forest, beside pond
        myCharacterManager.Cóem.transform.position = CóemCoords;


        Vector3 BrigidCoords = new Vector3(3201.4f, 38.52f, -3278.4f); //in forest, beside pond
        myCharacterManager.Brigid.transform.position = BrigidCoords;

        Vector3 RuairíCoords = new Vector3(3572.6f, 20.5f, -3226.6f); //at house
        myCharacterManager.Ruairí.transform.position = RuairíCoords;

    }








    //Day 3 character positions
    public static void setCharacterPositionsDay3()
    {
        Vector3 FachtnaCoords = new Vector3(3018.5f, 34.63f, -3340.5f); //beside house in forest
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;


        Vector3 BebhinnCoords = new Vector3(3536.74f, 20.8f, -3125.11f); //beside crystal
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;


        Vector3 BláithínCoords = new Vector3(3254.2f, 34.39f, -3281.92f); //in the forest
        myCharacterManager.Bláithín.transform.position = BláithínCoords;


        Vector3 BrianCoords = new Vector3(3493.01f, 21.4f, -3214.62f); //at the blacksmith
        myCharacterManager.Brian.transform.position = BrianCoords;


        Vector3 OdhránCoords = new Vector3(3476.7f, 21.24f, -3288.5f); //in village
        myCharacterManager.Odhrán.transform.position = OdhránCoords;


        Vector3 DearbhlaCoords = new Vector3(3343.5f, 21.2f, -3294.2f); //beside forest entrance
        myCharacterManager.Dearbhla.transform.position = DearbhlaCoords;


        Vector3 SadbhCoords = new Vector3(3526.1f, 24.5f, -3247.5f); //in village, at house
        myCharacterManager.Sadbh.transform.position = SadbhCoords;


        Vector3 SenánCoords = new Vector3(3554.74f, 21.3f, -3153.99f); //near crystal, with Caitlin
        myCharacterManager.Senán.transform.position = SenánCoords;


        Vector3 AonghusCoords = new Vector3(3603.5f, 39.17f, -3209.4f); //on higher area of village
        myCharacterManager.Aonghus.transform.position = AonghusCoords;


        Vector3 AoibheannCoords = new Vector3(3502.36f, 24.79f, -3299.92f); //in village
        myCharacterManager.Aoibheann.transform.position = AoibheannCoords;


        Vector3 FergusCoords = new Vector3(3415.23f, 21.2f, -3120.55f); //in village, at bell tower
        myCharacterManager.Fergus.transform.position = FergusCoords;


        Vector3 AoifeCoords = new Vector3(3547.57f, 21.2f, -3206.38f); //in village, at bridge
        myCharacterManager.Aoife.transform.position = AoifeCoords;


        Vector3 CaitilínCoords = new Vector3(3559.26f, 21.2f, -3148.43f); //near crystal, with senan
        myCharacterManager.Caitilín.transform.position = CaitilínCoords;


        Vector3 CóemCoords = new Vector3(3401.92f, 27.7f, -3285f); // at house
        myCharacterManager.Cóem.transform.position = CóemCoords;


        Vector3 BrigidCoords = new Vector3(3201.4f, 38.52f, -3278.4f); //in forest, beside pond
        myCharacterManager.Brigid.transform.position = BrigidCoords;

        Vector3 RuairíCoords = new Vector3(3572.6f, 20.5f, -3226.6f); //at house
        myCharacterManager.Ruairí.transform.position = RuairíCoords;

    }







    //Day 4 character positions
    public static void setCharacterPositionsDay4()
    {
        Vector3 FachtnaCoords = new Vector3(3018.5f, 34.63f, -3340.5f); //beside house in forest
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;


        Vector3 BebhinnCoords = new Vector3(3536.74f, 20.8f, -3125.11f); //beside crystal
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;


        Vector3 BláithínCoords = new Vector3(3254.2f, 34.39f, -3281.92f); //in the forest
        myCharacterManager.Bláithín.transform.position = BláithínCoords;


        Vector3 BrianCoords = new Vector3(3493.01f, 21.4f, -3214.62f); //at the blacksmith
        myCharacterManager.Brian.transform.position = BrianCoords;


        Vector3 OdhránCoords = new Vector3(3495.88f, 21.24f, -3228.8f); //in village
        myCharacterManager.Odhrán.transform.position = OdhránCoords;


        Vector3 DearbhlaCoords = new Vector3(3343.5f, 21.2f, -3294.2f); //beside forest entrance
        myCharacterManager.Dearbhla.transform.position = DearbhlaCoords;


        Vector3 SadbhCoords = new Vector3(3549.11f, 21.3f, -3159.06f); //in village, near crystal
        myCharacterManager.Sadbh.transform.position = SadbhCoords;


        Vector3 SenánCoords = new Vector3(3554.74f, 21.3f, -3153.99f); //in village, beside Sadbh
        myCharacterManager.Senán.transform.position = SenánCoords;


        Vector3 AonghusCoords = new Vector3(3481.17f, 21.27f, -3301.83f); //in village
        myCharacterManager.Aonghus.transform.position = AonghusCoords;


        Vector3 AoibheannCoords = new Vector3(3511.21f, 21.2f, -3117.07f); //in village, near crystal
        myCharacterManager.Aoibheann.transform.position = AoibheannCoords;


        Vector3 FergusCoords = new Vector3(3415.23f, 21.2f, -3120.55f); //in village, at bell tower
        myCharacterManager.Fergus.transform.position = FergusCoords;


        Vector3 AoifeCoords = new Vector3(3374.6f, 21.2f, -3177.2f); //at edge of the village, with Caitlin
        myCharacterManager.Aoife.transform.position = AoifeCoords;


        Vector3 CaitilínCoords = new Vector3(3378.9f, 21.2f, -3185f); //at edge of village, with Aoife
        myCharacterManager.Caitilín.transform.position = CaitilínCoords;


        Vector3 CóemCoords = new Vector3(3208.57f, 38.52f, -3272.76f); // in forest, beside pond
        myCharacterManager.Cóem.transform.position = CóemCoords;


        Vector3 BrigidCoords = new Vector3(3201.4f, 38.52f, -3278.4f); //in forest, beside pond
        myCharacterManager.Brigid.transform.position = BrigidCoords;

        Vector3 RuairíCoords = new Vector3(3572.6f, 20.5f, -3226.6f); //at house
        myCharacterManager.Ruairí.transform.position = RuairíCoords;

    }









    //Day 5 character positions
    public static void setCharacterPositionsDay5()
    {
        Vector3 FachtnaCoords = new Vector3(3529.1f, 21.27f, -3113.8f); //beside the crystal
        myCharacterManager.Fachtna.transform.position = FachtnaCoords;


        Vector3 BebhinnCoords = new Vector3(3536.74f, 20.8f, -3125.11f); //beside crystal
        myCharacterManager.Bebhinn.transform.position = BebhinnCoords;


        Vector3 BláithínCoords = new Vector3(3254.2f, 34.39f, -3281.92f); //in the forest
        myCharacterManager.Bláithín.transform.position = BláithínCoords;


        Vector3 BrianCoords = new Vector3(3493.01f, 21.4f, -3214.62f); //at the blacksmith
        myCharacterManager.Brian.transform.position = BrianCoords;


        Vector3 OdhránCoords = new Vector3(3544.63f, 24.2f, -3176.8f); //in village, on bridge
        myCharacterManager.Odhrán.transform.position = OdhránCoords;


        Vector3 DearbhlaCoords = new Vector3(3547.74f, 21.2f, -3133.16f); //beside crystal
        myCharacterManager.Dearbhla.transform.position = DearbhlaCoords;


        Vector3 SadbhCoords = new Vector3(3549.11f, 21.3f, -3159.06f); //in village, near crystal
        myCharacterManager.Sadbh.transform.position = SadbhCoords;


        Vector3 SenánCoords = new Vector3(3554.74f, 21.3f, -3153.99f); //in village, beside Sadbh
        myCharacterManager.Senán.transform.position = SenánCoords;


        Vector3 AonghusCoords = new Vector3(3547.2f, 40.31f, -3072.69f); //in higher area of village
        myCharacterManager.Aonghus.transform.position = AonghusCoords;


        Vector3 AoibheannCoords = new Vector3(3541.4f, 21.2f, -3090.1f); //in village, near crystal
        myCharacterManager.Aoibheann.transform.position = AoibheannCoords;


        Vector3 FergusCoords = new Vector3(3415.23f, 21.2f, -3120.55f); //in village, at bell tower
        myCharacterManager.Fergus.transform.position = FergusCoords;


        Vector3 AoifeCoords = new Vector3(3524.2f, 21.2f, -3170.4f); //near crystal
        myCharacterManager.Aoife.transform.position = AoifeCoords;


        Vector3 CaitilínCoords = new Vector3(3559.26f, 21.2f, -3148.43f); //near crystal, with Senan and Sadbh
        myCharacterManager.Caitilín.transform.position = CaitilínCoords;


        Vector3 CóemCoords = new Vector3(3208.57f, 38.52f, -3272.76f); // in forest, beside pond
        myCharacterManager.Cóem.transform.position = CóemCoords;


        Vector3 BrigidCoords = new Vector3(3201.4f, 38.52f, -3278.4f); //in forest, beside pond
        myCharacterManager.Brigid.transform.position = BrigidCoords;

        Vector3 RuairíCoords = new Vector3(3568.31f, 20.5f, -3156.3f); //at house
        myCharacterManager.Ruairí.transform.position = RuairíCoords;
    }
}



