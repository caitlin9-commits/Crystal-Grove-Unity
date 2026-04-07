using UnityEngine;

//This controller linked to the village tree game objects
//It manages the visual degradation of these objects
public class VillageTreeController : MonoBehaviour
{

    public Sprite healthyTree; //asset for healthy tree

    public Sprite crystalTree; //asset for crystal infected tree
    private SpriteRenderer spriteR; // the sprite renderer for the flower game object

    private int currentDay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>(); //get sprite renderer for current game object
        currentDay = 1;//initially set day to first day
    }

    // Update is called once per frame
    void Update()
    {
        int dayNow = TimeManager.getDay(); //get current in game day

        if(dayNow != currentDay) //recognise if day has changed
        {
            currentDay = dayNow; //set new current day


            int envScore = GlobalValues.calculateEnvironmentScore();//get your current environment score


            if(envScore >= 3) //if score is bad it sets game object's sprite renderer's sprite to infected flower asset
            {
                spriteR.sprite = crystalTree;
            }
            else //if score is not bad it sets game object's sprite renderer's sprite to healthy tree asset
            {
                spriteR.sprite = healthyTree;
            }
        }
    }
}
