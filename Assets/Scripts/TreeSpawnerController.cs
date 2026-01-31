using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public int treeCount = 5;
    public float spawnRadius = 10f;

    void Start()
    {
        SpawnTrees();
    }

    void SpawnTrees()
    {
        for (int i = 0; i < treeCount; i++)
        {
            Vector3 randomPos = transform.position +
            new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,
                Random.Range(-spawnRadius, spawnRadius)
            );

            


            RaycastHit hit;
            if (Physics.Raycast(randomPos + Vector3.up * 50f, Vector3.down, out hit, 100f))
            {
                randomPos.y = hit.point.y;
            }

            float treeHeight = 3f; 
            randomPos.y += treeHeight / 2f;

            Instantiate(treePrefab, randomPos, Quaternion.identity);

        }
    }
}
