using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

 
public class TimeManager : MonoBehaviour
{
    [SerializeField] private Material skyboxNight;
    [SerializeField] private Material skyboxSunrise;
    [SerializeField] private Material skyboxDay;
    [SerializeField] private Material skyboxSunset;

    public TMP_Text dayText;
    public TMP_Text timeText;
 
    // [SerializeField] private Gradient graddientNightToSunrise;
    // [SerializeField] private Gradient graddientSunriseToDay;
    // [SerializeField] private Gradient graddientDayToSunset;
    // [SerializeField] private Gradient graddientSunsetToNight;
 
    [SerializeField] private Light globalLight;

    private static TimeManager myTimeManager;

 
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
 
    private void Start()
    {
        clockActive = true;
        spokenToOldMan = false;
        Days = 1;
        RenderSettings.skybox = skyboxNight;
        DynamicGI.UpdateEnvironment();
        displayDay();
        displayTime(); 

        if(Days == 1){CharacterManager.setCharacterPositionsDay1(); }
    }


    public void Update()
    {

        // Debug.Log("Time Manager: ",clockActive,spokenToOldMan);

        if(clockActive && spokenToOldMan)
        {
            tempSecond += Time.deltaTime;
    
            if (tempSecond >= 1)
            {
                Minutes += 1.6; //1 second equals to 1.6 minutes in game
                //this equates to the day being 15min long
                tempSecond = 0;
            }
        }
    }
 
    private void OnMinutesChange(double value)
    {

        // Debug.Log("MINUTES CHANGE " +value);

        globalLight.transform.Rotate(Vector3.up, (1f / (1440f / 4f)) * 360f, Space.World);
        if (value >= 60)
        {
            Hours++;
            minutes = 0;
        }
        if (Hours >= 24)
        {
            // Hours = 0;
            // Days++;

            displayDay();
            //collapse
            clockActive = false;
            NomadController.sendToTent();

        }

        displayTime();
    }

 
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
        else if (value == 22)
        {
            SoundEffectManager.Play("Bell");
        }


        
    }
 
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

 
    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
    }



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


    public static void sleep()
    {

        Debug.Log("SLEEPING: ");

        myTimeManager.minutes = 0;
        myTimeManager.Hours = 5;
        myTimeManager.Days++;

        if(myTimeManager.Days == 2){CharacterManager.setCharacterPositionsDay2(); }
        else if(myTimeManager.Days == 3){CharacterManager.setCharacterPositionsDay3(); }
        else if(myTimeManager.Days == 4){CharacterManager.setCharacterPositionsDay4(); }
        else if(myTimeManager.Days == 5){CharacterManager.setCharacterPositionsDay5(); }



        if(myTimeManager.Days<6)
        {
            myTimeManager.displayDay();
            myTimeManager.displayTime();
            myTimeManager.clockActive = true;    
        }

        

    }

   public static void spokeToOldMan()
    {
        myTimeManager.spokenToOldMan = true;
    }

    public static bool hasSpokenToOldMan()
    {
        return myTimeManager.spokenToOldMan;
    }

    public static int getDay()
    {
        return myTimeManager.Days;
    }
}
