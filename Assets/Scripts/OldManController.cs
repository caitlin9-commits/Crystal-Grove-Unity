using UnityEngine;

public class OldManController : MonoBehaviour
{
    public Animator myAnim;
    private int counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();  
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(counter < 1000)
        {
            myAnim.SetBool("isTree",false);
        }
        else
        {
            myAnim.SetBool("isTree",true);
        }

        if(counter > 2000)
        {
            counter = 0;
        }

        counter++;
    }
}
