using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    Text textMag;
    //test above
    public playermovementballgame movementscript;
    public Hearts heartScript;
    //ball components
    public Collider2D ballHitBox;
    Rigidbody2D body;
    Transform trans;
    public SpriteRenderer sprite;
    ParticleSystem partsys;
    [SerializeField] Sprite player1spriteRed;
    [SerializeField] Sprite player2spriteBlue;
    //ball values
    public bool isRedPlayer1;
    public float mag;
    public float lastMag;
    public float targetMag;
    public float maxMag;
    public float inc;
    public Vector2 vecNorm;
    //start values
    public Vector3 start1 = new Vector3(-8, 0, 0);
    public Vector3 start2 = new Vector3(8, 0, 0);

    public bool respawnfullreset;
    // Start is called before the first frame update
    void Start()
    {
        
        body = GetComponent<Rigidbody2D>();
        ballHitBox = GetComponent<Collider2D>();
        trans = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        partsys = GetComponent<ParticleSystem>();
        
        mag = body.velocity.magnitude;
        
        int randomNum = Random.Range(1, 3);
        if (randomNum == 2) 
        {
            isRedPlayer1 = false;
            //sprite.color = Color.blue;
            //sprite.sprite = player2spriteBlue;
            trans.position = start2;
        } 
        else if( randomNum == 1)
        {
            isRedPlayer1 = true;
            //sprite.color = Color.red;
            //sprite.sprite = player1spriteRed;
            trans.position = start1;
        }
        Color();
    }

    // Update is called once per frame
    void Update()
    {
        GetMagnitude();
        Color();
    }

    void FixedUpdate()
    {
        BallSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movementscript = collision.gameObject.GetComponentInParent<playermovementballgame>();
        playertopdownattack topdownattack = collision.gameObject.GetComponentInParent<playertopdownattack>();
        topdownattack.canAttack = false; topdownattack.attackTime = Time.time; topdownattack.attackCoolOff = topdownattack.startTime + topdownattack.attackCoolOffMax;
        isRedPlayer1 = movementscript.isPlayer1;
        vecNorm = movementscript.FindUp();
        //if(targetMag == 0)
        //{
        //    targetMag = targetMag + inc*5;
        //}
        targetMag = targetMag + inc;
        body.velocity = vecNorm * targetMag;
        //partsys.emissionRate = 10 + 2*targetMag;
        //partsys.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            movementscript = collision.gameObject.GetComponent<playermovementballgame>();
            
            if (movementscript.isPlayer1 != isRedPlayer1)
            {
                Respawn(collision.gameObject);
                
            }
            
        }
    }

    public void Color()
    {
        if (isRedPlayer1)
        {
            sprite.sprite = player1spriteRed;
        }
        else if (isRedPlayer1 == false)
        {
            sprite.sprite = player2spriteBlue;
        }
    }

    void BallSpeed()
    {
        if (mag != targetMag)
        {
            mag = targetMag;
        }
        vecNorm = body.velocity.normalized;
        body.velocity = vecNorm*targetMag;
        textMeshProUGUI.text = mag.ToString();
    }

    void GetMagnitude()
    {
        mag = body.velocity.magnitude;
        //textMeshProUGUI.text = mag.ToString();
        //UnityEngine.Debug.Log(body.velocity.magnitude); 
        //UnityEngine.Debug.Log(mag);
    }

    public void Respawn(GameObject player)
    {


        if (respawnfullreset)
        {
            // | resets scene
            // V
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); Start();
        }
        else
        {
            //player reset
            player.transform.position = movementscript.startPos;
            player.transform.rotation = movementscript.startRot;
            player.SetActive(true);
            //ball reset
            targetMag = 0;
            body.velocity = new Vector2(0f, 0f);
            heartScript = player.GetComponent<Hearts>();
            movementscript = player.GetComponent<playermovementballgame>();
            if (movementscript.isPlayer1)
            {
                trans.position = start1;
                isRedPlayer1 = true;
            }
            else if (movementscript.isPlayer1 == false)//player 2 blue
            {
                trans.position = start2;
                isRedPlayer1 = false;
            }
            //isRedPlayer1 = movementscript.isPlayer1;
            heartScript.subtractHealth();

            if(heartScript.returnHealth() == 0)
            {
                UnityEngine.Debug.Log("PLayer loses");
            }
        }
    }
}