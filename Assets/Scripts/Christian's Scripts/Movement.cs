using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
     public Rigidbody2D  body;

    [SerializeField] Hearts heartScript;
    public Transform trans;
    // [SerializeField] Animator animator;
    public Vector2 faceDirection;
    Animator animator;


    int p1Health;
    
    Vector2 move;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        animator = GetComponent<Animator>();
        animator.SetBool("isredplayer1", true);
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

    public void Start()
    {
        heartScript = gameObject.GetComponent<Hearts>();
    }

    // Update is called once per frame
    public void Update()
    {
        
        if(Input.GetKey(KeyCode.A))
        {
            animator.SetFloat("animeinputvertical", 1);
            move.x = -1;
            FaceForward();
        }

        else if(Input.GetKey(KeyCode.D))
        {
            animator.SetFloat("animeinputvertical", 1);
            move.x = 1;
            FaceForward();
        }

        else
        {
            move.x = 0;
            
        }
        // move.x = Input.GetAxisRaw("Horizontal");
        // move.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetFloat("animeinputvertical", 1);
            move.y = 1;
            FaceForward();
        }

        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetFloat("animeinputvertical", 1);
            move.y = -1;
            FaceForward();
        }

        else
        {
            
            move.y = 0;
        }

        if(move.x == 0 && move.y == 0)
        {
            animator.SetFloat("animeinputvertical", 0);
        }
    }

     public void FixedUpdate()
    {
        
        body.MovePosition(body.position + move * playerSpeed * Time.fixedDeltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.playHitSound();
           // SoundManager.Instance.playHitSound();
            heartScript.subtractHealth();
            ShakeBehaviour.Instance.TriggerShake();
            //animator.Play("");

            //Hearts.Instance.subtractHealth();


        }
    }


    public int ReturnP1Health()
    {
        return( heartScript.returnHealth());
    }

    void FaceForward()
    {
        Vector2 movedirection = move;
        faceDirection = movedirection;
       // if(move.x == 0 && move.y == 0)
      //  { 

           
        Quaternion rotationdirection = Quaternion.LookRotation(Vector3.forward, faceDirection);
        trans.rotation = Quaternion.RotateTowards(trans.rotation, rotationdirection, 1);
    }

}
