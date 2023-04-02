using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class playermovementballgame : MonoBehaviour
{
    //Collects Scripts
    [SerializeField] Ball ballScript;
    [SerializeField] Hearts heartScript;
    //player components
    public Collider2D hitBox;
    Rigidbody2D body;
    Transform trans;
    public SpriteRenderer sprite;
    Animator animator;
        
    ////Attack values
    //[SerializeField] GameObject attackBoxObject;
    //SpriteRenderer attackBoxRend;
    //Collider2D attackBox;
    //public int attackTime;
    //public int attackTimeMax;
    //public int attackCoolOff;
    //public int attackCoolOffMax;
    
    public Vector2 upDirection;
    
    //Movement values
    public float speed;
    public float rotationStr;
    public float steeringInput;
    public float moveInput;
    public bool buttonInput;
    

    public Vector3 startPos;
    public quaternion startRot;

    public bool isDangerDodgeBall;
    //setting up the button inputs to string var
    public bool isPlayer1;
    string horizontal1 = "Horizontal",horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2= "Fire2";
    string horizontalInput, verticalInput, fireInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        animator = GetComponent<Animator>();
        animator.SetBool("isredplayer1", isPlayer1);

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
            body.simulated = true;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            body.simulated = false;
        }
    }


    void StartGetInputs()
    {
        if (isPlayer1)
        {
            horizontalInput = horizontal1;
            verticalInput = vertical1;
            fireInput = button1;
        }
        else
        {
            horizontalInput = horizontal2;
            verticalInput = vertical2;
            fireInput = button2;
        }
    }
    //end of setting up the button inputs to string var
    
    // Start is called before the first frame update
    void Start()
    {
        StartGetInputs();
        hitBox = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
        heartScript = gameObject.GetComponent<Hearts>();
        //animator = GetComponent<Animator>();
        startPos = trans.position;
        startRot = trans.rotation;
        //attackBox = attackBoxObject.GetComponentInChildren<PolygonCollider2D>();
        //attackBoxRend = attackBoxObject.GetComponentInChildren<SpriteRenderer>();
        //animator.SetBool("isredplayer1", isPlayer1);
        //if (isPlayer1)
        //{
            
        //    //use character red sprite
        //}
        //else
        //{
        //    //use character blue sprite
        //}
    }

    // Update is called once per frame
    void Update()
    {
        FindUp();
        FindInputs();
        //Attack();
        //AttackBoxReady();
        //Respawn();  
    }

    void FixedUpdate()
    {
        AntiCollision();
        Movement();
    }

    //    void Attack()
    //{
    //    if (buttonInput && attackCoolOff == 0)
    //    {
    //        attackTime = attackTimeMax;
    //        attackCoolOff = attackCoolOffMax;
    //    }
    //    if (attackTime > 0)
    //    {
    //        attackBox.enabled = true;
            
    //        attackTime--;
    //    } 
    //    else
    //    {
    //        if(attackCoolOff > 0)
    //        {
    //            attackCoolOff--;
    //        }
    //        attackBox.enabled = false;
    //    }
        //if (attackBox.enabled)
        //{
        //    attackBoxObject.SetActive(true);
        //}
        //else
        //{
        //    attackBoxObject.SetActive(false);
        //}
    //}
    void Movement()
    {
        AnimatorMethod();
        body.MovePosition(body.position + upDirection * moveInput * speed * Time.fixedDeltaTime);
        //body.MoveRotation(body.rotation + (-steeringInput1 * rotationStr));
        trans.Rotate(0f,0f,-steeringInput * rotationStr,Space.Self);


        //added hitbox off/on
        //if (ballScript.isRedPlayer1 == isPlayer1)
        //{
        //    hitBox.enabled = false;
        //}
        //else
        //{
        //    hitBox.enabled = true;
        //}
    }

    void AntiCollision()
    {
        if (isDangerDodgeBall)
        {
            if (ballScript.isRedPlayer1 == isPlayer1)
            {
                Physics2D.IgnoreCollision(ballScript.ballHitBox, hitBox, true);
            }
            else if (ballScript.isRedPlayer1 != isPlayer1)
            {
                Physics2D.IgnoreCollision(ballScript.ballHitBox, hitBox, false);
            }
        }
    }
    public Vector2 FindUp()
    {
        float x = trans.up.x;
        float y =trans.up.y;
        upDirection = new Vector2(x, y);
        return upDirection;
    }
    void FindInputs()
    {
        steeringInput = Input.GetAxis(horizontalInput);
        moveInput = Input.GetAxis(verticalInput);
        buttonInput = Input.GetButtonDown(fireInput);
        //if (isPlayer1)
        //{
        //    steeringInput = Input.GetAxis("Horizontal");
        //    moveInput = Input.GetAxis("Vertical");
        //    buttonInput = Input.GetButtonDown("Fire1");
        //}
        //else
        //{
        //    steeringInput = Input.GetAxis("Horizontal2");
        //    moveInput = Input.GetAxis("Vertical2");
        //    buttonInput = Input.GetButtonDown("Fire2");
        //}
    }

    //void AttackBoxReady()
    //{// this will need to be changed once an animation is created
    //    if(attackTime > 0)//when active
    //    {
    //        attackBoxRend.color = Color.yellow;
    //    }
    //    else if(attackCoolOff == 0 & attackTime ==0)//when off cool down
    //    {
    //        attackBoxRend.color = Color.green;
    //    }
    //    else
    //    {
    //        attackBoxRend.color = Color.grey;//when on cool down
    //    }
    //}
    void AnimatorMethod()
    {
        animator.SetFloat("animeinputvertical", moveInput);
    }
}