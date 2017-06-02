using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Transform blockContainer;

    [SerializeField][Range(1,20)] private float interval;
    [SerializeField] private GameManager manager;
    private float currentInterval;

    [System.Serializable]
    public class BlockReferences
    {
        public GameObject blockPrefab;
        public float blockLength;
        public float blockHeigth;
    }

    [SerializeField] private BlockReferences[] blocks;

    private int rightSpawnBoundary;
    private int leftSpawnBoundary;
    private float spawnPosition;
    private int blockL;
	
	private void Update ()
    {
        currentInterval += Time.deltaTime;
        if (currentInterval >= interval)
        {
            int blockNumber = 0;
            float xPos;
            currentInterval = 0;
            int blockPoolNumber = GetBlockPool();
            if (blockPoolNumber == 1)
            {
                blockNumber = (int)Random.Range(0, 8);
            }
            else if (blockPoolNumber == 2)
            {
                blockNumber = (int)Random.Range(8, 32);
            }
            else if (blockPoolNumber == 3)
            {
                blockNumber = (int)Random.Range(32, 87);
            }
            else if (blockPoolNumber == 4)
            {
                blockNumber = (int)Random.Range(87, 157);
            }
            if (blocks[blockNumber].blockLength == 1)
            {
                rightSpawnBoundary = (int)-manager.levelWidth;
                leftSpawnBoundary = (int)manager.levelWidth -1;
            }
            else if (blocks[blockNumber].blockLength == 2)
            {
                rightSpawnBoundary = (int)-manager.levelWidth +1;
                leftSpawnBoundary = (int)manager.levelWidth - 1;
            }
            else if (blocks[blockNumber].blockLength == 3)
            {
                rightSpawnBoundary = (int)-manager.levelWidth +1;
                leftSpawnBoundary = (int)manager.levelWidth -2;
            }
            else if (blocks[blockNumber].blockLength == 4)
            {
                rightSpawnBoundary = (int)-manager.levelWidth +2;
                leftSpawnBoundary = (int)manager.levelWidth - 2;
            }
            blockL = (int)blocks[blockNumber].blockLength;
            xPos = CalculatePosition(rightSpawnBoundary, leftSpawnBoundary, blockL);
            if (xPos == (manager.levelWidth * -2)) return;
            if (blocks[blockNumber].blockLength == 1 || blocks[blockNumber].blockLength == 3)
            {
                spawnPosition = xPos + 0.5f;
            }
            else if (blocks[blockNumber].blockLength == 2 || blocks[blockNumber].blockLength == 4)
            {
                spawnPosition = xPos;
            }
            GameObject.Instantiate(blocks[blockNumber].blockPrefab, new Vector3(spawnPosition, manager.levelHeight, 0), Quaternion.Euler(0, 0, 0), blockContainer);
     
        }
	}
    private int GetBlockPool()
    {
        int blockPool = 0;
        int blockPoolNumber = (int)Random.Range(1, 19);
        if (blockPoolNumber < 7)
        {
            blockPool = 1;
        }
        else if (blockPoolNumber > 6 && blockPoolNumber < 12)
        {
            blockPool = 2;
        }
        else if (blockPoolNumber > 11 && blockPoolNumber < 16)
        {
            blockPool = 3;
        }
        else if (blockPoolNumber > 15)
        {
            blockPool = 4;
        }
        return blockPool;
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
