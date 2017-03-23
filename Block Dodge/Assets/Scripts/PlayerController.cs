using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool isGrounded;
    public bool isJumping;

    public Rigidbody rb;

    public float sideSpeed;
    public float moveSpeed;
    public float jumpForce;


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isJumping = true;
                jumpForce = 200;
            }
        }
        Debug.Log(isGrounded);
	}

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(sideSpeed,jumpForce,0));
    } 

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("You hit it. " + other.collider.name);
        if (other.collider.tag == "Floor")
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Floor")
        {
            isGrounded = false;
            jumpForce = 0;
        }
    }
}
