using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Text maxTimeText;
    [SerializeField] private GameObject menuHolder;
    public bool isPlayable;
    private int minMax;
    private int secMax;

    private void Start()
    {
        //Time.timeScale = 0f;
        isPlayable = false;
    }

    private void Update()
    {
        //At the start of the game, a timer is set. this is to display those minutes and seconds.
        minMax = Mathf.FloorToInt(StaticSettings.maxTime / 60);
        secMax = Mathf.FloorToInt(StaticSettings.maxTime % 60);
        maxTimeText.text = minMax.ToString("00") + ":" + secMax.ToString("00");
        //this is so that the timer wont be lower then 10 seconds.
        if (StaticSettings.maxTime <= 10)
        {
            StaticSettings.maxTime = 10;
        }
        //After the game becomes playable, the menu is set to active and the game begins. 
        if (isPlayable)
        {
            menuHolder.SetActive(false);
        }
        else
        {
            menuHolder.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopGame();
        }
    }
    //the time can be made higher by 1 second and 1 minute, or made lower by 1 second or 1 minute.
    public void TimeUp()
    {
        StaticSettings.maxTime += 1;
    }
    public void TimeUpMin()
    {
        StaticSettings.maxTime += 60;
    }
    public void TimeDown()
    {
        StaticSettings.maxTime -= 1;
    }
    public void TimeDownMin()
    {
        StaticSettings.maxTime -= 60;
    }
    //the game will start.
    public void StartGame()
    { 
        isPlayable = true;
        StaticSettings.timeHasPassed = false;
        StaticSettings.score = 0;
    }
    //After the amount of time passes, the game is stopped and the end menu is activated.
    public void StopGame()
    {
        isPlayable = false;
        StaticSettings.timeHasPassed = true;
    }
    //this handles the speed with a dropdown menu. 
    public void SpeedHandler(int speed)
    {
        switch (speed)
        {
            case 0:
                StaticSettings.spawnTime = 10;
                break;
            case 1:
                StaticSettings.spawnTime = 14;
                break;
            case 2:
                StaticSettings.spawnTime = 6;
                break;
        }
    }
    //for people who think the animations are too much, they can use this option.
    public void animationsOnOrOff()
    {
        StaticSettings.animationOff = !StaticSettings.animationOff;
    }
}
