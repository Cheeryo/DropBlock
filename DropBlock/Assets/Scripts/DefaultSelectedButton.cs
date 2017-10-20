using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultSelectedButton : MonoBehaviour {
    
    public GameObject defaultButton;

    public void Start()
    {
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton);
        }
    }

}
