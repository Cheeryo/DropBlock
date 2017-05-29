using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject[] Player;
    private StartCountdown countdown;

    private void Start()
    {
        countdown = GetComponent<StartCountdown>();
    }

    void Update () {
		if (Input.GetButtonDown("Menu"))
        {
            Pause();
        }
	}

    public void Pause()
    {
        if (!PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            for (int i = 0; i < 4; i++)
            {
                Player[i].GetComponentInChildren<PlayerController>().enabled = false;
            }
        }
        else if (PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            for (int i = 0; i < 4; i++)
            {
                Player[i].GetComponentInChildren<PlayerController>().enabled = true;
            }
            StartCoroutine(countdown.GameCountdown());
        }  
    }
}
