using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{

    private static SoundEffectManager managerInstance;

    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;
    

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

    public static void Play(string soundName, float volume = 1f)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);
        if(audioClip != null)
        {
        
            audioSource.PlayOneShot(audioClip);

            if(soundName=="Footsteps" || soundName=="Glowing")
            {
                audioSource.volume = volume;    
            }
            

            
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
