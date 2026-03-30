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
    public float spawnRadiusZ = 10f;

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
        // SpawnPineCones();
    }

    private void GenerateTreeLocations()
    {
        int amountOfTrees = myTreeSpawner.treeCount;

        for (int i = 0; i < amountOfTrees; i++)
        {

            bool foundAppropriateCoordinates = false;

            while(!foundAppropriateCoordinates)
            {
                float randomX = Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius);
                float randomZ = Random.Range(-myTreeSpawner.spawnRadiusZ, myTreeSpawner.spawnRadiusZ);

                Vector3 newPos = myTreeSpawner.transform.position +
                new Vector3(
                    randomX,
                    0,
                    randomZ
                );

                foundAppropriateCoordinates = IsAppropriateSpawnLocation(newPos.x,newPos.z);
            
                if(foundAppropriateCoordinates)
                {
                    trees.Add(new TreeObject{x=randomX,z=randomZ,isChopped = false});
                }
            }

            
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


            // if(myTreeSpawner.treeHealthState == 4)
            // {
            //     if(netTrees<=-2){myTreeSpawner.treeHealthState--;}
            // }
            // else if(myTreeSpawner.treeHealthState == 3)
            // {
            //     if(netTrees<=-10){myTreeSpawner.treeHealthState--;}
            //     else if(netTrees>-2){myTreeSpawner.treeHealthState++;}
            // }
            // else if(myTreeSpawner.treeHealthState == 2)
            // {
            //     if(netTrees<=-15){myTreeSpawner.treeHealthState--;}
            //     else if(netTrees>-10){myTreeSpawner.treeHealthState++;}
            // }
            // else if(myTreeSpawner.treeHealthState == 1)
            // {
            //     if(netTrees<=-20){myTreeSpawner.treeHealthState--;}
            //     else if(netTrees>-15){myTreeSpawner.treeHealthState++;}
            // }

            
            int envScore = GlobalValues.calculateEnvironmentScore();


            if(envScore == 1){treePrefab = myTreeSpawner.healthyTree;}
            else if(envScore == 2){treePrefab = myTreeSpawner.brownTree;}
            else if(envScore == 3){treePrefab = myTreeSpawner.deadTree;}
            else if(envScore == 4){treePrefab = myTreeSpawner.cystalTree;}
            

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

            if(envScore == 1){
                randomPos.y += myTreeSpawner.treeHeight / 2f;
            }
            else
            {
                randomPos.y += myTreeSpawner.treeHeight / 3f;
            }

            GameObject newTree = Instantiate(treePrefab, randomPos, Quaternion.identity);
            Tree treeComponent = newTree.GetComponent<Tree>();
            treeComponent.treeData = myTreeSpawner.trees[i];

            myTreeSpawner.spawnedTrees.Add(newTree);

        }
    }


    public static bool IsAppropriateSpawnLocation(float x, float z)
    {



        if(x>3246.45 && x<3282.43 && z<-3273.85 && z>-3303.66) //tent and sign + forest fairy
        {
            return false;
        }
        else if(x>3100.1 && x<3232 && z<-3238.6  && z>-3316.1) //forest pond + raised area
        {
            return false;
        }
        else if(x>3049.73 && x<3089.7 && z<-3327.22  && z>-3364.14) //forest bridge
        {
            return false;
        }
        else if(x>3026.4 && x>3068.2 && z<-3307  && z>-3329.9) //forest river (middle start)
        {
            return false;
        }
        else if(x>3007.4 && x>3026.4 && z<-3300.4  && z>-3325.3) //forest river (middle middle)
        {
            return false;
        }
        else if(x>2988.4 && x>3032.6 && z<-3294.3  && z>-3324.8) //forest river (another middle square)
        {
            return false;
        }
        else if(x>2970.1 && x<3018.1 && z<-3280.3  && z>-3318.7) //forest river mid-end square
        {
            return false;
        }
        else if(x>2953.6 && x<2996.6 && z<-3261.9  && z>-3286.1) //forest river (end corner)
        {
            return false;
        }
        else if(x>2978.5 && x<3026.7 && z<-3300.3  && z>-3369.1) //old man house
        {
            return false;
        }
        else if(x>3084.4 && x<3104.6 && z<-3328.9  && z>-3375.82) //Nomad spawner
        {
            return false;
        }
        else if(x>3007.8 && x<3027 && z<-3330.4  && z>-3369.2) //Old man house location
        {
            return false;
        }
        else if(x>3045.1 && x<3077.8 && z<-3307.1  && z>-3332) //river corner square
        {
            return false;
        }
        else{
            
            return true;
        }
    }

    public static void SpawnPineCones()
    {   

        Debug.Log("TREE SPAWNER LOC" + myTreeSpawner.transform.position.x + "   " +  myTreeSpawner.transform.position.z);

        for (int i = 0; i < 50; i++)
        {
        
            GameObject pineconePrefab = myTreeSpawner.pineconePrefab;
            
            bool foundAppropriateCoordinates = false;
            
            while(!foundAppropriateCoordinates)
            {
                float pineconeX = Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius);
                float pineconeZ = Random.Range(-myTreeSpawner.spawnRadiusZ, myTreeSpawner.spawnRadiusZ);

                foundAppropriateCoordinates = IsAppropriateSpawnLocation(pineconeX,pineconeZ);


                if(foundAppropriateCoordinates)
                {
                    Vector3 randomPos = myTreeSpawner.transform.position +
                        new Vector3(
                            pineconeX,
                            0,
                            pineconeZ
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


            
            

        }
    }


    public static void PlantSprout(float x, float z)
    {   
        

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

        plantPos.y=33.5f; //+= 2f / 2f;

        Debug.Log("SRPOUT PLANTED: "+plantPos.x+" "+plantPos.y+" "+plantPos.z);


        GameObject newSprout = Instantiate(sproutPrefab, plantPos, Quaternion.identity);
        myTreeSpawner.spawnedTrees.Add(newSprout);


        Vector3 local = new Vector3(x,0,z) - myTreeSpawner.transform.position;

        myTreeSpawner.sprouts.Add(new TreeObject{
            x = local.x,
            z = local.z,
            isChopped = false
        });
            
        InventoryManager.changePineconeAmount(-1);


        
    }




}


public class TreeObject
{

    public float x { get; set; }
    public float z { get; set; }
    public bool isChopped { get; set; }

}