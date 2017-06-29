using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<PlayerController> players = new List<PlayerController>();

    [SerializeField] private float lerpSpeed;
    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float offsetY;

    private Vector3 startPosition;
    private GameManager manager;

	private void Start ()
    {
        startPosition = Camera.main.transform.position;
        manager = GetComponent<GameManager>();
	}
	
	private void LateUpdate ()
    {
        if (players.Count == 0) return;

        Bounds b = CalculateBounds();
        float distance = Mathf.Max(20, b.size.y, b.size.z) / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));
        Camera.main.transform.position = startPosition + new Vector3(startPosition.x, b.center.y + offsetY, -distance * zoomMultiplier);
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
