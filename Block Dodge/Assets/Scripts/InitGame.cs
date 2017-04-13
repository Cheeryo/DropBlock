using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private GameObject startButton;

	private void Awake()
    {
        startCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update ()
    {
		if (Input.GetButtonDown("Menu") && startCanvas.enabled == true)
        {
            startCanvas.enabled = false;
            menuCanvas.enabled = true;
        } 
	}
}
