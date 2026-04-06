using UnityEngine;


//This controller linked to the flower game objects
//It manages the visual degradation of these objects
public class FlowerController : MonoBehaviour
{
    public Sprite deadFlowers; //asset for dead flowers
    public Sprite aliveFlowers; //asset for healthy flowers
    private SpriteRenderer spriteR; // the sprite renderer for the flower game objet

    private int currentDay; // variable representing current in game day
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>(); //get sprite renderer for current game object
        currentDay = 1; //initially set day to first day
        aliveFlowers = spriteR.sprite; //get respective healthy flower asset for game object (there are 4 different colours)
    }

    // Update is called once per frame
    void Update()
    {
        int dayNow = TimeManager.getDay(); //get current in game day

        if(dayNow != currentDay) //recognise if day has changed
        {
            currentDay = dayNow; //set new current day

            int envScore = GlobalValues.calculateEnvironmentScore(); //get your current environment score


            if(envScore >= 3) //if score is bad it sets game object's sprite renderer's sprite to dead flower asset
            {
                spriteR.sprite = deadFlowers;
            }
            else{ //if score is not bad it sets game object's sprite renderer's sprite to appropriate healthy flower asset
                spriteR.sprite = aliveFlowers;
            }
        }
    }
}
