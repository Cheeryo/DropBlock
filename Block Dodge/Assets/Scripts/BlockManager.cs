using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Transform blockContainer;
    [SerializeField] private GameObject[] blockPrefab;

    [SerializeField] private float interval;
    private float currentInterval;
    private int blockNumber;

	private void Start ()
    {

	}

    private int blockSelector()
    {
        blockNumber = (int) Random.Range(-0.1f, 5.0f);
        return blockNumber;
    }
	
	private void Update ()
    {
        currentInterval += Time.deltaTime;
		if(currentInterval >= interval)
        {
            blockSelector();
            currentInterval = 0;
            GameObject.Instantiate(blockPrefab[blockNumber], new Vector3(Random.Range(-10, 10) + .5f, 27, 0), Quaternion.Euler(0,90,0), blockContainer);
        }
	}
}
