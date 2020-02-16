using System.Collections.Generic;
using System.Linq;
using TMPro;
using TritraNet;
using UnityEngine;
using UnityInput = UnityEngine.Input;
using UnityEngine.UI;

public class LetterSelector : MonoBehaviour
{
    private Recognizer recognizer;
    [SerializeField] private List<double> apexAngles;
    public LetterTextSelector textSelector;
    public string SelectedLetter;
    private WordAlgorithm wordAlgorithm;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] PackageAudio;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        var packageOptions = new RecognizerOptions()
        {
            maxAngleTolerance = 15,
            maxPointDistance = 400,
        };
        this.recognizer = new Recognizer(apexAngles, packageOptions);

        wordAlgorithm = GetComponent<WordAlgorithm>();
    }

    void Update()
    {
        if (wordAlgorithm.GameOver == false)
        {
            CalculateAngles(UnityInput.touches);
        }

        if (Input.GetMouseButtonDown(0) && wordAlgorithm.GameOver == false)
        {
            InputWithMouse();
        }
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
                if (foundMatches[0].MatchedAngle == apexAngles[0] && GetComponent<WordAlgorithm>().word.ToUpper().Contains(hit.transform.GetChild(0).name) && hit.transform.tag == "Letter")
                {
                    CorrectHit(hit);

                }
                else if (foundMatches[0].MatchedAngle == apexAngles[0] && hit.transform.tag == "Letter")
                {
                    IncorrectHit(hit);
                }
            }
        }
    }

    private void InputWithMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.DrawLine(transform.position, hit.point, Color.red);

        if (hit.collider != null)
        {

            print(hit.transform.name);


            if (GetComponent<WordAlgorithm>().word.ToUpper().Contains(hit.transform.GetChild(0).name) && hit.transform.tag == "Letter")
            {
                CorrectHit(hit);

            }
            else if (hit.transform.tag == "Letter")
            {

                IncorrectHit(hit);
            }
        }
    }

    private void CorrectHit(RaycastHit2D hit)
    {
        hit.transform.tag = "HitLetter";
        hit.collider.enabled = false;
        for (int i = 0; i < GetComponent<WordAlgorithm>().word.Length; i++)
        {
            textSelector = hit.transform.gameObject.GetComponent<LetterTextSelector>();
            SelectedLetter = textSelector.ObjectLetter;

            wordAlgorithm.RevealLetter();
            hit.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
        }

        audioSource.clip = PackageAudio[0];
        audioSource.Play();
    }

    private void IncorrectHit(RaycastHit2D hit)
    {
        hit.collider.enabled = false;

        hit.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        textSelector = hit.transform.gameObject.GetComponent<LetterTextSelector>();
        wordAlgorithm.RevealLetter();
        hit.transform.tag = "Cooldown";

        audioSource.clip = PackageAudio[1];
        audioSource.Play();
    }
}