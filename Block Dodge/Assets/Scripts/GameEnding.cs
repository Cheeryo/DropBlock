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
	// Use this for initialization
	void Start () {
        manager = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (manager.gameHasEnded)
        {
            playerPanels.gameObject.SetActive(false);
            gameEndPanel.gameObject.SetActive(true);
        }
	}
}
