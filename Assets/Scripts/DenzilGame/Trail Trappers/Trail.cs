using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Trail : MonoBehaviour
{
    private SpriteShapeController spriteShapeController;

    [SerializeField] private GameObject trail;

    [SerializeField] private float trailUpdateFrequency = 1;
    [SerializeField] private int pointsTillExtend = 10;
    [SerializeField] private int trailLength = 10;

    private int trailIndex = 2;
    private int updateCount = 0;

    private bool resetPosition = true;
    private Vector3 position;

    private GameTimer checkPositionTimer;
    private GameTimer trailTimer;

    private void Awake()
    {
        spriteShapeController = trail.GetComponent<SpriteShapeController>();

        checkPositionTimer = new GameTimer(trailUpdateFrequency / 2f, false);
        trailTimer = new GameTimer(trailUpdateFrequency / 2f, true);

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
            checkPositionTimer.PauseTimer(false);
            if (checkPositionTimer.GetTimerAlreadyFinished())
            {
                trailTimer.PauseTimer(false);
            }
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            checkPositionTimer.PauseTimer(true);
            trailTimer.PauseTimer(true);
        }
    }

    private void Update()
    {
        if (checkPositionTimer.UpdateTimer())
        {
            if (!checkPositionTimer.GetTimerAlreadyFinished())
            {
                if (resetPosition)
                {
                    position = transform.position - trail.transform.position - transform.up * 0.15f;
                    resetPosition = false;
                }
                trailTimer.PauseTimer(false);
            }

            if (trailTimer.UpdateTimer())
            {
                spriteShapeController.spline.SetPosition(spriteShapeController.spline.GetPointCount() - 1, position);
                if ((transform.position - trail.transform.position - transform.up * 0.15f - position).magnitude > 0.15f)
                {
                    resetPosition = true;
                    spriteShapeController.spline.InsertPointAt(trailIndex, transform.position - trail.transform.position - transform.up * 0.15f);
                    spriteShapeController.spline.SetTangentMode(trailIndex, ShapeTangentMode.Continuous);
                    if (trailIndex >= trailLength)
                    {
                        spriteShapeController.spline.RemovePointAt(0);
                        updateCount++;
                        if (updateCount >= pointsTillExtend)
                        {
                            trailLength++;
                            updateCount = 0;
                        }
                    }
                    else
                    {
                        trailIndex++;
                    }
                }

                checkPositionTimer.RestartTimer();
                trailTimer.RestartTimer();
                trailTimer.PauseTimer(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (spriteShapeController != null)
        {
            spriteShapeController.spline.SetPosition(spriteShapeController.spline.GetPointCount() - 1, transform.position - trail.transform.position - transform.up * 0.25f);
        }
    }

    public void SetTrailLength(int amount)
    {
        trailLength = amount;
    }

    public void ShrinkTailNow(int amount)
    {
        while (trailIndex > amount)
        {
            spriteShapeController.spline.RemovePointAt(0);
            trailIndex--;
        }
        trailLength = amount;
    }

    public void PlaceTrail(bool isPlayerOne)
    {
        GameObject placedTrail;
        if (isPlayerOne)
        {
            placedTrail = Instantiate(TrailGameController.Instance.playerOneTrailPrefab);
        } else
        {
            placedTrail = Instantiate(TrailGameController.Instance.playerTwoTrailPrefab);
        }
        SpriteShapeController spriteShape = placedTrail.GetComponent<SpriteShapeController>();
        int a = spriteShape.spline.GetPointCount() - 1;
        for (int i = spriteShapeController.spline.GetPointCount() - 1; i >= 0; i--)
        {
            if (a >= 0)
            {
                spriteShape.spline.SetPosition(a, spriteShapeController.spline.GetPosition(i));
                spriteShape.spline.SetTangentMode(a, ShapeTangentMode.Continuous);
                a--;
            } else
            {
                spriteShape.spline.InsertPointAt(0, spriteShapeController.spline.GetPosition(i));
                spriteShape.spline.SetTangentMode(0, ShapeTangentMode.Continuous);
            }
        }
    }
}