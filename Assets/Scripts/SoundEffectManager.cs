using UnityEngine;
//start of code from Game Code Library  on YouTube, video link: https://www.youtube.com/watch?v=rAX_r0yBwzQ


//This controller is linked to the SoundEffectManager game object (invisible object)
//This controller plays a specific sound effect
public class SoundEffectManager : MonoBehaviour
{

    private static SoundEffectManager managerInstance;

    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;
    
    //creates instance of the class, so it can be used in other classes
    private void Awake()
    {
        if(managerInstance == null)
        {
            managerInstance = this;
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Takes in specific sound effect name and volume and plays it at this volume
    public static void Play(string soundName, float volume = 1f)
    {
        //Finds sound effect under this name
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);
        if(audioClip != null)
        {
        
            audioSource.PlayOneShot(audioClip); //If found, plays sound effect

            //For certain sound effects, you can set volume
            if(soundName=="Footsteps" || soundName=="Glowing")
            {
                audioSource.volume = volume;    
            }
            

            
        }
    }

   
}
//end of code from Game Code Library  on YouTube, video link: https://www.youtube.com/watch?v=rAX_r0yBwzQ