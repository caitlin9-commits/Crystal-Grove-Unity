using UnityEngine;
using System.Threading.Tasks;

//This controller is linked to the screen fader game object
//This controller controls the screen fading in and out between days
public class ScreenFader : MonoBehaviour
{
   public static ScreenFader Instance;
   [SerializeField] CanvasGroup canvasGroup;
   [SerializeField] float fadeDuration = 0.5f;

//creates instance of the class, so it can be used in other classes
   private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    //The fader game object is just a black screen
    //This function changes it from being completely transparent to completely opaque
    //It takes the target opacity and sets the game object to this over a set duration
    async Task Fade(float targetTransparency)
    {
        float start = canvasGroup.alpha, t = 0;
        while(t<fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start,targetTransparency,t/fadeDuration);
            await Task.Yield();
        }
        canvasGroup.alpha = targetTransparency;
    }

    //Sets the screen to black and plays sound to represent new day
    public async Task FadeOut()
    {
        await Fade(1);
        SoundEffectManager.Play("NewDay");
    }

    //Removes the black screen
    public async Task FadeIn()
    {
        await Fade(0);
    }
   
}
