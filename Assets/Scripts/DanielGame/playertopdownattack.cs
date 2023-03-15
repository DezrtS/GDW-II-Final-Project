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
    Collider2D attackBox;
    public int attackTime;
    public int attackTimeMax;
    public int attackCoolOff;
    public int attackCoolOffMax;
    public Vector2 upDirection;
    // Start is called before the first frame update
    void Start()
    {
        playermovementballgame = GetComponent<playermovementballgame>();
        attackBox = attackBoxObject.GetComponentInChildren<PolygonCollider2D>();
        attackBoxRend = attackBoxObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        AttackBoxReady();
    }
    void AttackBoxReady()
    {// this will need to be changed once an animation is created
        if (attackTime > 0)//when active
        {
            attackBoxRend.color = Color.yellow;
        }
        else if (attackCoolOff == 0 & attackTime == 0)//when off cool down
        {
            attackBoxRend.color = Color.green;
        }
        else
        {
            attackBoxRend.color = Color.grey;//when on cool down
        }
    }

    void Attack()
    {
        if (playermovementballgame.buttonInput && attackCoolOff == 0)
        {
            attackTime = attackTimeMax;
            attackCoolOff = attackCoolOffMax;
        }
        if (attackTime > 0)
        {
            attackBox.enabled = true;

            attackTime--;
        }
        else
        {
            if (attackCoolOff > 0)
            {
                attackCoolOff--;
            }
            attackBox.enabled = false;
        }
    }
}
