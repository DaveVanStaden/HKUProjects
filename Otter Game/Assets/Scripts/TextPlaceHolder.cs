using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPlaceHolder : MonoBehaviour
{
    [SerializeField] private Text[] PlaceholderText;
    void Update()
    {
        PlaceholderText[0].text = StaticVariable.MaxScore.ToString();
        PlaceholderText[1].text = StaticVariable.ObjectSize.ToString();
    }
}
