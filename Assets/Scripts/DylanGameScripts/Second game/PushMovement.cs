using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushMovement : MonoBehaviour
{
    private string horizontal1 = "Horizontal", horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2 = "Fire2";
    private string horizontalInput, verticalInput, fireInput;

    private Rigidbody2D rig;
    private PlayerPush playerPush;

    [SerializeField] private bool isPlayerOne;

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationStrength = 5f;

    private float horizontalMovement;
    private float verticalMovement;
    private bool actionButton;

    private bool knockedBack;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
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
            rig.simulated = true;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            rig.simulated = false;
        }
    }

    private void Start()
    {
        playerPush = GetComponent<PlayerPush>();
        SetupInputs();
    }

    void SetupInputs()
    {
        if (isPlayerOne)
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

    private void Update()
    {
        horizontalMovement = Input.GetAxis(horizontalInput);
        verticalMovement = Input.GetAxis(verticalInput);
        actionButton = Input.GetButtonDown(fireInput);

        if (actionButton)
        {
            SoundManager.Instance.playAttackSound();
            playerPush.Push();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        if (knockedBack)
        {
            if (rig.velocity.magnitude <= 2f)
            {
                knockedBack = false;
            }
        }
        Vector2 moveVelocity = transform.up * verticalMovement * movementSpeed;
        //rig.MovePosition(rig.position + moveVelocity * Time.fixedDeltaTime);
        if (!knockedBack)
        {
            rig.velocity = moveVelocity;
        }
        else
        {
            rig.AddForce(moveVelocity / 50f, ForceMode2D.Impulse);
        }

        transform.Rotate(0f, 0f, -horizontalMovement * rotationStrength, Space.Self);
    }

    public void KnockBack(Vector2 direction, float force)
    {
        Vector2 knockback = direction * force;
        rig.AddForce(knockback, ForceMode2D.Impulse);
        knockedBack = true;
    }
}
