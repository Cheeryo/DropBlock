using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    private float blockFallSpeed;
    Vector3 finalSpeed;
    private bool negateMoving;
    private Vector3 locked;

	// Use this for initialization
	void Start () {
        negateMoving = false;
        declareFallSpeed();
        Debug.Log(blockFallSpeed);
        finalSpeed = new Vector3(0, blockFallSpeed, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(finalSpeed * Time.deltaTime);
        if (negateMoving)
        {
            transform.position = locked;
        }
    }

    private float declareFallSpeed()
    {
        int declareNumber = (int) Random.Range(1.0f, 26.0f);
        if (declareNumber <= 4)
        {
            blockFallSpeed = Random.Range(-15.0f, -1.0f);
        }
        else if (declareNumber == 5 || declareNumber == 6)
        {
            blockFallSpeed = -1.0f;
        }
        else if (declareNumber > 6 && declareNumber < 12)
        {
            blockFallSpeed = -3.0f;
        }
        else if (declareNumber > 11 && declareNumber < 19)
        {
            blockFallSpeed = -5.0f;
        }
        else if (declareNumber > 18 && declareNumber < 24)
        {
            blockFallSpeed = -10.0f;
        }
        else if (declareNumber == 24 || declareNumber == 25)
        {
            blockFallSpeed = -15.0f;
        }
        return blockFallSpeed;
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
