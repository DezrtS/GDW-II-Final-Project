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

    private GameTimer attackTimer;
    private GameTimer cooldownTimer;

    public float startTime;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        attackTimer = new GameTimer(0.5f, true);
        cooldownTimer = new GameTimer(1f, true);

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Gameplay)
        {
            enabled = true;
            animator.speed = 1;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            animator.speed = 0;
        }
    }

    void Start() 
    {
        playermovementballgame = GetComponent<playermovementballgame>();
        attackBox = attackBoxObject.GetComponentInChildren<Collider2D>();
        attackBoxRend = attackBoxObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Attack();
        
        if (playermovementballgame.buttonInput && canAttack)
        {
            canAttack = false;
            attackBox.enabled = true;
            attackTimer.PauseTimer(false);
        }

        if (attackTimer.UpdateTimer())
        {
            if (!attackTimer.GetTimerAlreadyFinished())
            {
                cooldownTimer.PauseTimer(false);
                attackBox.enabled = false;
            }

            if (cooldownTimer.UpdateTimer())
            {
                canAttack = true;
                attackTimer.RestartTimer();
                cooldownTimer.RestartTimer();
                attackTimer.PauseTimer(true);
                cooldownTimer.PauseTimer(true);
            }
        }

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
