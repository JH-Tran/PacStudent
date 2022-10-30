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
    private float gameTimeTaken = -0.02f;
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
        string timeString = timeSpan.ToString("mm\\:ss\\:ff");
        timeTakenText.text = timeString;
    }
    public void toggleTime()
    {
        if (isTimeTicking == true)
            isTimeTicking = false;
        else
            isTimeTicking = true;
    }

    public int getScore()
    {
        return gameScore;
    }
    public float getTime()
    {
        return gameTimeTaken;
    }
}
