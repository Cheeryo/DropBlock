using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

    public float inputDelay = 0.1f;
    public float forwardVel = 12;
    public float jumpForce = 0;

    public bool isGrounded;
    public bool isMidAir;
    public bool isHanging;

    public Rigidbody rb;
    float forwardInput;

    void Start ()
    {
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("No Rigidbody");
        }
        forwardInput = 0;
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isMidAir = true;
                jumpForce = 3;
            }
        }
    }

    void Update()
    {
        GetInput();
        Debug.Log(jumpForce);
    }
	
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move ()
    {
        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            //move
            rb.velocity = new Vector3(forwardInput * forwardVel,rb.velocity.y,0);
        }
        else
        {
            rb.velocity = new Vector3(0,rb.velocity.y,0);
        }
    }

    void Jump ()
    {
        if (isGrounded)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("You hit it. " + other.collider.name);
        if (other.collider.tag == "Floor")
        {
            isGrounded = true;
            isMidAir = false;
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
