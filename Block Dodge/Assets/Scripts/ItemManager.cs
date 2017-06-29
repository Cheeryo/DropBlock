using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Settings")][Range(.5f, 25)]
    [SerializeField] private float spawnInterval = 10f;
    [Header("Prefabs")]
    [SerializeField] private GameObject[] items;

    private GameManager manager;
    private Transform itemContainer;

    private void Start()
    {
        manager = GetComponent<GameManager>();
        itemContainer = GameObject.Find("Gameplay/Items").transform;
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameObject.Instantiate(items[Random.Range(0,items.Length)], CalculatePosition() + Vector3.up * .5f, Quaternion.identity, itemContainer);
        }
    }

    private Vector3 CalculatePosition()
    {
        Vector3 result = new Vector3(0, 4.5f, 0);
        Vector3 pos = new Vector3(Random.Range((int)-manager.levelWidth, (int)manager.levelWidth) + .5f, manager.levelHeight, 0);
        RaycastHit[] hit = Physics.RaycastAll(pos + Vector3.down, Vector3.down, manager.levelHeight + 10);
        Debug.DrawRay(pos + Vector3.down, Vector3.down * (manager.levelHeight + 10), Color.red);
        hit = hit.Where(o => (o.collider.CompareTag("Block") && o.collider.GetComponent<BlockController>().Locked) || (o.collider.CompareTag("Level"))).OrderByDescending(o => o.point.y).ToArray();

        if (hit.Length > 0)
            result = hit[0].point;
        return result;
    }
}
