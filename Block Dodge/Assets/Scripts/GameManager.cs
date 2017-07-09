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

    [SerializeField] public PlayerReferences[] players;
    [Header("Level")]
    //[SerializeField] private BlockManager blockM;
    [SerializeField] private Transform levelContainer;
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
    public float firstScore;
    public float secondScore;


	private void Start ()
    {
        countdown = GetComponent<StartCountdown>();
        SetPlayers();
        SetLevel();
        StartCoroutine(countdown.GameCountdown());
        
    }

    private void Update()
    {
        GameEnding();
        Debug.Log(levelWidth);
        Debug.Log(levelHeight);
    }
    // Programmiert von Maximilian Schöberl - Anfang
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
        }

        CameraManager c = GetComponent<CameraManager>();
        foreach (PlayerReferences p in players)
        {
            if (p.active)
                c.players.Add(p.controller);
        }
        // Programmiert von Maximilian Schöberl - Ende
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
        levelWidth = 12;
        levelHeight = 35;
        // Programmiert von Maximilian Schöberl - Anfang
        try
        {
            levelNumber = ExchangeManager.instance.SelectedLevelNumber;
        }
        catch (System.Exception e) { }

        if (levelNumber == 0) // 20x30
        {
            levelWidth = 12;
            levelHeight = 35;
        }
        // Programmiert von Maximilian Schöberl - Ende
        // GameObject.Instantiate(levels[levelNumber], new Vector3(0, 2, 0), Quaternion.Euler(0, 180, 0), levelContainer);
    }

    private void GameEnding()
    {
        if (playerReachedGoal == playerCount)
        {
            gameHasEnded = true;
        }
    }
}
