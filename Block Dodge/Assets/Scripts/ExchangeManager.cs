﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeManager : MonoBehaviour
{
    public static ExchangeManager instance;

    [SerializeField] private int selectedPlayerCount = 1;

    public int SelectedPlayerCount
    {
        get
        {
            return selectedPlayerCount;
        }
    }

    private void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
	}
	
    public void SetPlayerCount(int count)
    {
        selectedPlayerCount = count;
    }
}
