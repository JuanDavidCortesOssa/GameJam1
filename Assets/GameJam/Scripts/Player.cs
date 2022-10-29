using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    private bool isFacingRight;


    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //Variables of time loop
    private Vector2 finalValue;
    public static bool exitLoop = false;
    private int seconds = 5;
    private float remainingTime;
    private bool runningTime;


    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
        }

        // key to start time loop
        if (Input.GetKey(KeyCode.L))
        {
            Transform exit = GetComponent<Transform>();
            finalValue = exit.position;
            StartTime();
        }

        //Check end of time loop
        if (runningTime)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 1)
            {
                exitLoop = true;
                runningTime = false;
            }
        }

        //Move player after time loop ends
        if (exitLoop)
        {
            Transform FinalPosition = GetComponent<Transform>();
            FinalPosition.DOMove(finalValue, 1);
            exitLoop = false;
        }

        Flip();
    }

    //Start the time loop
    public void StartTime()
    {
        remainingTime = seconds;
        runningTime = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void MovePlayer()
    {
        rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


}
