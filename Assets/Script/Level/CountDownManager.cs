using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{
    [SerializeField] Text countdownText;

    private float timer = 4;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        timer -= Time.fixedUnscaledDeltaTime;
        if (timer < 4 && timer > 3)
            countdownText.text = "3";
        else if (timer < 3 && timer > 2)
            countdownText.text = "2";
        else if (timer < 2 && timer > 1)
            countdownText.text = "1";
        else if (timer < 1 && timer > 0)
        {
            countdownText.text = "GO";
        }
        else
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}
