using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MaxScoreHolder : MonoBehaviour
{
    [SerializeField] private Text ScoreHolder;

    // Update is called once per frame
    void Update()
    {
        ScoreHolder.text = "Score to reach: =" + " " + StaticVariable.MaxScore.ToString();
    }
}
