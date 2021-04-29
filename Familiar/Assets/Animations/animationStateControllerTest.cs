using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateControllerTest : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isAttackingHash; // :)

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackingHash = Animator.StringToHash("isAttacking"); // :)
    }

    // Update is called once per frame
    void Update()
    {
        bool isrunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isAttacking = animator.GetBool(isAttackingHash); // :)
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool attackPressed = Input.GetKey("e"); // :)

        //if player presses w key
        if (!isWalking && forwardPressed)
        {
            //then set the isWalking boolean to be true
            animator.SetBool(isWalkingHash, true);
        }

        //if player is not pressing w key
        if (isWalking && !forwardPressed)
        {
            //set the isWalking boolean to false
            animator.SetBool(isWalkingHash, false);
        }

        //if player is walking and not running and presses left shift
        if (!isrunning && (forwardPressed && runPressed))
        {
            //set isRunning boolean to be true
            animator.SetBool(isRunningHash, true);
        }

        //if player is running and stops running or stops walking
        if(isrunning && (!forwardPressed || !runPressed))
        {
            //then set isRunning boolean to be false
            animator.SetBool(isRunningHash, false);
        }

        if(attackPressed) // :)
        { // :)
            animator.SetBool(isAttackingHash, true); // :)
        } // :)
        else
        {   //:)
            animator.SetBool(isAttackingHash, false); //:)
        } // :)
    }
}
