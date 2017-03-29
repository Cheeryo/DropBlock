using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    private float blockFallSpeed = -5.0f;
    Vector3 finalSpeed;
    private bool negateMoving;
    private Vector3 locked;

	// Use this for initialization
	void Start () {
        negateMoving = false;
        finalSpeed = new Vector3(0, blockFallSpeed, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(negateMoving);
        transform.Translate(finalSpeed * Time.deltaTime);
        if (negateMoving)
        {
            transform.position = locked;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Cube") || col.collider.CompareTag("Floor"))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube") || other.CompareTag("Floor"))
        {
            finalSpeed = Vector3.zero;
            locked = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
            negateMoving = true;
        }
        if (other.CompareTag("Player"))
        {
            //Destroy(gameObject,1);
        }
    }
}
