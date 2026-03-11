using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager myCharacterManager;


    public GameObject Fachtna;


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


    public static void setCharacterPositionsDay1()
    {
        Vector3 temp = new Vector3(
            3036.1f,
            44.73f,
            -3405.6f
            );
        myCharacterManager.Fachtna.transform.position = temp;
    }
}
