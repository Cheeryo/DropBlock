using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour {

    private GameManager manager;
    public RectTransform playerPanels;
    public RectTransform gameEndPanel;
    public RectTransform[] placementPanels;
    public Text[] placementTexts;
    public Color playerOneColor;
    public Color playerTwoColor;

	// Use this for initialization
	void Start () {
        manager = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (manager.gameHasEnded)
        {
            Debug.Log(manager.firstScore);
            Placement();
            playerPanels.gameObject.SetActive(false);
            gameEndPanel.gameObject.SetActive(true);
        }
	}

    private void Placement()
    {
        if (manager.players[0].controller.score > manager.players[1].controller.score)
        {
            manager.firstScore = manager.players[0].controller.score;
            manager.secondScore = manager.players[1].controller.score;
            placementTexts[0].color = playerOneColor;
            placementTexts[1].color = playerTwoColor;
        }
        else
        {
            manager.firstScore = manager.players[1].controller.score;
            manager.secondScore = manager.players[0].controller.score;
            placementTexts[1].color = playerOneColor;
            placementTexts[0].color = playerTwoColor;
        }
        placementTexts[0].text = manager.firstScore.ToString();
        placementTexts[1].text = manager.secondScore.ToString();
    }
}
