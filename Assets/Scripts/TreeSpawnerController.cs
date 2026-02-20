using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public int treeCount = 5;
    public float spawnRadius = 10f;

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
        for (int i = 0; i < myTreeSpawner.treeCount; i++)
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

            float treeHeight = 3f; 
            randomPos.y += treeHeight / 2f;

            Instantiate(myTreeSpawner.treePrefab, randomPos, Quaternion.identity);

        }
    }
}
