using UnityEngine;

public class VillageTreeController : MonoBehaviour
{

    public Sprite crystalTree;
    private SpriteRenderer spriteR;

    private int currentDay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        currentDay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        int dayNow = TimeManager.getDay();

        if(dayNow != currentDay)
        {
            currentDay = dayNow;

            int envScore = GlobalValues.calculateEnvironmentScore();


            if(envScore >= 3)
            {
                spriteR.sprite = crystalTree;
            }
        }
    }
}
