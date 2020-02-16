using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Text maxTimeText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject victoryScreen;

    private float timerTime;

    private int min;
    private int sec;

    private int minMax;
    private int secMax;

    private bool resetTimeOnce = true;


    // Update is called once per frame
    void Update()
    {
        if (!StaticSettings.timeHasPassed)
        {
            if (resetTimeOnce)
            {
                timerTime = 0;
                resetTimeOnce = false;
                victoryScreen.gameObject.SetActive(false);
            }

            min = Mathf.FloorToInt(timerTime / 60); //Every 60 seconds are divided into 1 minute
            sec = Mathf.FloorToInt(timerTime % 60); //Every 60 seconds, the 60 seconds are divided by 60. Turning them into 0

            minMax = Mathf.FloorToInt(StaticSettings.maxTime / 60);
            secMax = Mathf.FloorToInt(StaticSettings.maxTime % 60);

            timerTime += Time.deltaTime;

            timerText.text = min.ToString("00") + ":" + sec.ToString("00");
            //victoryText.text = min.ToString("00") + ":" + sec.ToString("00");
            maxTimeText.text = minMax.ToString("00") + ":" + secMax.ToString("00");


            //If the timer has surpased the maxTime, this bool will be set to true, which triggers the endgame funcion in the FoodSelector Script.
            if (timerTime >= StaticSettings.maxTime)
            {
                StaticSettings.timeHasPassed = true;
                victoryScreen.gameObject.SetActive(true);
            }
        }
        else
        {
            timerTime = 0;
            resetTimeOnce = true;
        }

        scoreText.text = StaticSettings.score.ToString();
    }
    //After the game is done and you click on the reload scene button in the end screen, the scene will reload.
    public void ReloadScene()
    {
        StaticSettings.timeHasPassed = false;
        victoryScreen.gameObject.SetActive(false);
        timerTime = 0;
        StaticSettings.score = 0;
    }
}