using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeManager : MonoBehaviour
{
    public static ExchangeManager instance;

    [SerializeField] private int selectedPlayerCount = 1;
    [SerializeField] private int selectedLevelNumber = 1;

    public int SelectedPlayerCount
    {
        get
        {
            return selectedPlayerCount;
        }
    }

    public int SelectedLevelNumber
    {
        get
        {
            return selectedLevelNumber;
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

    public void SetLevelNumber (int number)
    {
        selectedLevelNumber = number;
    }
}
