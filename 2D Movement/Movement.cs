using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    Rigidbody2D rb;
    Vector2 movement;
    Animator animator;
    float lastMoveX;
    float lastMoveY;
    bool lastmove;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        if(Math.Abs(movement.x) > 0)
        {
            lastMoveX = movement.x;
            lastmove = true;
        }

        if(Math.Abs(movement.y) > 0)
        {
            lastMoveY = movement.y;
            lastmove = false;
        }

        if(movement.x == 0 && movement.y == 0)
        {
            if (lastmove)
            {
                animator.SetFloat("IdleHorizontal", lastMoveX);
                animator.SetFloat("IdleVertical", 0);
            }
            else
            {
                animator.SetFloat("IdleVertical", lastMoveY);
                animator.SetFloat("IdleHorizontal", 0);
            }
        }
    }

    private void FixedUpdate()
    {
        MovePosition();
    }

    private void MovePosition()
    {
        rb.MovePosition(rb.position + movement.normalized * MoveSpeed * Time.fixedDeltaTime);
    }
}
