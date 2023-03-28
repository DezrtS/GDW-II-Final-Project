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
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] Animator animator;
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
    bool playCountdown;
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

        //Testing Sound
        SoundManager.Instance.pauseTitleMusic();
        SoundManager.Instance.playGameMusicSound2();
        //Remove Here if needed
    }

    // Update is called once per frame
    void Update()
    {
        GetMagnitude();
        Color();
        PlayCountdownFunction();
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
        SoundManager.Instance.playBounceSound();
        UnityEngine.Debug.Log("attackhit");
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
        else
        {
            SoundManager.Instance.playHitSound();
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
        MagText();
    }

    void GetMagnitude()
    {
        mag = body.velocity.magnitude;
        //textMeshProUGUI.text = mag.ToString();
        //UnityEngine.Debug.Log(body.velocity.magnitude); 
        //UnityEngine.Debug.Log(mag);
    }

    void MagText()
    {
        //textMeshProUGUI.text = mag.ToString();
        textMeshPro.text = mag.ToString();
        if (isRedPlayer1)
        {
            textMeshPro.color = new Color32(245, 80, 80, 155);
        }
        else
        {
            textMeshPro.color = new Color32(101, 141, 171, 155);
        }
        if (mag > 9)
        {
            textMeshPro.rectTransform.position = new Vector3
            (1f, textMeshPro.rectTransform.position.y, textMeshPro.rectTransform.position.z);
        }
        else
        {
            textMeshPro.rectTransform.position = new Vector3
            (0f, textMeshPro.rectTransform.position.y, textMeshPro.rectTransform.position.z);
        }
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
            SoundManager.Instance.playBounceSound();
            UnityEngine.Debug.Log("hit");
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

            if(heartScript.returnHealth() == 0 && !GameEnder.instance.IsGameEnding())
            {
                GameEnder.instance.StartEndGame();
                UnityEngine.Debug.Log("PLayer loses");
                if (movementscript.isPlayer1)
                {
                    P2Score.Instance.AddScore();
                }
                else
                {
                    P1Score.Instance.AddScore();
                }

            }
            else
            {
                ShakeBehaviour.instance.TriggerShake();playCountdown = true;
            }
            
            //PlayCountdown();
            
            
        }
    }
    void PlayCountdownFunction()
    {

        if (ShakeBehaviour.instance.shakeDuration == 0 && playCountdown) 
        {
            playCountdown = false; 
            animator.Play("");
        }
    }
}