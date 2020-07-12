using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerCone;
    [SerializeField] private GameObject darkness;
    [SerializeField] private GameObject sleepText;

    private Scene currentScene;
    private string sceneName;
    private float direction;
    private float jumpHeight = 300f;
    private float moveSpeed = 10f;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;
    private bool nearBed = false;
    private bool fPress = false;
    private float levelLength = 0;
    private float levelTime = 30;

    void Start()
    {
        levelLength = Time.time + levelTime;
    }

    void Update()
    {
        if (Time.time > levelLength)
        {
            success();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            fPress = true;
        }
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
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
            if (sceneName == "Room")
            {
                playerCone.SetActive(false);
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                playerCone.SetActive(true);
            }
            if (Globals.level > 8)
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            }
            animator.SetBool("moving", true);
            transform.localScale = new Vector3(10, 13, 1);
            moveRight = false;
        }
        if (moveLeft)
        {
            if (sceneName == "Room")
            {
                playerCone.SetActive(false);
                transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                playerCone.SetActive(true);
            }
            if (Globals.level > 8)
                transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            animator.SetBool("moving", true);
            transform.localScale = new Vector3(-10, 13, 1);
            moveLeft = false;
        }
        if (jump)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0));
            jump = false;
        }
        if (fPress && nearBed)
        {
            fPress = false;
            goToDream();
        }
        checkJumpAnimation();
        setConeWidth();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bed")
        {
            showSleepText();
            nearBed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "bed")
        {
            hideSleepText();
            nearBed = false;
        }
    }
    private void goToDream()
    {
        Debug.Log("Sleep");
        SceneManager.LoadScene(1);
    }
    private void showSleepText()
    {
        sleepText.SetActive(true);
    }
    private void hideSleepText()
    {
        sleepText.SetActive(false);
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
        //TODO: scene transition to game win text and exit.
        SceneManager.LoadScene(3);
    }
    private void loseGame()
    {
        Debug.Log("GG die");
        //TODO: scene transition to game lose text and exit.
        //TODO: implementation
        SceneManager.LoadScene(4);
    }
    private void success()
    {
        //TODO: Scene transition to bedroom.
        Debug.Log("Player has won. Levels: " + Globals.level);
        if (Globals.level == Globals.maxLevel)
            winGame();
        else
        {
            SceneManager.LoadScene(2);
            Globals.level++;
        }
    }
    public void takeDamage()
    {
        //TODO: implement 'wakeup' state. Scene transition to bedroom. 
        Debug.Log("Player has been touched. Levels: " + Globals.level);
        if (Globals.level == Globals.minLevel)
            loseGame();
        else
        {
            SceneManager.LoadScene(2);
            Globals.level--;
        }
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