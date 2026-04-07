using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

 //This controller is linked to the time manager game object (invisible game object)
 //This controller manages in game passing of time and changing of days
public class TimeManager : MonoBehaviour
{
    //Skybox assets passed in
    [SerializeField] private Material skyboxNight;
    [SerializeField] private Material skyboxSunrise;
    [SerializeField] private Material skyboxDay;
    [SerializeField] private Material skyboxSunset;

    //Game objects for the day and time in top right corner of screen
    public TMP_Text dayText;
    public TMP_Text timeText;
 
    //Attempted to follow tutorial for transitioning between skyboxes however this library seems outdated and didn't work

    // [SerializeField] private Gradient graddientNightToSunrise;
    // [SerializeField] private Gradient graddientSunriseToDay;
    // [SerializeField] private Gradient graddientDayToSunset;
    // [SerializeField] private Gradient graddientSunsetToNight;
 
    //The main light source for tha game
    [SerializeField] private Light globalLight;

    private static TimeManager myTimeManager;

 
 //Getters and setters for the days,hours and minutes
    private double minutes;
 
    public double Minutes
    { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }
 
    private int hours = 5;
 
    public int Hours
    { get { return hours; } set { hours = value; OnHoursChange(value); } }
 
    private int days;
 
    public int Days
    { get { return days; } set { days = value; } }
 
    private float tempSecond;

    private bool clockActive ;
    private bool spokenToOldMan ;

//creates instance of the class, so it can be used in other classes
    private void Awake()
    {
        if(myTimeManager == null)
        {
            myTimeManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
 //Starts first day of game
    private void Start()
    {
        clockActive = true; //Clock is active
        spokenToOldMan = false; //Haven't spoken to Fachtna yet
        Days = 1; //Day is monday
        RenderSettings.skybox = skyboxNight; //Game starts at 5am, so is night time sky
        DynamicGI.UpdateEnvironment(); //Updates scene to have correctly set skybox
        displayDay(); //Display current day
        displayTime(); //Display current time

        //MOve characters in position for Day 1
        if(Days == 1){CharacterManager.setCharacterPositionsDay1(); }
    }


    public void Update()
    {

        //Time only starts moving once you have spoken to Fachtna
        if(clockActive && spokenToOldMan)
        {
            tempSecond += Time.deltaTime;
    
            if (tempSecond >= 1)
            {
                Minutes += 1.6; //1 second equals to 1.6 minutes in game
                //this equates to the day being ~15min long
                tempSecond = 0;
            }
        }
    }
 
    //Triggered as minutes change
    private void OnMinutesChange(double value)
    {


        //This main light position rotates as minutes to almost replicate the sun moving
        globalLight.transform.Rotate(Vector3.up, (1f / (1440f / 4f)) * 360f, Space.World);

        //As minutes hit 60, new hour
        if (value >= 60)
        {
            Hours++;
            minutes = 0;
        }
        //As hours hits 24, new day
        if (Hours >= 24)
        {
            // Hours = 0;
            // Days++;

            displayDay();//Show new days
            //collapse
            clockActive = false;//Clock not active as we transition between days
            NomadController.sendToTent();//Send nomad to tent at end of day, if not already there

        }

        displayTime(); //Show new time
    }

 
    //Called as hours change, based on hour of day, shows appropriate skybox
    //start of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=ihurRMKz2es&t=1s - but altered
    private void OnHoursChange(int value)
    {
        // Debug.Log("HOURS CHANGE "+ value);

        if (value == 5) 
        {
            StartCoroutine(TransitionSkybox(skyboxNight, skyboxSunrise, 10f));
            // StartCoroutine(LerpLight(graddientNightToSunrise, 10f));
        }
        else if (value == 8)
        {
            StartCoroutine(TransitionSkybox(skyboxSunrise, skyboxDay, 10f));
            // StartCoroutine(LerpLight(graddientSunriseToDay, 10f));
        }
        else if (value == 18)
        {
            StartCoroutine(TransitionSkybox(skyboxDay, skyboxSunset, 10f));
            // StartCoroutine(LerpLight(graddientDayToSunset, 10f));
        }
        else if (value == 20)
        {
            StartCoroutine(TransitionSkybox(skyboxSunset, skyboxNight, 10f));
            // StartCoroutine(LerpLight(graddientSunsetToNight, 10f));
        }
        //end of code from ThatOneUnityDev on YouTube, video link: https://www.youtube.com/watch?v=ihurRMKz2es&t=1s - but altered
        else if (value == 22)
        {
            //Plays bell to remind Nomad to go to sleep
            SoundEffectManager.Play("Bell");
        }


        
    }
 
    //Was being used for transitioning sky boxes, but didn't work    
    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        RenderSettings.skybox.SetTexture("_Texture1", b);
    }

    //Changes current skybox
    private IEnumerator TransitionSkybox(Material from, Material to, float time)
    {
        RenderSettings.skybox = to;
        DynamicGI.UpdateEnvironment();

        // float startExposure = from.GetFloat("_Exposure");
        // float endExposure = to.GetFloat("_Exposure");

        // for (float t = 0; t < time; t += Time.deltaTime)
        // {
        //     float lerp = t / time;
        //     RenderSettings.skybox.SetFloat(
        //         "_Exposure",
        //         Mathf.Lerp(startExposure, endExposure, lerp)
        //     );

        //     DynamicGI.UpdateEnvironment();
        //     yield return null;
        // }
        yield return null;
    }

 
    //Was being used for transitioning lighting, but didn't work    
    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
    }


