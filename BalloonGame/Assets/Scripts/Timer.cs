using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text pointText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text endBlurb;
    [SerializeField] private Text errorText;
    [SerializeField] private GameObject endGameScreen;

    static public float timeLeft = 30;
    static public float badHits = 0;
    static public float MaximumTime;

    // Start is called before the first frame update
    void Start()
    {
        MaximumTime = timeLeft;
        StaticVariable.gameIsOver = false;
        StaticVariable.points = 0;
        badHits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MaximumTime -= Time.deltaTime;
        if (MaximumTime < 0)
        {
            CollisionDetector.gameEnded = true;
            print("Game has ended");
            EndGame();
            timerText.text = "0";
        }
        else
        {
            CollisionDetector.gameEnded = false;
            timerText.text = Mathf.RoundToInt(MaximumTime).ToString();
        }

        pointText.text = "Punten: " + StaticVariable.points.ToString();
    }
    //the game ends after a certain time, giving the end screen.
    public void EndGame()
    {
        StaticVariable.gameIsOver = true;
        endGameScreen.gameObject.SetActive(true);
        endBlurb.text = StaticVariable.points.ToString();
        errorText.text = badHits.ToString();
    }
}
