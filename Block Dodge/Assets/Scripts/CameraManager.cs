using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float zoomMultiplier;

    private Vector3 startPosition;
    private PlayerController[] players;

	private void Start ()
    {
        startPosition = Camera.main.transform.position;
        players = GameObject.FindObjectsOfType<PlayerController>();
	}
	
	private void LateUpdate ()
    {
        Bounds b = CalculateBounds();
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(b.center.x, Mathf.Clamp(3.5f-b.size.y,0,3.5f) + b.center.y, startPosition.z - Mathf.Abs(b.size.y) * zoomMultiplier), Time.deltaTime * lerpSpeed);
    }

    private Bounds CalculateBounds()
    {
        float minX, minY, maxX, maxY;

        List<Vector3> positions = new List<Vector3>();
        foreach(PlayerController p in players)
        {
            positions.Add(p.transform.position);
        }

        positions = positions.OrderBy(o => o.x).ToList();
        maxX = positions[0].x;
        minX = positions[positions.Count - 1].x;
        positions = positions.OrderBy(o => o.y).ToList();

        maxY = positions[0].y;
        minY = positions[positions.Count - 1].y;

        return new Bounds(new Vector3((minX+maxX)*.5f,(minY + maxY) * .5f, 0), new Vector3(minX - maxX, minY - maxY, 0));
    }
}
