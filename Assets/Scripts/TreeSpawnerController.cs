using UnityEngine;
using System.Collections.Generic;

//This controller is linked to the tree spawner game object
//This controller manages the spawn of multiple trees and pinecones
//In retrospect, this seemed a quicker way to generate lots of choppable trees in the forest,
//however we may have been better suited just manually placing them in the scene like with the non choppable background trees
public class TreeSpawner : MonoBehaviour
{
    //Receives game objects for the different health level of trees, this are prefabs
    public GameObject healthyTree;
    public GameObject brownTree;
    public GameObject deadTree;
    public GameObject cystalTree;
    public GameObject pineconePrefab;
    public GameObject sproutPrefab;

    //Passed in values for how many trees to spawn and how big a radius to spawn them
    public int treeCount = 5;
    public float spawnRadius = 10f;
    public float spawnRadiusZ = 10f;

    public float treeHeight = 1f;

    private static TreeSpawner myTreeSpawner;

    private int treeHealthState = 4;

    //Arrays representing tree locations, spawened trees and where seeds have planted
    private List<TreeObject> trees = new List<TreeObject>();
    private List<TreeObject> sprouts = new List<TreeObject>();
    private List<GameObject> spawnedTrees = new List<GameObject>();
    

//creates instance of the class, so it can be used in other classes
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
        
