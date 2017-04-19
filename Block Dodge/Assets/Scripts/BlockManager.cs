using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Transform blockContainer;
    [SerializeField] private GameObject[] blockPrefab;

    [SerializeField][Range(1,20)] private float interval;
    [SerializeField] private GameManager manager;
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

            int blockNumber = (int)Random.Range(0.0f, 100.0f);
            float xPos;

            if (blockNumber >= 1 && blockNumber <= 5) // spawn 3x1 Block
            {
                xPos = CalculatePosition((int)-manager.levelWidth+1, (int)manager.levelWidth-2, 3); 
                if (xPos == (manager.levelWidth *-2)) return; //if there's no free position to spawn, no block will be spawned
                GameObject.Instantiate(blockPrefab[0], new Vector3(xPos + 1.5f, manager.levelHeight, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber >= 6 && blockNumber <= 27) // spawn 2x1 Block
            {
                xPos = CalculatePosition((int)-manager.levelWidth, (int)manager.levelWidth -1, 2);
                if (xPos == (manager.levelWidth * -2)) return;
                GameObject.Instantiate(blockPrefab[1], new Vector3(xPos + 1, manager.levelHeight, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber >= 28 && blockNumber <= 45) // spawn 2x2 Block
            {
                xPos = CalculatePosition((int)-manager.levelWidth, (int)manager.levelWidth -1, 2);
                if (xPos == (manager.levelWidth * -2)) return;
                GameObject.Instantiate(blockPrefab[2], new Vector3(xPos + 1, manager.levelHeight -1, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber >= 46 && blockNumber <= 73) // spawn 1x1 Block
            {
                xPos = CalculatePosition((int)-manager.levelWidth, (int)manager.levelWidth);
                if (xPos == (manager.levelWidth * -2)) return; 
                GameObject.Instantiate(blockPrefab[3], new Vector3(xPos + 0.5f, manager.levelHeight, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber >= 74 && blockNumber <= 95) // spawn 1x2 Block
            {
                xPos = CalculatePosition((int)-manager.levelWidth, (int)manager.levelWidth);
                if (xPos == (manager.levelWidth * -2)) return;
                GameObject.Instantiate(blockPrefab[4], new Vector3(xPos + 0.5f, manager.levelHeight -1, 0), Quaternion.Euler(0, 90, 0), blockContainer);
            }
            else if (blockNumber >= 96 && blockNumber <= 100) // spawn 1x3 Block
            {
                xPos = CalculatePosition((int)-manager.levelWidth, (int)manager.levelWidth);
                if (xPos == (manager.levelWidth * -2)) return;
                GameObject.Instantiate(blockPrefab[5], new Vector3(xPos + 0.5f, manager.levelHeight -2, 0), Quaternion.Euler(0, 90, 0), blockContainer);
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
        float result = manager.levelWidth *-2;
        List<bool> columns = new List<bool>();
        List<int> possiblePositions = new List<int>();
        //initially fills the List
        for (int i = min; i < max+1; i++)
            columns.Add(true);
        
        for (int i = 0; i < columns.Count; i++)
        {
            Vector3 pos = new Vector3(i+min + .5f, manager.levelHeight, 0);
            RaycastHit hit;
            if (Physics.Raycast(pos, Vector3.down, out hit, 29))
            {
                //does Raycast hit an Cube that is currently falling?
                if (hit.collider.CompareTag("Block") && !hit.collider.GetComponent<BlockController>().Locked || hit.point.y > 26)
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
