using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private float inputDelay = 0.1f;

    [Header("Stats")]
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float forwardVel = 12;
    [SerializeField] private float jumpForce = 0;

    private float currentEnergy;

    [Header("States")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool jumpUsed;
    [SerializeField] private bool isMidAir;
    [SerializeField] private bool isHanging;
    [SerializeField] private bool isRespawning;

    [Header("Spawner")]
    [SerializeField] private GameObject spawnController;
    [SerializeField] private GameManager manager;
    [SerializeField] private Transform spawnContainer;
    private float spawnControllerPosition;
    private bool pressedLR;
    private bool pressedRR;

    [Header("Misc")]
    [SerializeField] private Material[] materials;

    private float score;
    private Rigidbody rb;
    private Renderer playerRend;
    private float forwardInput;

    [Header("Interface")]
    public Text scoreText;
    public Slider energySlider;
    public Image energyBar;
    public Color energyAbove75 = Color.green;
    public Color energyAbove50 = Color.yellow;
    public Color energyAbove25;
    public Color energyAbove0 = Color.red;

    [Header("Controls")]
    public string moveButton = "Horizontal_P1";
    public string jumpButton = "Jump_P1";
    public string respawnButton = "Respawn_P1";
    public string itemButton = "UseItem_P1";
    public string spawnerRightButton = "SpawnerRight_P1";
    public string spawnerLeftButton = "SpawnerLeft_P1";

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
        playerRend = GetComponentInChildren<Renderer>();
        playerRend.enabled = true;
        forwardInput = 0;
        pressedLR = pressedRR = false;
        currentEnergy = maxEnergy;
    }

    private void GetInput()
    {
        if (!isRespawning)
        {
            forwardInput = Input.GetAxis(moveButton);

            if (isGrounded && Input.GetButtonDown(jumpButton))
            {
                isJumping = true;
            }
            if (Input.GetButtonDown(respawnButton))
            {
                Die();
            }
            if (Input.GetButtonDown(itemButton))
            {

            }
            if (!pressedRR && Input.GetButtonDown(spawnerRightButton))
            {
                pressedRR = true;
            }
            if (!pressedLR && Input.GetButtonDown(spawnerLeftButton))
            {
                pressedLR = true;
            }
        }
    }

    private void Update()
    {
        GetInput();
        GroundCheck();
        spawnMovementControl();
        SetUI();
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
            jumpUsed = true;
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isJumping = false;
        }
    }

    private void SetUI()
    {
        energySlider.value = currentEnergy;

        if (currentEnergy > 75)
        {
            energyBar.color = energyAbove75;
        }
        else if (currentEnergy < 76 && currentEnergy > 50)
        {
            energyBar.color = energyAbove50;
        }
        else if (currentEnergy < 51 && currentEnergy > 25)
        {
            energyBar.color = energyAbove25;
        }
        else
        {
            energyBar.color = energyAbove0;
        }

        scoreText.text = "Score: " + score.ToString();
    }

    private void spawnMovementControl ()
    {
        spawnControllerPosition = spawnController.transform.position.x;
        if (pressedRR)
        {
            if (spawnControllerPosition < (manager.levelWidth -1))
            {
                spawnController.transform.position = new Vector3(spawnControllerPosition + 1, spawnController.transform.position.y, spawnController.transform.position.z);
            }
            pressedRR = false;
        }
        if (pressedLR)
        {
            if (spawnControllerPosition > (-manager.levelWidth +1))
            {
                spawnController.transform.position = new Vector3(spawnControllerPosition - 1, spawnController.transform.position.y, spawnController.transform.position.z);
            }
            pressedLR = false;
        }
    }

    private void SpawnUseControl(GameObject spawnObject)
    {
        RaycastHit hit;
        Vector3 raycastPosition = new Vector3(spawnControllerPosition, manager.levelHeight, 0);
        Vector3 spawnPosition = new Vector3(spawnControllerPosition,6,0);

        if (Physics.Raycast(raycastPosition, Vector3.down, out hit, manager.levelHeight))
        {
            if (hit.collider.CompareTag("Block") && hit.collider.GetComponent<BlockController>().Locked && !hit.collider.GetComponent<BlockController>().IsCorrupted || hit.collider.CompareTag("Level"))
            {
                spawnPosition = hit.point;
                
            }      
        }
        if (spawnObject.CompareTag("Player"))
        {
            spawnPosition.y += 2;
            spawnObject.transform.position = spawnPosition;
        }
        else if (spawnObject.CompareTag("Item"))
        {
            GameObject.Instantiate(spawnObject, spawnPosition, Quaternion.Euler(0, 0, 0), spawnContainer);
        }
    }
    
    private void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, .9f))
        {
            if (hit.collider.CompareTag("Level") || hit.collider.CompareTag("Block"))
            {
                isRespawning = false;
                isGrounded = true;
                isMidAir = false;
                jumpUsed = false;
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

    private void Die()
    {
        isRespawning = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        playerRend.sharedMaterial = materials[1];
        Respawn();
    }

    private void Respawn()
    {
        SpawnUseControl(gameObject);
        playerRend.sharedMaterial = materials[0];
        forwardInput = 0;
    }

    private void OnCollisionEnter(Collision col)
    {
        /*
            # the player was hit by an GameObject with tag 'Cube'
            # the collision point was within the upper half of the player -> cube above player
            # the cube is currently falling
        */
        if (col.collider.CompareTag("Block") && col.contacts[0].point.y > transform.position.y && !col.collider.GetComponent<BlockController>().Locked)
        {
            Debug.Log("You were squeezed by a cube!");
            currentEnergy -= 10;
            //Locks (freezes) the cube to prevent bugs in the editor -> will not be needed later due to players death on this event
            col.gameObject.GetComponent<BlockController>().DespawnBlock();
        }
    }
}
