using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RunSpeed;
    public float JumpSpeed;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private BoxCollider2D playerfeet;
    private bool isGround;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerfeet = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Run();
        JudgeGround();
        Jump();
        Fall();
        Debug.Log(playerRigidbody.velocity.x + "  " + playerRigidbody.velocity.y);
    }
    void Run()
    {
        var move = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(move * RunSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVel;
        bool playerHasMoveX = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Run", playerHasMoveX);
        if (playerHasMoveX)
        {
            if (playerRigidbody.velocity.x > 0.1f)
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            else if (playerRigidbody.velocity.x < -0.1f)
                transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Jump()
    {
        if (isGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                playerAnimator.SetBool("Jump", true);
                var jumpVel = new Vector2(0.0f, JumpSpeed);
                playerRigidbody.velocity = jumpVel;
            }
        }
    }
    void Fall()
    {
        if (playerRigidbody.velocity.y < 0.1f)
        {
            playerAnimator.SetBool("Fall", true);
            playerAnimator.SetBool("Jump", false);
        }
        if (isGround)
            playerAnimator.SetBool("Fall", false);
    }
    void JudgeGround()
    {
        isGround = playerfeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
