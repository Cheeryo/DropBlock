using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    public bool isPlayerInGoal = false;
    public Animator platformAnimator;
    public Animator doorAnimatorLeft;
    public Animator doorAnimatorRight;
    private float goalTimer;
    private bool goalOpen;
    private bool halvePoints = false;
    public GameManager manager;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isPlayerInGoal)
        {
            if (!goalOpen)
            {
                goalTimer += Time.deltaTime;
            }            
        }
        else if (!isPlayerInGoal)
        {
            goalTimer = 0;
            halvePoints = false;
        }
        if (goalTimer >= 1.5f)
        {
            goalOpen = true;
            if (!halvePoints)
            {
                manager.goalScore /= 2;
                halvePoints = true;
            }            
            goalTimer = 0;
        }
	}

    void FixedUpdate()
    {
        SetAnimator();
    }

    private void SetAnimator()
    {
        platformAnimator.SetBool("PlayerInGoal", isPlayerInGoal);
        doorAnimatorLeft.SetBool("GoalOpenL", goalOpen);
        doorAnimatorRight.SetBool("GoalOpenR", goalOpen);
    }

    private void CloseGoal()
    {
        goalOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInGoal = true;
            if (goalOpen)
            {
                other.gameObject.GetComponent<PlayerController>().GoalReached = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !other.gameObject.GetComponent<PlayerController>().GoalReached && goalOpen)
        {
            other.gameObject.GetComponent<PlayerController>().score += manager.goalScore;
            other.gameObject.GetComponent<PlayerController>().GoalReached = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInGoal = false;
            Invoke("CloseGoal", 2);
            if (other.gameObject.GetComponent<PlayerController>().GoalReached)
            {
                other.gameObject.SetActive(false);
                manager.playerReachedGoal += 1;
            }
        }
    }
}
