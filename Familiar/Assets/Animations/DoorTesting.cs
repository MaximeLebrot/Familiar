using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTesting : MonoBehaviour
{
    Animator animator;
    private readonly string openString = "open";
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool(openString, true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool(openString, false);
        }
    }
           

        
}
