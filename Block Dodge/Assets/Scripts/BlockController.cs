using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    public Material corrupted;

    private Vector3 fallSpeed;
    private bool locked;

    private Rigidbody rb;
    private Renderer blockRend;

    public bool Locked
    {
        get
        {
            return locked;
        }

        set
        {
            locked = value;
        }
    }

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blockRend = GetComponent<Renderer>();
        blockRend.enabled = true;
        fallSpeed = new Vector3(0, DeclareFallSpeed(), 0);
    }
	
	private void Update ()
    {
        if (!Locked)
        {
            transform.Translate(fallSpeed * Time.deltaTime);
        }
    }

    private float DeclareFallSpeed()
    {
        float blockFallSpeed = 0;
        int declareNumber = (int) Random.Range(0.0f, 100.0f);
        if (declareNumber == 1 || declareNumber == 2)
        {
            blockFallSpeed = -1.0f;
        }
        else if (declareNumber >= 3 && declareNumber <= 6)
        {
            blockFallSpeed = -2.0f;
        }
        else if (declareNumber >= 7 && declareNumber <= 16)
        {
            blockFallSpeed = -3.0f;
        }
        else if (declareNumber >= 17 && declareNumber <= 32)
        {
            blockFallSpeed = -4.0f;
        }
        else if (declareNumber >= 33 && declareNumber <= 68)
        {
            blockFallSpeed = -5.0f;
        }
        else if (declareNumber >= 69 && declareNumber <= 84)
        {
            blockFallSpeed = -6.0f;
        }
        else if (declareNumber >= 85 && declareNumber <= 94)
        {
            blockFallSpeed = -7.0f;
        }
        else if (declareNumber >= 95 && declareNumber <= 98)
        {
            blockFallSpeed = -8.0f;
        }
        else if (declareNumber == 99 || declareNumber == 100)
        {
            blockFallSpeed = -10.0f;
        }
        return blockFallSpeed;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Block") || col.collider.CompareTag("Level"))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block") || other.CompareTag("Level"))
        {
            LockBlock();
        }
        if (other.CompareTag("Player"))
        {
            //Destroy(gameObject,1);
        }
    }

    public void LockBlock()
    {
        Locked = true;
        transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
        rb.isKinematic = true;
    }

    public void UnlockBlock()
    {
        Locked = false;
        rb.isKinematic = false;
    }

    public void DespawnBlock ()
    {
        blockRend.sharedMaterial = corrupted;
        (gameObject.GetComponent(typeof(Collider)) as Collider).enabled = false;
        LockBlock();
        Destroy(gameObject, 2.0f);
    }
}
