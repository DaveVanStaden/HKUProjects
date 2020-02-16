using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Text victoryText;
    [SerializeField] private Text maxTimeText;

    private float time;

    private int min;
    private int sec;

    private int minMax;
    private int secMax;

    public bool timeHasPassed = false;

    // Update is called once per frame
    void Update()
    {
        if (FoodSelector.gameIsRunning)
        {
            min = Mathf.FloorToInt(time / 60); //Every 60 seconds are divided into 1 minute
            sec = Mathf.FloorToInt(time % 60); //Every 60 seconds, the 60 seconds are divided by 60. Turning them into 0

            minMax = Mathf.FloorToInt(StaticSettings.maxTime / 60);
            secMax = Mathf.FloorToInt(StaticSettings.maxTime % 60);

            time += Time.deltaTime;

            timerText.text = min.ToString("00") + ":" + sec.ToString("00");
            //victoryText.text = min.ToString("00") + ":" + sec.ToString("00");
            maxTimeText.text = minMax.ToString("00") + ":" + secMax.ToString("00");


            //If the timer has surpased the maxTime, this bool will be set to true, which triggers the endgame funcion in the FoodSelector Script.
            if (time >= StaticSettings.maxTime)
            {
                timeHasPassed = true;
            }
        }
    }
}