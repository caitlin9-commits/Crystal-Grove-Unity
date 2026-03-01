using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public int treeCount = 5;
    public float spawnRadius = 10f;
    public float treeHeight = 1f;

    private static TreeSpawner myTreeSpawner;


     private void Awake()
    {
        if(myTreeSpawner == null)
        {
            myTreeSpawner = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        SpawnTrees();
    }

    public static void SpawnTrees()
    {
        int treesCut = GlobalValues.getTreesCut();
        double multiplier = 1;
        if(treesCut > 20){multiplier*=0.2;}
        else if(treesCut > 15){multiplier*=0.4;}
        else if(treesCut > 10){multiplier*=0.6;}
        else if(treesCut > 5){multiplier*=0.8;}

        int amountOfTrees = (int)(myTreeSpawner.treeCount*multiplier);

        for (int i = 0; i < amountOfTrees; i++)
        {
            Vector3 randomPos = myTreeSpawner.transform.position +
            new Vector3(
                Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius),
                0,
                Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius)
            );

            


            RaycastHit hit;
            if (Physics.Raycast(randomPos + Vector3.up * 50f, Vector3.down, out hit, 100f))
            {
                randomPos.y = hit.point.y;
            }

            randomPos.y += myTreeSpawner.treeHeight / 2f;

            Instantiate(myTreeSpawner.treePrefab, randomPos, Quaternion.identity);

        }
    }
}
