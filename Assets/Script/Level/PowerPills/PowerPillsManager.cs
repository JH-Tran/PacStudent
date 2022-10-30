using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPillsManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioClip ghostScaredClip;
    [SerializeField] private AudioClip normalBackgroundClip;
    [SerializeField] private AudioClip ghostDeathClip;
    [SerializeField] private Text ghostScaredText;
    [SerializeField] private Text ghostScaredNumText;

    private bool backgroundMusicPlaying = false;
    private float ghostScaredTimer = 10;
    private float ghostDeathTimer;
    private float timerCount;

    void Start()
    {
        hideText();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerCount > 0) 
        { 
            timerCount -= Time.deltaTime;
            if (timerCount > 0)
            {
                ghostScaredNumText.text = timerCount.ToString("F2");
            }
            
            if (ghostDeathTimer > 0)
            {
                ghostDeathTimer -= Time.deltaTime;
            }
            else if ((ghostDeathTimer <= 0) && backgroundMusicPlaying == false)
            {
                backgroundMusicPlaying = true;
                backgroundMusic.clip = ghostScaredClip;
                backgroundMusic.Play();
            }
        }
        else if (timerCount < 0)
        {
            timerCount = 0;
            ghostScaredNumText.text = "0";
            ghostDeathTimer = 0;
            backgroundMusicPlaying = false;
            hideText();
        }
    }

    public void addGhostScaredTimer()
    {
        timerCount = ghostScaredTimer;
        ghostScaredText.enabled = true;
        ghostScaredNumText.enabled = true;
        backgroundMusic.clip = ghostScaredClip;
        backgroundMusic.Play();
    }

    private void hideText()
    {
        ghostScaredText.enabled = false;
        ghostScaredNumText.enabled = false;
        backgroundMusic.clip = normalBackgroundClip;
        backgroundMusic.Play();
    }

    public float getGhostScaredTimer()
    {
        return timerCount;
    }

    public void playGhostDeathAudio(float timer)
    {
        backgroundMusic.clip = ghostDeathClip;
        backgroundMusic.Play();
        ghostDeathTimer = timer;
        backgroundMusicPlaying = false;
    }
}
