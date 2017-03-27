using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Transform blockContainer;
    [SerializeField] private GameObject blockPrefab;

    [SerializeField] private float interval;
    private float currentInterval;

	private void Start ()
    {
		
	}
	
	private void Update ()
    {
        currentInterval += Time.deltaTime;
		if(currentInterval >= interval)
        {
            currentInterval = 0;
            GameObject.Instantiate(blockPrefab, new Vector3(Random.Range(-10, 10) + .5f, 28, 0), Quaternion.Euler(0,90,0), blockContainer);
        }
	}
}
