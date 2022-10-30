using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Level1Load()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1);
    }
    public void Level2Load()
    {
        Debug.Log("NOT FOUND");
    }
    public void ExitLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
}
