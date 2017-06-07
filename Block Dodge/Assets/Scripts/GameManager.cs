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
    [SerializeField] private Transform goalContainer;
    [SerializeField] private GameObject[] levels;
    [HideInInspector] public float levelWidth;
    [HideInInspector] public float levelHeight;
    public float goalScore;
    public int playerCount;
    public int levelNumber;
    public int playerReachedGoal;
    public bool gameHasEnded = false;
    [SerializeField] private GameObject[] goals;
    private StartCountdown countdown;
    private GameEnding gameEnding;


	private void Start ()
    {
        countdown = GetComponent<StartCountdown>();
        gameEnding = GetComponent<GameEnding>();
        SetPlayers();
        SetLevel();
        StartCoroutine(countdown.GameCountdown());
        
    }

    private void Update()
    {
        Debug.Log(playerReachedGoal);
    }

    private void SetPlayers()
    {
        playerCount = 2;

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
            gameEnding.placementPanels[i].gameObject.SetActive(false);
        }

        CameraManager c = GetComponent<CameraManager>();
        foreach (PlayerReferences p in players)
        {
            if (p.active)
                c.players.Add(p.controller);
        }

        switch (playerCount)
        {
            case 1:
                goalScore = 200;
                break;                
            case 2:
                goalScore = 400;
                break;
            case 3:
                goalScore = 700;
                break;
            case 4:
                goalScore = 900;
                break;

        }
    }

    private void SetLevel()
    {
        levelNumber = 0;

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
        goals[levelNumber].gameObject.SetActive(true);
    }

    private void GameEnding()
    {
        if (playerReachedGoal == playerCount)
        {
            gameHasEnded = true;
        }
    }
}
