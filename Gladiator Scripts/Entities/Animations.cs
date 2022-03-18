using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Animation

        if (animator != null)
        {
            //If W, A, S or D is being pressed
            bool moving = (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S));
            //Set the Animator bool to true (moving is true if W, A, S or D is pressed)
            animator.SetBool("Moving", moving);

            //If left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                //Animator trigger "Punch" is turned on
                animator.Play("Attack");  
            }
        }
    }
}
