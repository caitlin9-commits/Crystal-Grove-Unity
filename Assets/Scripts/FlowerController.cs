using UnityEngine;

public class FlowerController : MonoBehaviour
{
    public Sprite deadFlowers;
    private SpriteRenderer spriteR;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int envScore = GlobalValues.calculateEnvironmentScore();

        if(envScore >= 3 || Input.GetKey(KeyCode.C))
        {
                spriteR.sprite = deadFlowers;
        }
    }
}
