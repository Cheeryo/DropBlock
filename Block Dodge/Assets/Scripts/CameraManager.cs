using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lerpSpeed;
    private Vector3 offset;

	private void Start ()
    {
        offset = Camera.main.transform.position - player.position;
	}
	
	private void LateUpdate ()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, player.position.y + offset.y, Camera.main.transform.position.z), Time.deltaTime * lerpSpeed);
    }
}
