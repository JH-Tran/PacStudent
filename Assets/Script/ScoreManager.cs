using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text scoreNumText;
    [SerializeField] Text timeTakenText;

    private bool isTimeTicking = true;
    private float gameTimeTaken = 0.0f;
    private int gameScore = 0;

    void FixedUpdate()
    {
        gameTimeTaken += Time.deltaTime;
        if (isTimeTicking)
        {
            timeToText();
        }
    }

    public void addScore(int score)
    {
        gameScore += score;
        scoreNumText.text = "" + gameScore;
    }
    public void timeToText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTimeTaken);
        string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        timeTakenText.text = timeString;
    }
    public void toggleTime()
    {
        if (isTimeTicking == true)
            isTimeTicking = false;
        else
            isTimeTicking = true;
    }
}
