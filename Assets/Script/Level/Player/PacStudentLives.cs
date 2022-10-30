using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacStudentLives : MonoBehaviour
{
    [SerializeField] Image lifeImage1, lifeImage2, lifeImage3;
    [SerializeField] GameObject pelletsParent;
    [SerializeField] GameOverManager gameOverManager;
    private InputManager inputManager;
    
    private GameObject player;
    private int lives = 3;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        player = gameObject;
    }

    private void FixedUpdate()
    {
        if (pelletsParent.transform.childCount == 0)
        {
            playerGameOver();
        }
    }

    public void playerLoseLife()
    {
        lives -= 1;

        switch (lives)
        {
            case 0:
                lifeImage1.enabled = false;
                lifeImage3.enabled = false;
                lifeImage2.enabled = false;
                playerGameOver();
                break;
            case 1:
                lifeImage3.enabled = false;
                lifeImage2.enabled = false;
                playerReset();
                break;
            case 2:
                lifeImage3.enabled = false;
                playerReset();
                break;
        }
    }

    private void playerReset()
    {
        inputManager.resetPlayer();
    }

    private void playerGameOver()
    {
        gameOverManager.gameOverTigger();
    }
}
