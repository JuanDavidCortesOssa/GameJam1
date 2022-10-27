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

    [Header("Animation")]
    private Animator animator;

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
    }

    private void FixedUpdate() {

        OnFloor = Physics2D.OverlapBox(CheckFloor.position, BoxDimesion, 0f, IsFloor);
        animator.SetBool("OnFloor", OnFloor);
        //Mover
        if (CanMove)
        {
            Move(MovementHorizontal * Time.fixedDeltaTime, jump);
        }
        jump = false;
    }

    private void Move(float move, bool jump){
        Vector3 VelocityObject = new Vector2(move, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, VelocityObject, ref Velocity, SmoothMovement);

        if(move > 0 && !IsRight)
        {
            //Girar
            Turn();
        }
        else if(move < 0 && IsRight)
        {
            //Girar
            Turn();
        }

        if(OnFloor && jump){
            OnFloor = false;
            rb2d.AddForce(new Vector2(0f, JumpForce));
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
}
