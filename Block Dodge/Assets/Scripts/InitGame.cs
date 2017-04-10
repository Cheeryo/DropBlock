using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitGame : MonoBehaviour {

    public Canvas startCanvas;
    public Canvas menuCanvas;

	// Use this for initialization
	void Awake()
    {
        startCanvas.enabled = true;
        menuCanvas.enabled = false;
    }	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Menu") && startCanvas.enabled == true)
        {
            startCanvas.enabled = false;
            menuCanvas.enabled = true;
        } 
	}
}
