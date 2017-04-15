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
    }

    [SerializeField] private PlayerReferences[] players;

	private void Start ()
    {
        int playerCount = ExchangeManager.instance.SelectedPlayerCount;

        for(int i = players.Length - 1; i >= playerCount; i--)
        {
            players[i].controller.transform.parent.gameObject.SetActive(false);
            players[i].uiContainer.gameObject.SetActive(false);
        }
	}
}
