using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerReferences
    {
        public PlayerController controller;
        public RectTransform uiContainer;
        public bool active;
    }

    [SerializeField] private PlayerReferences[] players;
    [Header("Level")]
    //[SerializeField] private BlockManager blockM;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private GameObject[] levels;
    [HideInInspector] public float levelWidth;
    [HideInInspector] public float levelHeight;

	private void Start ()
    {
        SetPlayers();
        SetLevel();
        
    }

    private void SetPlayers()
    {
        int playerCount = 4;

        try
        {
            playerCount = ExchangeManager.instance.SelectedPlayerCount;
        }
        catch (System.Exception e) { }

        for (int i = players.Length - 1; i >= playerCount; i--)
        {
            players[i].controller.transform.parent.gameObject.SetActive(false);
            players[i].uiContainer.gameObject.SetActive(false);
            players[i].active = false;
        }

        CameraManager c = GetComponent<CameraManager>();
        foreach (PlayerReferences p in players)
        {
            if (p.active)
                c.players.Add(p.controller);
        }
    }

    private void SetLevel()
    {
        int levelNumber = 0;

        try
        {
            levelNumber = ExchangeManager.instance.SelectedLevelNumber;
        }
        catch (System.Exception e) { }

        if (levelNumber == 0) // 20x30
        {
            levelWidth = 10;
            levelHeight = 35;
        }
        else if (levelNumber == 1) // 20x15
        {
            levelWidth = 10;
            levelHeight = 20;
        }
        else if (levelNumber == 2) // 40x30
        {
            levelWidth = 20;
            levelHeight = 35;
        }
        else if (levelNumber == 3) // 40x15
        {
            levelWidth = 20;
            levelHeight = 20;
        }

        GameObject.Instantiate(levels[levelNumber], new Vector3(0, 2, 0), Quaternion.Euler(0, 180, 0), levelContainer);
    }
}
