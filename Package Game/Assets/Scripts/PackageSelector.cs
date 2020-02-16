using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using TritraNet;
using UnityEngine;
using UnityInput = UnityEngine.Input;
using UnityEngine.UI;

public class PackageSelector : MonoBehaviour
{
    private Recognizer recognizer;
    [SerializeField] private List<double> apexAngles;

    [SerializeField] private Text PointsText;
    private int Points;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] PackageAudio;

    private bool WrongSoundCanPlay = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
            /* For each match, give the new object:
                - its corresponding position (which depends on the center of the triangle)
                - its corresponding rotation (which depends on the orientation of the triangle) */

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector2((float)center.x, (float)center.y)), Vector2.zero);


            if (hit.collider != null)
            {
                if (hit.transform.name == "Package1(Clone)" && foundMatches[0].MatchedAngle == apexAngles[0])
                {
                    hit.transform.GetComponent<OnHitChangePackage>().AddStamp();
                    hit.transform.GetChild(1).localRotation = Quaternion.Euler((float)-(foundMatches[0].GetOrientation()) + 90, 90, -90);
                    CorrectInputActions();
                    hit.transform.name = "HitPackage1";
                }
                else if (hit.transform.name == "Package1(Clone)" && foundMatches[0].MatchedAngle != apexAngles[0])
                {
                    print("Wrong input   1");
                    IncorrectInputActions();
                    WrongSoundCanPlay = true;
                    hit.transform.GetComponent<OnHitChangePackage>().PlayNegativeParticle();
                    hit.transform.name = "Cooldown1";
                }

                if (hit.transform.name == "Package2(Clone)" && foundMatches[0].MatchedAngle == apexAngles[1])
                {
                    hit.transform.GetComponent<OnHitChangePackage>().AddStamp();
                    hit.transform.GetChild(1).localRotation = Quaternion.Euler((float)-(foundMatches[0].GetOrientation()) + 90, 90, -90);
                    CorrectInputActions();
                    hit.transform.name = "HitPackage2";
                }
                else if (hit.transform.name == "Package2(Clone)" && foundMatches[0].MatchedAngle != apexAngles[1])
                {
                    print("Wrong input   2");
                    IncorrectInputActions();
                    WrongSoundCanPlay = true;
                    hit.transform.GetComponent<OnHitChangePackage>().PlayNegativeParticle();
                    hit.transform.name = "Cooldown2";
                }

                if (hit.transform.name == "Package3(Clone)" && foundMatches[0].MatchedAngle == apexAngles[2])
                {
                    hit.transform.GetComponent<OnHitChangePackage>().AddStamp();
                    hit.transform.GetChild(1).localRotation = Quaternion.Euler((float)-(foundMatches[0].GetOrientation()) + 90, 90, -90);
                    CorrectInputActions();
                    hit.transform.name = "HitPackage3";
                }
                else if (hit.transform.name == "Package3(Clone)" && foundMatches[0].MatchedAngle != apexAngles[2])
                {
                    print("Wrong input   3");
                    IncorrectInputActions();
                    WrongSoundCanPlay = true;
                    hit.transform.GetComponent<OnHitChangePackage>().PlayNegativeParticle();
                    hit.transform.name = "Cooldown3";
                }
            }
        }
    }

    private void CorrectInputActions()
    {
        Points++;
        audioSource.clip = PackageAudio[0];
        audioSource.Play();
        GameObject.FindObjectOfType<PackageSpawner>().IncreaseSpawnSpeed();
    }

    private void IncorrectInputActions()
    {
        if (WrongSoundCanPlay)
        {
            audioSource.clip = PackageAudio[1];
            audioSource.Play();
            WrongSoundCanPlay = false;
        }
    }
}