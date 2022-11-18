using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [HeaderAttribute("Movement")]
    private float MovementHorizontal = 0f;
    [SerializeField] private float MovementVelocity;
    [Range(0, 0.3f)] [SerializeField] private float SmoothMovement;
    private Vector3 Velocity = Vector3.zero;
    private bool IsRight = true;
    public bool CanMove = true;
    [SerializeField] private Vector2 ReboundVelocity;

    [Header("Jump")]
    [SerializeField] private float JumpForce;
    [SerializeField] private LayerMask IsFloor;
    [SerializeField] private Transform CheckFloor;
    [SerializeField] private Vector3 BoxDimesion;
    [SerializeField] private bool OnFloor;
    private bool jump = false;

    //Variables for the trail
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private GameObject timeClonePrefab;
    private GameObject timeClone;

    //Variables of time loop
    private Vector2 finalValue;
    private bool exitLoop;
    private int seconds = 5;
    private float remainingTime;
    private bool runningTime;
    private bool loop = true;
    [SerializeField] private TimeLineBar timeLineBar;

    [Header("Animation")]
    private Animator animator;
    [SerializeField] private AudioClip soundJump;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        MovementHorizontal = Input.GetAxisRaw("Horizontal") * MovementVelocity;

        animator.SetFloat("Horizontal", Mathf.Abs(MovementHorizontal));
        animator.SetFloat("YVelocity", rb2d.velocity.y);

        if(Input.GetButtonDown("Jump")){
            jump = true;
        }

        // key to start time loop and check loop
        if (Input.GetKey(KeyCode.R) && loop == true)
        {
            loop = false;
            Transform exit = GetComponent<Transform>();
            finalValue = exit.position;
            StartTime();
            StartTrail();
            timeLineBar.EmptyBar();
            Invoke("CoolDownLoop", 5f);

        }

        //Check end of time loop
        if (runningTime)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 1)
            {
                runningTime = false;
                exitLoop = true;
            }
        }

        //Move player after time loop ends
        if (exitLoop)
        {
            Transform finalPosition = GetComponent<Transform>();
            finalPosition.DOMove(finalValue, 1);
            timeLineBar.ReloadBar();
            exitLoop = false;
            StopTrail();
        }
    }

    //Start the time loop
    public void StartTime()
    {
        remainingTime = seconds;
        runningTime = true;
    }

    private void FixedUpdate() {

        OnFloor = Physics2D.OverlapBox(CheckFloor.position, BoxDimesion, 0f, IsFloor);
        animator.SetBool("OnFloor", OnFloor);

        //Move player
        if (CanMove)
        {
            Move(MovementHorizontal * Time.fixedDeltaTime, jump);
        }
        jump = false;
    }

    private void Move(float move, bool jump){
        Vector3 VelocityObject = new Vector2(move, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, VelocityObject, ref Velocity, SmoothMovement);

        //Turn the player
        if(move > 0 && !IsRight)
        {
            Turn();
        }
        else if(move < 0 && IsRight)
        {
            Turn();
        }

        //Jump and jump sound
        if(OnFloor && jump){
            OnFloor = false;
            rb2d.AddForce(new Vector2(0f, JumpForce));
            SoundController.Instance.PlaySound(soundJump);
        }
    }

    public void Rebound(Vector2 HitPoint){
        rb2d.velocity = new Vector2(-ReboundVelocity.x * HitPoint.x, ReboundVelocity.y);
    }

    private void Turn (){
        IsRight = !IsRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(CheckFloor.position, BoxDimesion);
    }

    //Reload the loop
    private void CoolDownLoop()
    {
        loop = true;
    }


    //Use trail effect
    private void StartTrail()
    {
        //Emmit trail
        trailRenderer.emitting = true;

        //Generate clone
        timeClone = Instantiate(timeClonePrefab, transform.position + Vector3.back , transform.rotation);
        timeClone.transform.localScale = transform.localScale;
    }

    private void StopTrail()
    {
        //Stop trail
        trailRenderer.emitting = false;
        trailRenderer.Clear();

        //Delete Clone
        Destroy(timeClone);
    }
}
