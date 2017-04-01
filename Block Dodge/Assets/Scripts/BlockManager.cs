using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Transform blockContainer;
    [SerializeField] private GameObject[] blockPrefab;

    [SerializeField][Range(1,20)] private float interval;
    private float currentInterval;

	private void Start ()
    {

	}

    private int BlockSelector()
    {
        return (int)Random.Range(1.0f, 21.0f);
    }
	
	private void Update ()
    {
        currentInterval += Time.deltaTime;
		if(currentInterval >= interval)
        {
            currentInterval = 0;

            int blockNumber = BlockSelector();
            float xPos;
            if (blockNumber <= 8)
            {
                xPos = CalculatePosition(-10, 10);
                if (xPos == -20) return; //if there's no free position to spawn, no block will be spawned
                GameObject.Instantiate(blockPrefab[0], new Vector3(xPos + 0.5f, 27, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber == 9 || blockNumber == 10)
            {
                xPos = CalculatePosition(-10, 9, 2);
                if (xPos == -20) return;
                GameObject.Instantiate(blockPrefab[1], new Vector3(xPos + 1, 26, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber > 10 && blockNumber < 15)
            {
                xPos = CalculatePosition(-10, 10);
                if (xPos == -20) return;
                GameObject.Instantiate(blockPrefab[2], new Vector3(xPos + 0.5f, 26, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber == 15)
            {
                xPos = CalculatePosition(-10, 10);
                if (xPos == -20) return;
                GameObject.Instantiate(blockPrefab[3], new Vector3(xPos + 0.5f, 25, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber == 16 || blockNumber == 17)
            {
                xPos = CalculatePosition(-10, 9, 2);
                if (xPos == -20) return;
                GameObject.Instantiate(blockPrefab[4], new Vector3(xPos + 1, 27, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else
            {
                xPos = CalculatePosition(-9, 8, 3);
                if (xPos == -20) return;
                GameObject.Instantiate(blockPrefab[5], new Vector3(xPos + 0.5f, 27, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
        }
	}


    /// <summary>
    /// Returns a new X Position where the column is not blocked by another block.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="blockWidth"></param>
    /// <returns></returns>
    private float CalculatePosition(int min, int max, int blockWidth = 1)
    {
        float result = -20;
        List<bool> columns = new List<bool>();
        List<int> possiblePositions = new List<int>();
        //initially fills the List
        for (int i = min; i < max+1; i++)
            columns.Add(true);
        
        for (int i = 0; i < columns.Count; i++)
        {
            Vector3 pos = new Vector3(i+min + .5f, 29, 0);
            RaycastHit hit;
            if (Physics.Raycast(pos, Vector3.down, out hit, 29))
            {
                //does Raycast hit an Cube that is currently falling?
                if (hit.collider.CompareTag("Cube") && !hit.collider.GetComponent<BlockController>().Locked || hit.point.y > 26)
                {
                    columns[i] = false;
                }
            }
        }

        //Checks whether the entire block (width) will fit / is not blocked
        for (int i = 0; i < columns.Count; i++)
        {
            if(i < columns.Count - blockWidth)
            {
                bool validPos = true;

                for(int u = 0; u < blockWidth; u++)
                {
                    if (columns[i + u] == false)
                    {
                        validPos = false;
                    }
                }

                if (validPos)
                    possiblePositions.Add(min+i);
            }
        }

        if (possiblePositions.Count > 0)
        {
            result = possiblePositions[Random.Range(0, possiblePositions.Count)];
        }
        /*
        else
            Debug.LogWarning("No valid position available! >> Errors may occur");
        */
        return result;
    }
}
