using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject startButton;

	private void Awake()
    {
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update ()
    {

	}
}
