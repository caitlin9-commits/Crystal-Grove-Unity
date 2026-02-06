using UnityEngine;

public class GlobalValues : MonoBehaviour
{

    private static GlobalValues myGlobalValues;

    private int treesCut;


    private void Awake()
    {
        if(myGlobalValues == null)
        {
            myGlobalValues = this;
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
        treesCut = 0;
    }


    public static void treesCutIncrememt()
    {
        myGlobalValues.treesCut++;
    }

    public static int getTreesCut()
    {
        return myGlobalValues.treesCut;
    }
   
}
