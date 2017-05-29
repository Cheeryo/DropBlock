using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour {

    public RectTransform countdownPanel;
    public Text countdownText;
    public bool firstStart;

    public void Start()
    {
        firstStart = true;
    }

    public IEnumerator GameCountdown()
    {
        countdownPanel.gameObject.SetActive(true);
        Time.timeScale = 0.1f;
        countdownText.text = "3";
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.25f;
        countdownText.text = "2";
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0.5f;
        countdownText.text = "1";
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        if (firstStart)
        {
            countdownText.text = "START";
            yield return new WaitForSeconds(0.5f);
            firstStart = false;
        }        
        countdownPanel.gameObject.SetActive(false);
    }
}
