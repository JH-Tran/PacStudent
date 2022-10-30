using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacStudentLives : MonoBehaviour
{
    [SerializeField] Image lifeImage1, lifeImage2, lifeImage3;
    [SerializeField] GameObject respawnObject;

    private GameObject player;
    private int lives = 3;

    void Start()
    {
        player = gameObject;
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
        player.transform.position = respawnObject.transform.position;
    }

    private void playerGameOver()
    {

    }
}
