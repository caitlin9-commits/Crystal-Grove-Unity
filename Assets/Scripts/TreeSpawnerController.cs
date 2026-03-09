using UnityEngine;
using System.Collections.Generic;
public class TreeSpawner : MonoBehaviour
{
    public GameObject healthyTree;
    public GameObject brownTree;
    public GameObject deadTree;
    public GameObject cystalTree;
    public GameObject pineconePrefab;
    public GameObject sproutPrefab;

    public int treeCount = 5;
    public float spawnRadius = 10f;
    public float treeHeight = 1f;

    private static TreeSpawner myTreeSpawner;

    private int treeHealthState = 4;

    private List<TreeObject> trees = new List<TreeObject>();
    private List<TreeObject> sprouts = new List<TreeObject>();
    private List<GameObject> spawnedTrees = new List<GameObject>();

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

        GenerateTreeLocations();
    }


    void Start()
    {
        SpawnTrees();
        SpawnPineCones();
    }

    private void GenerateTreeLocations()
    {
        int amountOfTrees = myTreeSpawner.treeCount;

        for (int i = 0; i < amountOfTrees; i++)
        {
            float randomX = Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius);
            float randomZ = Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius);
        
            trees.Add(new TreeObject{x=randomX,z=randomZ,isChopped = false});
        }
    }


    public static void SpawnTrees()
    {   
        // Wipe trees from before
        foreach (GameObject tree in myTreeSpawner.spawnedTrees)
        {
            Destroy(tree);
        }
        myTreeSpawner.spawnedTrees.Clear();

        
        foreach (TreeObject sprout in myTreeSpawner.sprouts)
        {
            myTreeSpawner.trees.Add(sprout);
            Debug.Log("BUDDED SRPOUT: "+sprout.x+" "+sprout.z);
        }

        myTreeSpawner.sprouts.Clear();


        for (int i = 0; i < myTreeSpawner.trees.Count; i++)
        {
           
            
        
            GameObject treePrefab = myTreeSpawner.healthyTree;

            int treesCut = GlobalValues.getTreesCut();
            int treesPlanted = 0;
            int netTrees = treesPlanted-treesCut;
            int day = TimeManager.getDay();


            if(myTreeSpawner.treeHealthState == 4)
            {
                if(netTrees<=-2){myTreeSpawner.treeHealthState--;}
            }
            else if(myTreeSpawner.treeHealthState == 3)
            {
                if(netTrees<=-10){myTreeSpawner.treeHealthState--;}
                else if(netTrees>-2){myTreeSpawner.treeHealthState++;}
            }
            else if(myTreeSpawner.treeHealthState == 2)
            {
                if(netTrees<=-15){myTreeSpawner.treeHealthState--;}
                else if(netTrees>-10){myTreeSpawner.treeHealthState++;}
            }
            else if(myTreeSpawner.treeHealthState == 1)
            {
                if(netTrees<=-20){myTreeSpawner.treeHealthState--;}
                else if(netTrees>-15){myTreeSpawner.treeHealthState++;}
            }


            if(myTreeSpawner.treeHealthState == 4){treePrefab = myTreeSpawner.healthyTree;}
            else if(myTreeSpawner.treeHealthState == 3){treePrefab = myTreeSpawner.brownTree;}
            else if(myTreeSpawner.treeHealthState == 2){treePrefab = myTreeSpawner.deadTree;}
            else if(myTreeSpawner.treeHealthState == 1){treePrefab = myTreeSpawner.cystalTree;}
            

            TreeObject tree = myTreeSpawner.trees[i];

            

            if (tree.isChopped){continue;} //Skip trees that have already been chopped, don't re-add 

             Vector3 randomPos = myTreeSpawner.transform.position +
            new Vector3(
                tree.x,
                0,
                tree.z
            );


            RaycastHit hit;
            int groundLayer = LayerMask.GetMask("Terrain");

            if (Physics.Raycast(randomPos + Vector3.up * 50f, Vector3.down, out hit, 100f,groundLayer))
            {
                randomPos.y = hit.point.y;
            }

            randomPos.y += myTreeSpawner.treeHeight / 2f;

            GameObject newTree = Instantiate(treePrefab, randomPos, Quaternion.identity);
            Tree treeComponent = newTree.GetComponent<Tree>();
            treeComponent.treeData = myTreeSpawner.trees[i];

            myTreeSpawner.spawnedTrees.Add(newTree);

        }
    }


    public static void SpawnPineCones()
    {   

        for (int i = 0; i < 10; i++)
        {
        
            GameObject pineconePrefab = myTreeSpawner.pineconePrefab;
            

            Vector3 randomPos = myTreeSpawner.transform.position +
            new Vector3(
                Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius),
                0,
                Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius)
            );

            RaycastHit hit;
            int groundLayer = LayerMask.GetMask("Terrain");

            if (Physics.Raycast(randomPos + Vector3.up * 50f, Vector3.down, out hit, 100f,groundLayer))
            {
                randomPos.y = hit.point.y;
            }

            randomPos.y += 2f / 2f;

            GameObject newPineCone = Instantiate(pineconePrefab, randomPos, Quaternion.identity);

        }
    }


    public static void PlantSprout(float x, float z)
    {   
        
        Debug.Log("SRPOUT: "+x+" "+z);

        GameObject sproutPrefab = myTreeSpawner.sproutPrefab;

        Vector3 plantPos = 
        new Vector3(
            x,
            0,
            z
        );

        RaycastHit hit;
        int groundLayer = LayerMask.GetMask("Terrain");

        if (Physics.Raycast(plantPos + Vector3.up * 50f, Vector3.down, out hit, 100f,groundLayer))
        {
            plantPos.y = hit.point.y;
        }

        plantPos.y += 2f / 2f;

        GameObject newSprout = Instantiate(sproutPrefab, plantPos, Quaternion.identity);
        myTreeSpawner.spawnedTrees.Add(newSprout);


        Vector3 local = new Vector3(x,0,z) - myTreeSpawner.transform.position;

        myTreeSpawner.sprouts.Add(new TreeObject{
            x = local.x,
            z = local.z,
            isChopped = false
        });
            
        GlobalValues.changePineconeAmount(-1);


        
    }




}


public class TreeObject
{

    public float x { get; set; }
    public float z { get; set; }
    public bool isChopped { get; set; }

}