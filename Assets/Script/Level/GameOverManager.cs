using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Text gameOverText;
    [SerializeField] ScoreManager saveInformation;
    [SerializeField] GameObject playerDeath;
    [SerializeField] GameObject currentPlayer;
    [SerializeField] AnimationClip playerDeathAni;

    private int gameBestScore;
    private float gameBestTime;

    private int currentGameScore;
    private float currentTime;

    private float maxSceneChangeTimer = 3;
    private float sceneChangeTimer;
    private bool changeScene;

    void Awake()
    {
        currentPlayer = GameObject.Find("Player");
        changeScene = false;
        gameOverText.enabled = false;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            gameBestTime = PlayerPrefs.GetFloat("BestTimeTaken");
            gameBestScore = PlayerPrefs.GetInt("HighScore");
        }
        //Debug.Log(gameBestTime + " || " + gameBestScore);
    }

    private void Update()
    {
        if (sceneChangeTimer > 0 && changeScene == true)
        {
            StartCoroutine("playerDeathEnumertator");
        }
    }

    public void gameOverTigger()
    {
        Instantiate(playerDeath, currentPlayer.transform.position, Quaternion.identity);
        currentGameScore = saveInformation.getScore();
        currentTime = saveInformation.getTime();
        gameOverText.enabled = true;
        Destroy(currentPlayer);
        Time.timeScale = 0;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (currentGameScore > gameBestScore)
            {
                savingPlayerPrefInformation();
            }
            else if (gameBestScore == currentGameScore)
            {
                if (currentTime < gameBestTime)
                {
                    savingPlayerPrefInformation();
                }
            }
        }
        else
        {
            savingPlayerPrefInformation();
        }
        sceneChangeTimer = maxSceneChangeTimer;
        changeScene = true;
    }

    private void savingPlayerPrefInformation()
    {
        PlayerPrefs.SetInt("HighScore", currentGameScore);
        PlayerPrefs.SetFloat("BestTimeTaken", currentTime);
    }

    IEnumerator playerDeathEnumertator()
    {
        yield return new WaitForSecondsRealtime(sceneChangeTimer);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
