﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerCone;
    [SerializeField] private GameObject darkness;
    private float direction;
    private float jumpHeight = 300f;
    private float moveSpeed = 10f;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;

    void Start()
    {

    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        if (move > 0)
            moveRight = true;
        else if (move < 0)
            moveLeft = true;
        else
            animator.SetBool("moving", false);
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {

        if (moveRight)
        {
            if (Globals.level > 8)
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            animator.SetBool("moving", true);
            transform.localScale = new Vector3(10, 10, 1);
            moveRight = false;
        }
        if (moveLeft)
        {
            if (Globals.level > 8)
                transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            animator.SetBool("moving", true);
            transform.localScale = new Vector3(-10, 10, 1);
            moveLeft = false;
        }
        //TODO: top check and bottom check for jump
        if (jump)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0));
            jump = false;
        }
        checkJumpAnimation();
        setConeWidth();
    }
    private void setConeWidth()
    {
        //TODO: cone width based on level. Smaller in lower level, bigger in higher,
        //      disable cone and darkness if level>8
    }

    private void checkJumpAnimation()
    {
        if (rb.velocity.y > 0.1)
        {
            animator.SetBool("jumping", true);
            animator.SetBool("falling", false);
        }
        if (rb.velocity.y < 0.1)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
        }
        if (rb.velocity.y == 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
        }

    }
    private void winGame()
    {
        Debug.Log("WIN! Time to meet our parents. Wait.....");
        //TODO: implementation
    }
    private void loseGame()
    {
        Debug.Log("GG die");
        //TODO: implementation
    }
    private void success()
    {
        //TODO: Scene transition to bedroom.
        Debug.Log("Player has won. Levels: " + Globals.level);
        if (Globals.level == Globals.maxLevel)
            winGame();
        else
            Globals.level++;
    }
    public void takeDamage()
    {
        //TODO: implement 'wakeup' state. Scene transition to bedroom. 
        Debug.Log("Player has been touched. Levels: " + Globals.level);
        if (Globals.level == Globals.minLevel)
            loseGame();
        else
            Globals.level--;
    }
    public void increaseConeLength(float width)
    {
        playerCone.transform.localScale = new Vector3(playerCone.transform.localScale.x, playerCone.transform.localScale.y + width, 1);
        playerCone.transform.position = new Vector3(playerCone.transform.position.x + (width / 2), playerCone.transform.position.y, 1);
    }
    public void decreaseConeLength(float width)
    {
        playerCone.transform.localScale = new Vector3(playerCone.transform.localScale.x, playerCone.transform.localScale.y - width, 1);
        playerCone.transform.position = new Vector3(playerCone.transform.position.x - (width / 2), playerCone.transform.position.y, 1);
    }
}