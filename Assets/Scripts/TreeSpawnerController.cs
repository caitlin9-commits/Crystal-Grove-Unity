using UnityEngine;
using System.Collections.Generic;
public class TreeSpawner : MonoBehaviour
{
    public GameObject healthyTree;
    public GameObject brownTree;
    public GameObject deadTree;
    public GameObject cystalTree;

    public int treeCount = 5;
    public float spawnRadius = 10f;
    public float treeHeight = 1f;

    private static TreeSpawner myTreeSpawner;

    private int treeHealthState = 4;

    private List<TreeObject> trees = new List<TreeObject>();
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
}


public class TreeObject
{

    public float x { get; set; }
    public float z { get; set; }
    public bool isChopped { get; set; }
}