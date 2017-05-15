using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private int playerNumber;
    [SerializeField] private float inputDelay = 0.1f;

    [Header("Stats")]
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float forwardVel = 12;
    [SerializeField] private float firstJumpForce = 4;
    [SerializeField] private float secondJumpForce = 7;
    private float timer;

    private float currentEnergy;

    [Header("States")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isMidAir;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isFirstJumping;
    [SerializeField] private bool isSecondJumping;
    [SerializeField] private bool isHanging;
    [SerializeField] private bool canWallJump;
    private float wallJumpDirection;
    [SerializeField] private bool isRespawning;
    [SerializeField] private bool chargeRespawn;
    private bool spawnPossible = true;
    private bool directionFacing;
    private Vector3 facingVector;
    private bool movementPossible = true;

    [Header("Spawner")]
    [SerializeField] private GameObject spawnController;
    [SerializeField] private GameManager manager;
    [SerializeField] private Transform spawnContainer;
    private Vector3 spawnPosition;
    private float spawnControllerPosition;
    private bool pressedLR;
    private bool pressedRR;

    [SerializeField] private float chargeMax;
    private float chargeTimer;

    [Header("Misc")]
    [SerializeField] private Material[] playerMaterials;
    [SerializeField] private Material[] spawnerMaterials;

    private float score;
    private string winner;
    private Rigidbody rb;
    private Animator playerAnimator;
    private Renderer playerRend;
    private Renderer spawnerRend;
    private float forwardInput;
    private RaycastHit spawnHit;
    private Vector3 raycastPosition;
    public GameObject playerModel;

    [Header("Interface")]
    public Text scoreText;
    public Text winnerText;
    public Slider energySlider;
    public Image energyBar;
    public Color energyAbove75 = Color.green;
    public Color energyAbove50 = Color.yellow;
    public Color energyAbove25;
    public Color energyAbove0 = Color.red;
    public Slider chargeSlider;
    public Image chargeSliderBar;
    public Color chargeSliderColor;

    private string moveButton;
    private string jumpButton = "Jump_P1";
    private string respawnButton = "Respawn_P1";
    private string itemButton = "UseItem_P1";
    private string spawnerRightButton = "SpawnerRight_P1";
    private string spawnerLeftButton = "SpawnerLeft_P1";

    public GameObject headPosition;

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerRend = GetComponentInChildren<Renderer>();
        spawnerRend = spawnController.GetComponent<Renderer>();
        playerRend.enabled = spawnerRend.enabled = spawnPossible = directionFacing = true;
        forwardInput = 0;
        pressedLR = pressedRR = chargeRespawn = canWallJump = false;
        currentEnergy = maxEnergy;
        moveButton = "Horizontal_P" + playerNumber;
        jumpButton = "Jump_P" + playerNumber;
        respawnButton = "Respawn_P" + playerNumber;
        itemButton = "UseItem_P" + playerNumber;
        spawnerRightButton = "SpawnerRight_P" + playerNumber;
        spawnerLeftButton = "SpawnerLeft_P" + playerNumber;
    }

    private void GetInput()
    {
        if (!isRespawning)
        {
            forwardInput = Input.GetAxis(moveButton);

            if (isGrounded && Input.GetButtonDown(jumpButton))
            {
                timer = 0f;
                isJumping = true;
                isFirstJumping = true;
                isSecondJumping = true;
            }
            if (Input.GetButtonUp(jumpButton))
            {
                isSecondJumping = false;
                isJumping = false;
            }
            if (isHanging && !isMidAir && Input.GetButtonDown(jumpButton))
            {
                canWallJump = true;
            }
            if (Input.GetButtonDown(respawnButton))
            {
                chargeRespawn = true;
            }
            if (Input.GetButtonUp(respawnButton))
            {
                chargeRespawn = false;
                chargeTimer = 0;
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
        Debug.Log(isMidAir);
        GetInput();
        PlayerDirection();
        GroundCheck();
        WallCheck();
        spawnMovementControl();
        CheckSpawner();
        SetUI();
        CheckRespawn();
        ForceRespawn();
        if (isHanging)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        }
    }
	
    private void FixedUpdate()
    {
        Move();
        Jump();
        WallJump();
        SetAnimator();
    }

    private void Move ()
    {
        if (!movementPossible) return;

        if (Mathf.Abs(forwardInput) > inputDelay) //move
        {
            rb.velocity = new Vector3(forwardInput * forwardVel, rb.velocity.y, 0);
        }
        else
        {
            if(!isGrounded)
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            else
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void PlayerDirection()
    {
        //DirectionFacing = true --> Character looking right
        //DirectionFacing = false --> Character looking left
        if (forwardInput < 0)
        {
            directionFacing = true;
        }
        else if (forwardInput > 0)
        {
            directionFacing = false;
        }
        if (directionFacing)
        {
            facingVector = Vector3.left;
            if (isHanging)
            {
                playerModel.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (!isHanging)
            {
                playerModel.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
        else if (!directionFacing)
        {
            facingVector = Vector3.right;
            if (isHanging)
            {
                playerModel.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (!isHanging)
            {
                playerModel.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }        
    }

    private void Jump ()
    {
        timer += Time.deltaTime;
        if (isJumping)
        {
            if (isFirstJumping)
            {
                rb.AddForce(0, firstJumpForce, 0, ForceMode.Impulse);
                isFirstJumping = false;
            }            
            if (isSecondJumping && timer > 0.1f)
            {
                rb.AddForce(0, secondJumpForce, 0, ForceMode.Impulse);
                isSecondJumping = false;
                isJumping = false;
            } 
        }
    }

    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isHanging = canWallJump = false;
            forwardInput = 0;
            movementPossible = false;
            rb.AddForce(new Vector3(10, 10, 0), ForceMode.Impulse);
            StartCoroutine(AfterWallJump(.2f));
        }
    }
    
    private IEnumerator AfterWallJump(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        movementPossible = true;
    }

    private void SetAnimator()
    {
        playerAnimator.SetFloat("MoveSpeed",forwardInput);
        playerAnimator.SetBool("Grounded", isGrounded);
        playerAnimator.SetBool("Jumping", isJumping);
        playerAnimator.SetBool("Hanging", isHanging);
        playerAnimator.SetBool("WallJumping", canWallJump);
    }

    private void SetUI()
    {
        energySlider.value = currentEnergy;
        chargeSlider.maxValue = chargeMax;
        chargeSlider.value = chargeTimer;
        chargeSliderBar.color = chargeSliderColor;

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
        Vector3 raycastPosition = new Vector3(spawnControllerPosition, manager.levelHeight, 0);
        if (Physics.Raycast(raycastPosition, Vector3.down, out spawnHit, manager.levelHeight))
        {
            if (spawnHit.collider.CompareTag("Block") && spawnHit.collider.GetComponent<BlockController>().Locked && !spawnHit.collider.GetComponent<BlockController>().IsCorrupted || spawnHit.collider.CompareTag("Level"))
            {              
                if (spawnObject.CompareTag("Player"))
                {
                    spawnPosition.x = spawnHit.point.x;
                    spawnPosition.y = spawnHit.point.y +2;
                    spawnObject.transform.position = spawnPosition;
                }
                else if (spawnObject.CompareTag("Item"))
                {
                    GameObject.Instantiate(spawnObject, spawnPosition, Quaternion.Euler(0, 0, 0), spawnContainer);
                }
            }    
        }
    }

    private void CheckSpawner()
    {
        if (spawnPossible)
        {
            spawnerRend.sharedMaterial = spawnerMaterials[0];
        }
        else
        {
            spawnerRend.sharedMaterial = spawnerMaterials[1];
        }
    }

    private void CheckRespawn()
    {
        Vector3 raycastPosition = new Vector3(spawnControllerPosition, manager.levelHeight, 0);
        if (Physics.Raycast(raycastPosition, Vector3.down, out spawnHit, manager.levelHeight))
        {
            if (spawnHit.collider.CompareTag("Block") && !spawnHit.collider.GetComponent<BlockController>().Locked)
            {
                spawnPossible = false;
            }
            else
            {
                spawnPossible = true;
            }
        }
    }
    
    private void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, .9f))
        {
            if (hit.collider.CompareTag("Level") || hit.collider.CompareTag("Block"))
            {
                //isRespawning = false;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    private void WallCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(headPosition.transform.position, facingVector, Color.red);
        if (Physics.Raycast(headPosition.transform.position, facingVector, out hit, 0.3f) && !canWallJump)
        {
            if ((hit.collider.CompareTag("Level") || hit.collider.CompareTag("Block")) && !isGrounded)
            {
                isHanging = true;
            }
            else
            {
                if (isMidAir)
                {
                    isHanging = false;
                }               
            }
        }
        else
        {
            if (isMidAir)
            {
                isHanging = false;
            }
        }
    }

    private void ForceRespawn()
    {
        
        if (spawnPossible && chargeRespawn)
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= chargeMax)
            {
                chargeRespawn = false;
                chargeTimer = 0;
                Die();          
            }
        }
    }

    private void Die()
    {
        isRespawning = true;
        forwardInput = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        playerRend.sharedMaterial = playerMaterials[1];
        currentEnergy -= 20;
        Invoke("Respawn",0.25f);
    }

    private void Respawn()
    {
        SpawnUseControl(gameObject);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        playerRend.sharedMaterial = playerMaterials[0];
        forwardInput = 0;
        isRespawning = false;
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
            currentEnergy -= 10;
            //Locks (freezes) the cube to prevent bugs in the editor -> will not be needed later due to players death on this event
            col.gameObject.GetComponent<BlockController>().DespawnBlock();
        }
        isMidAir = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        isMidAir = true;
    }
    private void OnTriggerEnter(Collider other)
    {
 
    }
}
