using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{
    [SerializeField] Text countdownText;

    private void Awake()
    {
        Time.timeScale = 0f;
        StartCoroutine("countdownEnumertator");
    }

    IEnumerator countdownEnumertator()
    {
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "GO";
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
