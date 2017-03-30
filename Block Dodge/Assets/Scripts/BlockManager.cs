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
        blockNumber = (int) Random.Range(1.0f, 21.0f);
        return blockNumber;
    }
	
	private void Update ()
    {
        currentInterval += Time.deltaTime;
		if(currentInterval >= interval)
        {
            blockSelector();
            currentInterval = 0;
            if (blockNumber <= 8)
            {
                GameObject.Instantiate(blockPrefab[0], new Vector3(Random.Range(-10, 11) + 0.5f, 27, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber == 9 || blockNumber == 10)
            {
                GameObject.Instantiate(blockPrefab[1], new Vector3(Random.Range(-9, 10), 26, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber > 10 && blockNumber < 15)
            {
                GameObject.Instantiate(blockPrefab[2], new Vector3(Random.Range(-10, 11) + 0.5f, 26, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber == 15)
            {
                GameObject.Instantiate(blockPrefab[3], new Vector3(Random.Range(-10, 11) + 0.5f, 25, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber == 16 || blockNumber == 17)
            {
                GameObject.Instantiate(blockPrefab[4], new Vector3(Random.Range(-9, 10), 27, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber == 18)
            {
                GameObject.Instantiate(blockPrefab[5], new Vector3(Random.Range(-8, 9) + 0.5f, 27, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
        }
	}
}