        //Before it starts, set all the positions for trees
         GenerateTreeLocations();


    }


    void Start()
    {
       
        SpawnTrees(); //Add all trees to the scene
        // SpawnPineCones(); //Previously added all pinecones to scene, but now do that on day 2
    }

    //This function sets all the random locations for the trees
    //It spawns the trees in these locations each day so tree remaining in same space a degradation changes
    private void GenerateTreeLocations()
    {
        int amountOfTrees = myTreeSpawner.treeCount; //Get amount of trees to spawn from input

        for (int i = 0; i < amountOfTrees; i++) //Loops through this amount of trees
        {

            bool foundAppropriateCoordinates = false;

            //Keeps looping until it finds an appropriate location to place tree
            while(!foundAppropriateCoordinates)
            {
                //Gets random position for tree based on the passed in bounds for the radius
                float randomX = Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius);
                float randomZ = Random.Range(-myTreeSpawner.spawnRadiusZ, myTreeSpawner.spawnRadiusZ);

                Vector3 newPos = myTreeSpawner.transform.position +
                new Vector3(
                    randomX,
                    0,
                    randomZ
                );

                //Checks if this is appopriate place to position the tree
                foundAppropriateCoordinates = IsAppropriateSpawnLocation(newPos.x,newPos.z);
            
                //If appropriate location, adds tree to array
                if(foundAppropriateCoordinates)
                {
                    trees.Add(new TreeObject{x=randomX,z=randomZ,isChopped = false});
                }
            }

            
        }
    }


    //This function spawns the trees in the generated locations
    public static void SpawnTrees()
    {   
        // Wipe trees objects from before
        foreach (GameObject tree in myTreeSpawner.spawnedTrees)
        {
            Destroy(tree);
        }
        myTreeSpawner.spawnedTrees.Clear();

        //Goes through seeds planted and spawns trees in these locations
        foreach (TreeObject sprout in myTreeSpawner.sprouts)
        {
            myTreeSpawner.trees.Add(sprout);
            Debug.Log("BUDDED SRPOUT: "+sprout.x+" "+sprout.z);
        }

        myTreeSpawner.sprouts.Clear(); //Removes sprouts from scene


        //Goes through list of trees and adds game objects here
        for (int i = 0; i < myTreeSpawner.trees.Count; i++)
        {
           
            
        
            GameObject treePrefab = myTreeSpawner.healthyTree;

            int treesCut = GlobalValues.getTreesCut();
            int treesPlanted = 0;
            int netTrees = treesPlanted-treesCut;
            int day = TimeManager.getDay();


            //Old logic for determining what version of tree to display

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


            //Use environment score to determine which version of tree to add
            
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


            //Attempts to set Y position of tree to be at ground level by making it relative to objects with 'terrain' tag
            RaycastHit hit;
            int groundLayer = LayerMask.GetMask("Terrain");

            if (Physics.Raycast(randomPos + Vector3.up * 50f, Vector3.down, out hit, 100f,groundLayer))
            {
                randomPos.y = hit.point.y;
            }
            randomPos.y += myTreeSpawner.treeHeight / 2f;
            

            //Create tree game object and adds to scene
            GameObject newTree = Instantiate(treePrefab, randomPos, Quaternion.identity);
            Tree treeComponent = newTree.GetComponent<Tree>();
            treeComponent.treeData = myTreeSpawner.trees[i]; //This is added so tree controller class can mark tree as chopped

            myTreeSpawner.spawnedTrees.Add(newTree);

        }
    }


    //This function changes if coordinates are appropriate location to spawn tree or pinecone
    // We want to avoid spawning them in water or in buildings
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

    //Function to add pinecones to the scene
    public static void SpawnPineCones()
    {   

        Debug.Log("TREE SPAWNER LOC" + myTreeSpawner.transform.position.x + "   " +  myTreeSpawner.transform.position.z);

        //Adds 50 pinecones
        for (int i = 0; i < 50; i++)
        {
        
            //Gets prefab for pinecone (linked in scene)
            GameObject pineconePrefab = myTreeSpawner.pineconePrefab;
            
            bool foundAppropriateCoordinates = false;
            
            //Keeps looping until it finds an appropriate location to place pinecone
            while(!foundAppropriateCoordinates)
            {
                //Gets random position for pinecone based on the passed in bounds for the radius
                float pineconeX = Random.Range(-myTreeSpawner.spawnRadius, myTreeSpawner.spawnRadius);
                float pineconeZ = Random.Range(-myTreeSpawner.spawnRadiusZ, myTreeSpawner.spawnRadiusZ);

                
                 Vector3 randomPos = myTreeSpawner.transform.position +
                    new Vector3(
                        pineconeX,
                        0,
                        pineconeZ
                    );

                //Checks if this is appopriate place to position the pinecone
                foundAppropriateCoordinates = IsAppropriateSpawnLocation(randomPos.x,randomPos.z);


                //If appropriate location, adds pinecone to scene
                if(foundAppropriateCoordinates)
                {
                   
                    //Attempts to set Y position of pinecone to be at ground level by making it relative to objects with 'terrain' tag
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


    //Function called when nomad has planted a seed, receives coordinates of where planted
    public static void PlantSprout(float x, float z)
    {   
        
        //Gets prefab for sprout (linked in scene)
        GameObject sproutPrefab = myTreeSpawner.sproutPrefab;

        Vector3 plantPos = 
        new Vector3(
            x,
            0,
            z
        );

        //Attempts to set Y position of sprout to be at ground level by making it relative to objects with 'terrain' tag
        RaycastHit hit;
        int groundLayer = LayerMask.GetMask("Terrain");

        if (Physics.Raycast(plantPos + Vector3.up * 50f, Vector3.down, out hit, 100f,groundLayer))
        {
            plantPos.y = hit.point.y;
        }

        plantPos.y=33.5f; //+= 2f / 2f;

        Debug.Log("SRPOUT PLANTED: "+plantPos.x+" "+plantPos.y+" "+plantPos.z);


        //Adds sprout to scene
        GameObject newSprout = Instantiate(sproutPrefab, plantPos, Quaternion.identity);

        //Saves location to array, so can turn into tree later
        myTreeSpawner.spawnedTrees.Add(newSprout); 


        Vector3 local = new Vector3(x,0,z) - myTreeSpawner.transform.position;

        myTreeSpawner.sprouts.Add(new TreeObject{
            x = local.x,
            z = local.z,
            isChopped = false
        });
        
        //Removes seed from inventory
        InventoryManager.changePineconeAmount(-1);


        
    }




}


public class TreeObject
{

    public float x { get; set; }
    public float z { get; set; }
    public bool isChopped { get; set; }

}