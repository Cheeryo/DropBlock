using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    public Material[] materials;

    private Vector3 fallSpeed;
    private bool locked;
    private bool isCorrupted;

    private Rigidbody rb;
    private Renderer blockRend;
    public float blockLength;
    public float blockHeigth;
    public float positionOffset;
    public float energyMinus;
    public float scoreMinus;
    public enum State { Normal,Charge,Magnet,Pulse,Virus }
    public State blockState;

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

    public bool IsCorrupted
    {
        get
        {
            return isCorrupted;
        }
        set
        {
            isCorrupted = value;
        }
    }

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blockRend = GetComponent<Renderer>();
        blockRend.enabled = true;
        fallSpeed = new Vector3(0, DeclareFallSpeed(), 0);
        isCorrupted = false;
        GetBlockState();
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
        int declareNumber = (int) Random.Range(0,6);
        if (declareNumber == 0)
        {
            blockFallSpeed = -4.0f;
        }
        else if (declareNumber == 1) // 4%
        {
            blockFallSpeed = -5.0f;
        }
        else if (declareNumber == 2) // 4%
        {
            blockFallSpeed = -6.0f;
        }
        else if (declareNumber == 3) // 4%
        {
            blockFallSpeed = -7.0f;
        }
        else if (declareNumber == 4) // 4%
        {
            blockFallSpeed = -8.0f;
        }
        else if (declareNumber == 5) // 4%
        {
            blockFallSpeed = -9.0f;
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
            if (!locked)
            {
                LockBlock();
            }            
        }
        if (other.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            if (locked)
            {
                UnlockBlock();
            }
        }        
    }

    private void GetBlockState()
    {
        int hasState = (int)Random.Range(1,6);
        if (hasState == 1)
        {
            int stateNumber = (int)Random.Range(1, 2);
            if (stateNumber == 1)
            {
                blockState = State.Charge;
            }
        }

        switch (blockState)
        {
            case State.Charge:
                blockRend.sharedMaterial = materials[1];
                break;
        }
    }

    public void LockBlock()
    {
        Locked = true;
        transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y) + positionOffset, transform.position.z);
        rb.isKinematic = true;
    }

    public void UnlockBlock()
    {
        Locked = false;
        rb.isKinematic = false;
    }

    public void DespawnBlock ()
    {
        isCorrupted = true;
        blockRend.sharedMaterial = materials[0];
        Debug.Log("kakka");
        gameObject.tag = "Corrupted Object";
        gameObject.layer = 9;
        LockBlock();
        Destroy(gameObject, 2.0f);
    }
}
