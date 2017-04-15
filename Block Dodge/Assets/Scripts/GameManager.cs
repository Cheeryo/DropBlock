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

	private void Start ()
    {
        int playerCount = 4;

        try
        {
            playerCount = ExchangeManager.instance.SelectedPlayerCount;
        }
        catch (System.Exception e) { }

        for(int i = players.Length - 1; i >= playerCount; i--)
        {
            players[i].controller.transform.parent.gameObject.SetActive(false);
            players[i].uiContainer.gameObject.SetActive(false);
            players[i].active = false;
        }

        CameraManager c = GetComponent<CameraManager>();
        foreach (PlayerReferences p in players)
        {
            if(p.active)
                c.players.Add(p.controller);
        }
	}
}
