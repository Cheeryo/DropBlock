using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject[] Player;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Menu"))
        {
            Pause();
        }
	}

    public void Pause()
    {
        Debug.Log("1");
        if (!PauseMenu.active)
        {
            Debug.Log("2");
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            for (int i = 0; i < 4; i++)
            {
                Player[i].GetComponentInChildren<PlayerController>().enabled = false;
            }
        }
        else if (PauseMenu.active)
        {
            Debug.Log("3");
            PauseMenu.SetActive(false);
            Time.timeScale = 1.1f;
            for (int i = 0; i < 4; i++)
            {
                Player[i].GetComponentInChildren<PlayerController>().enabled = true;
            }
        }  
    }
}