    //Based on the hour and minutes, displays it in the appropriate format
    private void displayTime()
    {
               
        bool isAM = true;
        int formattedHours = Hours;
        if(formattedHours>12 && formattedHours<24)
        {
            formattedHours-=12;
            isAM = false;
        }
        if(formattedHours==12){isAM=false;}
        else if(formattedHours==24){
            isAM=true;
            formattedHours = 0;
        }

        string hoursStr = formattedHours.ToString();
        if(formattedHours<10){hoursStr="0"+hoursStr;}

        string minutesStr = "00";
        if(Minutes<15){minutesStr = "00";}
        else if(Minutes<30){minutesStr = "15";}
        else if(Minutes<45){minutesStr = "30";}
        else if(Minutes<60){minutesStr = "45";}
 
        timeText.text = hoursStr+":"+minutesStr+ " "+(isAM?"AM":"PM");

    }

    //Displays the day in the correct format
    private void displayDay()
    {
        switch (Days)
        {
            case 1: dayText.text = "Mon. 1"; break;
            case 2: dayText.text = "Tue. 2"; break;
            case 3: dayText.text = "Wed. 3"; break;
            case 4: dayText.text = "Thu. 4"; break;
            case 5: dayText.text = "Fri. 5"; break;

        }

    }


    //Called when nomad goes to sleep (naturally or fainted)
    public static void sleep()
    {

        Debug.Log("SLEEPING: ");

        //Sets time to 5am the next dat
        myTimeManager.minutes = 0;
        myTimeManager.Hours = 5;
        myTimeManager.Days++;

        //Spawns pinecones on the 2nd day
        //Sets characters positions each day
        if(myTimeManager.Days == 2){
            TreeSpawner.SpawnPineCones();
            CharacterManager.setCharacterPositionsDay2(); 
        }
        else if(myTimeManager.Days == 3){CharacterManager.setCharacterPositionsDay3(); }
        else if(myTimeManager.Days == 4){CharacterManager.setCharacterPositionsDay4(); }
        else if(myTimeManager.Days == 5){CharacterManager.setCharacterPositionsDay5(); }


        //If within 5 days, clock betcomes active after new day, and new day/time is displayed
        if(myTimeManager.Days<6)
        {
            myTimeManager.displayDay();
            myTimeManager.displayTime();
            myTimeManager.clockActive = true;    
        }
        //Otherwise, after 5 days, show the end of the game
        else
        {
            TitleScreensController.ShowEnding();
        }

        

    }

    //Sets if clock is active or not 
    public static void setClockActive(bool active)
    {
        myTimeManager.clockActive = active;
    }

    //Sets if you have spoken to Fachtna
   public static void spokeToOldMan()
    {
        myTimeManager.spokenToOldMan = true;
    }

    //Checks if you have spoken to Fachtna
    public static bool hasSpokenToOldMan()
    {
        return myTimeManager.spokenToOldMan;
    }

    //Gets current day
    public static int getDay()
    {
        return myTimeManager.Days;
    }
}
