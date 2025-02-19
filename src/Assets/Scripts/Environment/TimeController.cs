﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{

    private static TimeController instance;

    public static TimeController MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<TimeController>();
            }
            return instance;
        }
    }
    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;
    
    [SerializeField]
    private Light sunlight;

    [SerializeField]
    private float sunriseHour;
    
    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

    private DateTime currentTime;


    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    public void StartTimer()
    {
        
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
    
        sunriseTime = TimeSpan.FromHours(sunriseHour);

        sunsetTime = TimeSpan.FromHours(sunsetHour);

    }
    // Update is called once per frame
    void Update()
    {
        
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    public string getTime()
    {
        return currentTime.ToString("HH:mm tt");
    }
    public DateTime getCurrentDay()
    {
        return currentTime;
    }

    // public void resetQuestDay()
    // {
    //     QuestDay = 0;
    //     startDate = System.DateTime.Now;

    // }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        

        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }
    // public double GetDaysPassed()
    // {
    //     System.TimeSpan elapsed= currentTime.Subtract(startDate);
    //     QuestDay = elapsed.TotalDays;
    //     return QuestDay;
    // }

    private void RotateSun()
    {
        float sunLightRotation;

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);

        }

        sunlight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunlight.transform.forward, Vector3.down);

        sunlight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));

        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));

        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }

    public void SetCurrentDate (DateTime dt)
    {
        currentTime = dt;

        sunriseTime = TimeSpan.FromHours(sunriseHour);

        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

}
