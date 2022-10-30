using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadBestInformation : MonoBehaviour
{
    [SerializeField] Text highScoreText;
    [SerializeField] Text bestTimeTakenText;

    void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");

            TimeSpan timeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("BestTimeTaken"));
            string timeString = timeSpan.ToString("mm\\:ss\\:ff");
            bestTimeTakenText.text = "Time Taken: " + timeString;
        }
        else
        {
            highScoreText.text = "High Score: 0";
            bestTimeTakenText.text = "Time Taken: 99:99:99";
        }
    }
}
