using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public float SetMaxScore = 40;

    [SerializeField] private Text speedText;

    /// <summary>
    /// This script is made for the options menu in the main menu.
    /// it is used to set the max score, volume, game speed and object size.
    /// Everything is fully cusomizable.
    /// </summary>

    private void Update()
    {

        if (StaticVariable.MaxScore <= 1)
        {
            StaticVariable.MaxScore = 1;
        }

        if (StaticVariable.ObjectSize <= 0.05f)
        {
            StaticVariable.ObjectSize = 0.05f;
        }

        if (StaticVariable.ObjectSize >= 0.30f)
        {
            StaticVariable.ObjectSize = 0.30f;
        }
    }
    //Volume is set using a slider this is why we use a float instead of a int(to make values more accurate).
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    
    public void SetScore(string MaximalScore)
    {
        SetMaxScore = float.Parse(MaximalScore);
        print(SetMaxScore);
        StaticVariable.MaxScore = SetMaxScore;
    }
    //The speed is set using a dropdown menu, so by using an int you can determine which one is currently selected.
    public void SetSpeed(int sizeIndex)
    {
        if (sizeIndex == 0)
        {
            StaticVariable.MovementSpeedSpawner = 1;
        }
        else if (sizeIndex == 1)
        {
            StaticVariable.MovementSpeedSpawner = 2;
        }
        else if (sizeIndex == 2)
        {
            StaticVariable.MovementSpeedSpawner = 4;
        }
    }
    public void SetSize(int sizeIndex)
    {
        print(sizeIndex);
        StaticVariable.ObjectSize = sizeIndex;
    }
    public void IncreaseSize()
    {
        StaticVariable.ObjectSize += 0.01f;
        print(StaticVariable.ObjectSize);
    }
    public void DecreaseSize()
    {
        StaticVariable.ObjectSize -= 0.01f;
        print(StaticVariable.ObjectSize);
    }
    public void IncreaseScore()
    {
        StaticVariable.MaxScore += 1f;
        print(StaticVariable.MaxScore);
    }
    public void DecreaseScore()
    {
        StaticVariable.MaxScore -= 1f;
        print(StaticVariable.MaxScore);
    }

    public void IncreaseScoreXTen()
    {
        StaticVariable.MaxScore += 10f;
        print(StaticVariable.MaxScore);
    }
    public void DecreaseScoreXTen()
    {
        StaticVariable.MaxScore -= 10f;
        print(StaticVariable.MaxScore);
    }
}