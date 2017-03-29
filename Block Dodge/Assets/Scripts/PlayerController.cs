using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private float inputDelay = 0.1f;
    [SerializeField] private float forwardVel = 12;
    [SerializeField] private float jumpForce = 0;

    [Header("States")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isMidAir;
    [SerializeField] private bool isHanging;

    private Rigidbody rb;
    private float forwardInput;

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
        forwardInput = 0;
    }

    private void GetInput()
    {
        forwardInput = Input.GetAxis("Horizontal");

        if (isGrounded && Input.GetButtonDown("Jump"))
            isJumping = true;
    }

    private void Update()
    {
        GetInput();
        GroundCheck();
    }
	
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move ()
    {
        if (Mathf.Abs(forwardInput) > inputDelay) //move
            rb.velocity = new Vector3(forwardInput * forwardVel,rb.velocity.y,0);
        else
            rb.velocity = new Vector3(0,rb.velocity.y,0);
    }

    private void Jump ()
    {
        if (isJumping)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isJumping = false;
        }
    }
    
    private void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, .6f))
        {
            if (hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Cube"))
            {
                isGrounded = true;
                isMidAir = false;
            }
            else
            {
                isGrounded = false;
                isMidAir = true;
            }
        }
        else
        {
            isGrounded = false;
            isMidAir = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        /*
            # the player was hit by an GameObject with tag 'Cube'
            # the collision point was within the upper half of the player -> cube above player
            # the cube is currently falling
        */
        if (col.collider.CompareTag("Cube") && col.contacts[0].point.y > transform.position.y && (col.rigidbody.velocity.y > .5f || col.rigidbody.velocity.y < -.5f))
            Debug.Log("You were squeezed by a cube!");
    }
}
