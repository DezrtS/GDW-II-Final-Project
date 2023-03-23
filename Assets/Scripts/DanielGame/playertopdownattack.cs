using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playertopdownattack : MonoBehaviour
{
    //Attack values

    [SerializeField] GameObject attackBoxObject;
    SpriteRenderer attackBoxRend;
    playermovementballgame playermovementballgame;
    Animator animator;
    Collider2D attackBox;
    public bool canAttack = true;
    public float attackTime;
    public float attackTimeMax;
    public float attackCoolOff;
    public float attackCoolOffMax;
    public Vector2 upDirection;

    public float startTime;
    // Start is called before the first frame update
    void Start() 
    {
        playermovementballgame = GetComponent<playermovementballgame>();
        attackBox = attackBoxObject.GetComponentInChildren<Collider2D>();
        attackBoxRend = attackBoxObject.GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        AttackBoxReady();
    }
    void AttackBoxReady()
    {// this will need to be changed once an animation is created
        if (attackBox.enabled)//when active
        {
            //attackBoxRend.color = Color.yellow;
            animator.SetInteger("animationNum", 1);
        }
        else if (canAttack)//when off cool down
        {
            //attackBoxRend.color = Color.green;
            animator.SetInteger("animationNum", 0);
        }
        else//when on cool down
        {
            //attackBoxRend.color = Color.grey;
            animator.SetInteger("animationNum", 2);
        }
    }

    void Attack()
    {
        if (playermovementballgame.buttonInput && canAttack && Time.timeScale == 1)
        {
            startTime = Time.time;
            attackTime = startTime + attackTimeMax;
            attackCoolOff = startTime + attackTimeMax + attackCoolOffMax;
            canAttack = false;
        }
        if(attackTime > Time.time)
        {
            attackBox.enabled = true;
        }
        else 
        {
            //startTime = Time.time;
            //attackCoolOff = startTime + attackCoolOffMax;
            //if (attackCoolOff > Time.time)
            //{

            //}
            if (attackCoolOff < Time.time)
            {
                canAttack = true;
                
            }
            attackBox.enabled = false;
            
        }
    }
}
