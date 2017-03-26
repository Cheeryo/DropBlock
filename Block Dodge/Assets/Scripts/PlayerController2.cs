using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

    public float inputDelay = 0.1f;
    public float forwardVel = 12;
    public float rotateVel = 100;

    Quaternion targetRotation;
    Rigidbody rb;
    float forwardInput, turnInput;

    void Start ()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("No Rigidbody");
        }
        forwardInput = turnInput = 0;
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Horizontal");
        //turnInput = Input.GetAxis("Horizontal");
    }

    void Update()
    {
        GetInput();
       // Turn();
    }
	
    void FixedUpdate()
    {
        Run();
    }

    void Run ()
    {
        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            //move
            rb.velocity = new Vector3(forwardInput * forwardVel,0,0);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Turn ()
    {
     //   targetRotation = Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
     //   transform.rotation = targetRotation;
    }
}
