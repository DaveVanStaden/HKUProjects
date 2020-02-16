using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Simple script that manages the options menu in the Main Menu scene. It displays the maxPoints and two options to either increase or decrease the required amount of points to win.
    /// </summary>

    //[SerializeField] private Text maxPointDisplay;
    [SerializeField] private Text maxTimeDisplay;
    [SerializeField] private Text secondsInputText;
    [SerializeField] private Text minutesInputText;

    private int min;
    private int sec;

    private void Update()
    {
        min = Mathf.FloorToInt(StaticSettings.maxTime / 60); //Every 60 seconds are divided into 1 minute
        sec = Mathf.FloorToInt(StaticSettings.maxTime % 60); //Every 60 seconds, the 60 seconds are divided by 60. Turning them into 0

        //maxPointDisplay.text = StaticSettings.maxPoints.ToString();
        maxTimeDisplay.text = min.ToString("00") + ":" + sec.ToString("00");

        if (StaticSettings.maxTime <= 5)
        {
            StaticSettings.maxTime = 5;
        }
    }

    public void IncreaseTimeByOneSecond()
    {
        StaticSettings.maxTime += 1;
    }

    public void DecreaseTimeByOneSecond()
    {
        StaticSettings.maxTime -= 1;
    }

    public void IncreaseTimeByTenSeconds()
    {
        StaticSettings.maxTime += 10;
    }

    public void DecreaseTimeByTenSeconds()
    {
        StaticSettings.maxTime -= 10;
    }

    public void IncreaseTimeBySixtySeconds()
    {
        StaticSettings.maxTime += 60;
    }

    public void DecreaseTimeBySixtySeconds()
    {
        StaticSettings.maxTime -= 60;
    }
}