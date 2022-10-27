using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float life;
    private MovementAnimation PlayerMove;
    [SerializeField] private float LoseControlTime;
    private Animator animator;

    private void Start() 
    {
        PlayerMove = GetComponent<MovementAnimation>();
        animator = GetComponent<Animator>();
    }


    public void TakeDamage(float damage)
    {
        life =-damage;
    }

    public void TakeDamage(float damage, Vector2 position){
        life -= damage;
        animator.SetTrigger("Hit");
        //Perder el control del personaje
        StartCoroutine(LoseControl());
        PlayerMove.Rebound(position);
    }

    private IEnumerator LoseControl()
    {
        PlayerMove.CanMove = false;
        yield return new WaitForSeconds(LoseControlTime);
        PlayerMove.CanMove = true;
    }
}
