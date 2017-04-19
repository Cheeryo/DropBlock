using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [HideInInspector] public List<PlayerController> players = new List<PlayerController>();

    [SerializeField] private float lerpSpeed;
    [SerializeField] private float zoomMultiplier;
    private Vector3 startPosition;

	private void Start ()
    {
        startPosition = Camera.main.transform.position;
	}
	
	private void LateUpdate ()
    {
        if (players.Count == 0) return;

        Bounds b = CalculateBounds();
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(b.center.x, b.center.y, startPosition.z - Mathf.Max(Mathf.Abs(b.size.y), Mathf.Abs(b.size.x) * .5f) * zoomMultiplier), Time.deltaTime * lerpSpeed);
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
