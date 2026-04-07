//start of code from Game Code Library  on YouTube, video link: https://www.youtube.com/watch?v=rAX_r0yBwzQ
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This controller is linked to the SoundEffectManager game object (invisible object)
//This controller stores all the different sound effect files
public class SoundEffectLibrary : MonoBehaviour
{
    //Array where we can link audio files in the scene
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    private Dictionary<string,List<AudioClip>> soundDictionary;

    private void Awake()
    {
        InitializeDictionary();
    }

    //Creates a dictionary object for the sound effects, where name is key and audio files are value
    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach(SoundEffectGroup soundEffectGroup in soundEffectGroups)
        {
            soundDictionary[soundEffectGroup.name] = soundEffectGroup.audioClips;
        }
    }

    //Receives name of sound effect and queries dictionary with it
    //Then returns a random audio clip from the array listed under this key
    public AudioClip GetRandomClip(string name)
    {
        if(soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDictionary[name];
            if(audioClips.Count > 0)
            {
                return audioClips[UnityEngine.Random.Range(0,audioClips.Count)];
            }
        }
        return null;
    }
}

//Class representing a sound effect group
//Has a name and a list of audio files for that sound effect
[System.Serializable]
public struct SoundEffectGroup
{
    public string name;
    public List<AudioClip> audioClips;
}
//end of code from Game Code Library  on YouTube, video link: https://www.youtube.com/watch?v=rAX_r0yBwzQ