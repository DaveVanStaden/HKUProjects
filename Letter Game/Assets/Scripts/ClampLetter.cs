using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampLetter : MonoBehaviour
{
    public Text letter;

    void Start()
    {
        //this is to clamp the words letter boxes to a position to not move them.
        letter = GameObject.FindWithTag("LetterBox").GetComponent<Text>();
        letter.text = gameObject.GetComponent<CharacterScript>().Character.ToString();

        print(gameObject.GetComponent<CharacterScript>().Character.ToString());
    }

    void Update()
    {
        Vector3 letterPos = Camera.main.WorldToScreenPoint(this.transform.position);
        letter.transform.position = letterPos;

        if (letter.text == "a" || letter.text == "e" || letter.text == "i" || letter.text == "o" || letter.text == "u")
        {
            gameObject.tag = "vowels";
        }
        else
        {
            gameObject.tag = "consonants";
        }
    }
}