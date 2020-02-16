using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityInput = UnityEngine.Input;
using TMPro;
using TritraNet;
using UnityEngine.UI;
using System.Linq;

public class BaloonSelector : MonoBehaviour
{
    private Recognizer recognizer;
    [SerializeField] private List<double> apexAngles;

    [SerializeField] private Text PointsText;
    private int Points;


    private void Awake()
    {
        var packageOptions = new RecognizerOptions()
        {
            maxAngleTolerance = 10,
            maxPointDistance = 400,
        };
        this.recognizer = new Recognizer(apexAngles, packageOptions);
    }

    void Update()
    {
        CalculateAngles(UnityInput.touches);
        PointsText.text = "Points: " + Points;
    }

    private void CalculateAngles(Touch[] touches)
    {
        var vecs = touches.Select(p => new Vector2d(p.position.x, p.position.y)).ToList();
        var foundMatches = this.recognizer.FindMatches(vecs);

        if (foundMatches.Count > 0)
        {
            var center = foundMatches[0].GetCenter();
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector2((float)center.x, (float)center.y)), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.transform.name == "Package1(Clone)" && foundMatches[0].MatchedAngle == apexAngles[0])
                {
                    
                }
            }
        }
    }
}
