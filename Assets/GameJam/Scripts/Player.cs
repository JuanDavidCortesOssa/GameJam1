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
    private Vector2 FinalValue;
    public static bool ExitLoop = false;
    private int Seconds = 5;
    private float RemainingTime;
    private bool RunningTime;


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
            Transform ExitLoop = GetComponent<Transform>();
            FinalValue = ExitLoop.position;
            StartTime();
        }

        //Check end of time loop
        if (RunningTime)
        {
            RemainingTime -= Time.deltaTime;
            if (RemainingTime < 1)
            {
                Player.ExitLoop = true;
                RunningTime = false;
            }
        }

        //Move player after time loop ends
        if (ExitLoop)
        {
            Transform FinalPosition = GetComponent<Transform>();
            FinalPosition.DOMove(FinalValue, 1);
            ExitLoop = false;
        }

        Flip();
    }

    //Start the time loop
    public void StartTime()
    {
        RemainingTime = Seconds;
        RunningTime = true;
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
