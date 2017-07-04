using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<PlayerController> players = new List<PlayerController>();

    [SerializeField] private Vector3 offset;
    
    private GameManager manager;

	private void Start ()
    {
        manager = GetComponent<GameManager>();
	}
	
	private void LateUpdate ()
    {
        if (players.Count == 0) return;

        Bounds b = CalculateBounds();
        float distance = Mathf.Max(20, b.size.y, b.size.z) / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));
        Camera.main.transform.position = offset + new Vector3(0, b.center.y, 0) - Camera.main.transform.forward * distance * (0.85f +b.size.y * .01f);
    }

    private Bounds CalculateBounds()
    {
        float minX, minY, maxX, maxY;

        List<Vector3> positions = new List<Vector3>();
        foreach(PlayerController p in players.Where(o => !o.GoalReached))
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
