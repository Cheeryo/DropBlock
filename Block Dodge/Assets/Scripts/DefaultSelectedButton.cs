using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultSelectedButton : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject defaultButton;

    public void OnOpen()
    {
        if (defaultButton != null)
        {
            eventSystem.SetSelectedGameObject(defaultButton);
        }
    }

}
