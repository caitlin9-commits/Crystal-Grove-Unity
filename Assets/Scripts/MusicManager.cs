using UnityEngine;

//This controller is linked the music manager game object (non-visible game object)
//This controller manages the music played in background
public class MusicManager : MonoBehaviour
{

    private static MusicManager myMusicManager;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip Village2;
    public AudioClip Village3;
    public AudioClip ForestMusic;
    public AudioClip Forest2;
    public AudioClip Forest3;
    // [SerializeField] private Slider mucisSlider;


//creates instance of the class, so it can be used in other classes
    private void Awake()
    {
        if(myMusicManager == null)
        {
            myMusicManager = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(ForestMusic != null)
        {
            Debug.Log("THERE IS MUSIC");
            PlayBackgroundMusic(false,ForestMusic);
        }
        else
        {
            Debug.Log("THERE IS NO MUSIC");
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public static void PlayNewSong(string songName)
    {
        AudioClip audioClip = null;
        if(songName == "ForestGood"){audioClip=myMusicManager.ForestMusic;}
        else if(songName == "ForestNeutral"){audioClip=myMusicManager.Forest2;}
        else if(songName == "ForestBad"){audioClip=myMusicManager.Forest3;}
        else if(songName == "VillageGood"){audioClip=myMusicManager.backgroundMusic;}
        else if(songName == "VillageNeutral"){audioClip=myMusicManager.Village2;}
        else if(songName == "VillageBad"){audioClip=myMusicManager.Village3;}


        PlayBackgroundMusic(true,audioClip);
    }

    public static void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if(audioClip != null)
        {
            myMusicManager.audioSource.clip = audioClip;
            myMusicManager.audioSource.Play();
        }
        else if(myMusicManager.audioSource.clip != null)
        {
            if (resetSong)
            {
                myMusicManager.audioSource.Stop();
            }
            myMusicManager.audioSource.Play();
        }
        
    }

    public static void PauseBackgroundMusic()
    {
        myMusicManager.audioSource.Pause();
    }
}
