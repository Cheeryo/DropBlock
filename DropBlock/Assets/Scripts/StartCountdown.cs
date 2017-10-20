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
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
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
