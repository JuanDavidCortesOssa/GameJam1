using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingLife : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float maxLife;

    private void Start()
    {
        life = maxLife; 
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Healing(float heal)
    {
        if((life + heal) > maxLife)
        {
            life = maxLife;
        }
        else
        {
            life += heal;
        } 
    }
}
