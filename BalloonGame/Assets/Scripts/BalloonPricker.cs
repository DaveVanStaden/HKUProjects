using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using TritraNet;
using UnityEngine;
using UnityInput = UnityEngine.Input;

public class BalloonPricker : MonoBehaviour
{
    private Recognizer recognizer;
    [SerializeField] private List<double> apexAngles;

    private AudioSource audioSource;

    private bool canHitSecondBalloon = false;
    /// <summary>
    /// The settings for our controller detection wil be set here.
    /// first off it checks if your hits are correct or incorrect. if it needs a hit from both controllers, then it asks for that. otherwise it will either make the object untargetable if its a bad hit, or fall down if its a good hit.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        var packageOptions = new RecognizerOptions()
        {
            maxAngleTolerance = 15,
            maxPointDistance = 400,
        };
        this.recognizer = new Recognizer(apexAngles, packageOptions);
    }

    void Update()
    {
        //if (!StaticVariable.gameIsOver)
        //{
        CalculateAngles(UnityInput.touches);

        if (Input.GetMouseButtonDown(0))
        {
            InputWithMouse();
        }
        //}
    }

    private void CalculateAngles(Touch[] touches)
    {
        var vecs = touches.Select(p => new Vector2d(p.position.x, p.position.y)).ToList();
        var foundMatches = this.recognizer.FindMatches(vecs);

        if (foundMatches.Count > 0)
        {
            var center = foundMatches[0].GetCenter();

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector2((float)center.x, (float)center.y)), Vector2.zero);
            Debug.DrawLine(transform.position, hit.point, Color.red);

            if (hit.collider != null)
            {
                if (hit.transform.tag == "Land" && foundMatches[0].MatchedAngle == apexAngles[0])
                {
                    CorrectHit(hit);
                }
                else if (hit.transform.name == "Land" && foundMatches[0].MatchedAngle != apexAngles[0])
                {
                    IncorrectHit(hit);
                }

                if (hit.transform.tag == "Sea" && foundMatches[0].MatchedAngle == apexAngles[1])
                {
                    CorrectHit(hit);
                }
                else if (hit.transform.name == "Sea" && foundMatches[0].MatchedAngle != apexAngles[1])
                {
                    IncorrectHit(hit);
                }
            }
        }
    }

    private void InputWithMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider == null) return;
        Debug.DrawLine(transform.position, hit.point, Color.red);

        if (hit.transform.tag == "Land")
        {
            CorrectHit(hit);
        }

        if (hit.transform.tag == "Sea")
        {
            CorrectHit(hit);
        }

        if (hit.transform.tag == "Danger")
        {
            BadHit(hit);
            Timer.badHits += 1;
        }

        if (hit.transform.tag == "Both")
        {
            //hit.transform.GetChild(1).gameObject.SetActive(false);
            hit.transform.tag = "HitAnimal";
            StartCoroutine("EnableBoth");
            canHitSecondBalloon = false;
        }

        if (hit.transform.tag == "HitAnimal" && canHitSecondBalloon)
        {
            BothHit(hit);
        }
        //print(hit.transform.tag);
    }

    private void CorrectHit(RaycastHit2D hit)
    {
        hit.collider.enabled = false;
        hit.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        hit.transform.GetChild(1).gameObject.SetActive(false);
        hit.transform.GetChild(0).GetComponent<Animator>().speed = 0;
    }

    private void IncorrectHit(RaycastHit2D hit)
    {
        hit.collider.enabled = false;

        hit.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        hit.transform.tag = "Cooldown";
    }

    private void BadHit(RaycastHit2D hit)
    {
        hit.collider.enabled = false;
        hit.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        hit.transform.GetChild(1).gameObject.SetActive(false);
        hit.transform.GetChild(0).GetComponent<Animator>().speed = 0;
        audioSource.Play();
    }

    private void BothHit(RaycastHit2D hit)
    {
        hit.collider.enabled = false;
        hit.transform.GetChild(1).gameObject.SetActive(false);
        hit.transform.GetComponent<FlyObjectManager>().StopAllCoroutines();
        hit.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        hit.transform.GetChild(0).GetComponent<Animator>().speed = 0;
        canHitSecondBalloon = false;
    }

    IEnumerator EnableBoth()
    {
        yield return new WaitForSeconds(0.1f);
        canHitSecondBalloon = true;
    }
}
