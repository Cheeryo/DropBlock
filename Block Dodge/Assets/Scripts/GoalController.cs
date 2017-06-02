using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    private bool isPlayerInGoal = false;
    private Animator goalAnimator;
    private float goalTimer;
    private bool goalOpen;

	// Use this for initialization
	void Start () {

        goalAnimator = GetComponentInChildren<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		if (isPlayerInGoal)
        {
            goalTimer += Time.deltaTime;
        }
        else if (!isPlayerInGoal)
        {
            goalTimer = 0;
        }
        if (goalTimer >= 1.5f)
        {
            goalOpen = true;
        }
	}

    void FixedUpdate()
    {
        SetAnimator();
    }

    private void SetAnimator()
    {
        goalAnimator.SetBool("PlayerInGoal", isPlayerInGoal);
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
                other.gameObject.GetComponent<PlayerController>().goalReached = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInGoal = false;
            Invoke("CloseGoal", 2);
        }
    }
}
