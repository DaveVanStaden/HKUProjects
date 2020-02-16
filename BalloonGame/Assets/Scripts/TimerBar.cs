using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private GameObject timerBar;
    private float barWidth;
    private float barHeigth;
    private float minusValue;
    private RectTransform rt;
    private float MaximumTime = 1;
    private Vector2 vector;
    private float startWidth;
    /// <summary>
    /// In this scrpt, the bar width height get calculated through the amount of time thats left, then with the width the minus value is calculated that gets deducted from it every second.
    /// Then the bar width lerps the minus value from it to get the effect of a draining bar.
    /// </summary>
    void Start()
    {
        rt = (RectTransform)timerBar.transform;
        startWidth = rt.rect.width;

        barWidth = rt.rect.width;
        barHeigth = rt.rect.height;
        minusValue = rt.rect.width / Timer.timeLeft / 50;
    }
    private void FixedUpdate()
    { 
        barWidth = Mathf.Lerp(startWidth, 0, Mathf.InverseLerp(Timer.timeLeft, 0, Timer.MaximumTime));
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(barWidth, barHeigth);
    }
}
