using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Text victoryText;

    private float time;

    private int min;
    private int sec;

    // Update is called once per frame
    void Update()
    {
        if (!EndGame.gameHasEnded)
        {
            min = Mathf.FloorToInt(time / 60); //Every 60 seconds are divided into 1 minute
            sec = Mathf.FloorToInt(time % 60); //Every 60 seconds, the 60 seconds are divided by 60. Turning them into 0

            time += Time.deltaTime;

            timerText.text = min.ToString("00") + ":" + sec.ToString("00");
            victoryText.text = min.ToString("00") + ":" + sec.ToString("00");
        }


        //Resets the time alongside the maze resetting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            time = 0;
        }
    }
}
