using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    private float moveSpeed = 1f;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool playerChecked = false;


    void Start()
    {
        /*
        some TODOS:
            enemy spawn code
            view cone barra kro
            main hub
            text/story
        */
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        if (moveRight)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            //animator.SetBool("moving", true);
            transform.localScale = new Vector3(15, 15, 1);
        }
        if (moveLeft)
        {
            transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            //animator.SetBool("moving", true);
            transform.localScale = new Vector3(-15, 15, 1);
        }
        if (!playerChecked)
        {
            Debug.Log("Checking player location: " + playerChecked);
            RaycastHit2D checkRight = Physics2D.Raycast(transform.position, Vector2.right);
            RaycastHit2D checkLeft = Physics2D.Raycast(transform.position, Vector2.left);
            Debug.Log(checkRight.collider.tag);
            Debug.Log(checkLeft.collider.tag);
            if (checkRight.collider.tag != "Level")
            {
                Debug.Log("Player on the right!");
                moveRight = true;
                playerChecked = true;
            }
            else if (checkLeft.collider.tag != "Level")
            {
                Debug.Log("Player on the left!");
                moveLeft = true;
                playerChecked = true;
            }
        }


    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag == "Player")
        {
            player.GetComponent<PlayerController>().takeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Cone")
        {
            Debug.Log(other.tag);
            if (moveRight)
            {
                moveLeft = true;
                moveRight = false;
            }
            else if (moveLeft)
            {
                moveLeft = false;
                moveRight = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player Cone")
        {
            if (moveRight)
            {
                moveLeft = true;
                moveRight = false;
            }
            else if (moveLeft)
            {
                moveLeft = false;
                moveRight = true;
            }
        }
    }

}
