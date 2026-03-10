using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static MusicManager myMusicManager;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip backgroundMusicForest;
    // [SerializeField] private Slider mucisSlider;


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
        if(backgroundMusic != null)
        {
            Debug.Log("THERE IS MUSIC");
            PlayBackgroundMusic(false,backgroundMusic);
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
