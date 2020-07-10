using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float direction;
    [SerializeField] private float jumpHeight = 50f;
    private float moveSpeed = 10f;
    private bool moveLeft = false;
    private bool moveRight = false;
    bool jump = false;
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
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            moveRight = false;
        }
        if (moveLeft)
        {
            transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            moveLeft = false;
        }
        //TODO: top check and bottom check for jump
        if (jump)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0));
            jump = false;
        }
    }
}