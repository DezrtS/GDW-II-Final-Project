using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    [SerializeField] private Color transitionColor = Color.black;
    [SerializeField] private Material transitionMaterial;
    [Space(10)]
    [Header("Enter Transition Components")]
    [SerializeField] private GameObject growCube;
    [SerializeField] private GameObject growCircle;
    [SerializeField] private GameObject slideCube;
    [SerializeField] private GameObject cubeCrossCover;
    [SerializeField] private GameObject growCoverCube;
    [SerializeField] private GameObject shutDoor;
    [Space(10)]
    [Header("Exit Transition Components")]
    [SerializeField] private GameObject shrinkCube;
    [SerializeField] private GameObject shrinkCircle;
    [SerializeField] private GameObject unslideCube;
    [SerializeField] private GameObject cubeUncrossCover;
    [SerializeField] private GameObject shrinkCoverCube;
    [SerializeField] private GameObject openDoor;
    [Space(10)]
    [SerializeField] private GameObject plainCube;
    [SerializeField] private GameObject plainLongCube;
    [Space(10)]
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool freezeOnStart = true;
    [SerializeField] private bool initiateGameUIAnimation = true;
    [SerializeField] private int currentAnimation = 0;

    private void Awake()
    {
        Instance = this;
        transitionMaterial.color = transitionColor;
    }

    private void Start()
    {
        if (freezeOnStart)
        {
            GameStateManager.Instance.SetState(GameState.Paused);
        }

        if (playOnStart)
        {
            PlayExitTransition(currentAnimation);
        }
    }

    public delegate void TransitionEndHandler(bool isExitTransition);
    public event TransitionEndHandler OnTransitionEnded;

    public void SetTransitionColor(Color transitionColor)
    {
        this.transitionColor = transitionColor;
    }

    private void OnPlayEnterTransitionEnd()
    {
        //foreach (Transform child in transform)
        //{
        //    Destroy(child.gameObject);
        //}
        OnTransitionEnded?.Invoke(false);
        //Debug.Log("Enter Transtion Ended");
    }

    private void OnPlayExitTransitionEnd()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        if (initiateGameUIAnimation)
        {
            CountdownManager.Instance.SpawnAndStartCountdown();
            GameUIManager.Instance.ShowUI();
        }
        OnTransitionEnded?.Invoke(true);
        //Debug.Log("Exit Transtion Ended");
    }

    public bool CheckEvenOrOdd(bool checkIfEven, int num)
    {
        if (checkIfEven)
        {
            return (Mathf.Abs(num % 2) == 0);
        } else
        {
            return (Mathf.Abs(num % 2) == 1);
        }
    }

    public float GetRandomRotation(bool lockToFourDirections)
    {
        float direction;
        if (lockToFourDirections)
        {
            int directionNum = Random.Range(0, 4);
            switch (directionNum)
            {
                case 0:
                    direction = 0;
                    break;
                case 1:
                    direction = 90;
                    break;
                case 2:
                    direction = 180;
                    break;
                case 3:
                    direction = 270;
                    break;
                default:
                    direction = 0;
                    break;
            }
        } else
        {
            direction = Random.Range(0, 360);
        }
        return direction;
    }

    public void PlayRandomEnterTransition()
    {
        SoundManager.Instance.FadeGameMusic();
        PlayEnterTransition(Random.Range(0, 7));
    }

    public void PlayEnterTransition(int transition)
    {
        StartCoroutine(SoundManager.Instance.fadeTitleMusicOut());
        switch (transition)
        {
            case 0:
                // Multiple Small Squares Grow From Center Of Screen
                //
                StartCoroutine(GrowCubesFromCenter(0, 0, true));
                break;
            case 1:
                // Circle Grows From Center Of Screen
                Instantiate(growCircle, transform.position, Quaternion.identity, transform);
                StartCoroutine(OnTransitionEndTimer(2, true));
                break;
            case 2:
                // Square Slides Across Screen From Any Angle
                Instantiate(slideCube, transform.position, Quaternion.Euler(0, 0, GetRandomRotation(false)), transform);
                StartCoroutine(OnTransitionEndTimer(2, true));
                break;
            case 3:
                // Squares Cross Over Screen At Any Angle
                Instantiate(cubeCrossCover, transform.position, Quaternion.Euler(0, 0, GetRandomRotation(false)), transform);
                StartCoroutine(OnTransitionEndTimer(2, true));
                break;
            case 4:
                // Randomly Placed Small Squares Grow 
                //
                StartCoroutine(PlaceCubesRandomly(new List<Vector2>(), true));
                break;
            case 5:
                // Multiple Stacked Squares Grow Horizontally Across Screen
                //
                StartCoroutine(GrowCoverCubes(new List<Vector2>(), true));
                break;
            case 6:
                // Multiple Stacked Squares At Any Angle Close Off At Center Of Screen
                //
                StartCoroutine(ShutDoor(new List<Vector2>(), Vector3.zero, true));
                break;
            default:
                // Plays Any Transition
                PlayRandomEnterTransition();
                break;
        }
    }

    public void PlayRandomExitTransition()
    {
        PlayExitTransition(Random.Range(0, 7));
    }

    public void PlayExitTransition(int transition)
    {
        switch (transition)
        {
            case 0:
                StartCoroutine(ShrinkCubesFromCenter(10, 10, true, true, null));
                break;
            case 1:
                Instantiate(shrinkCircle, transform.position, Quaternion.identity, transform);
                StartCoroutine(OnTransitionEndTimer(2, false));
                break;
            case 2:
                Instantiate(unslideCube, transform.position, Quaternion.Euler(0, 0, GetRandomRotation(false)), transform);
                StartCoroutine(OnTransitionEndTimer(2, false));
                break;
            case 3:
                Instantiate(cubeUncrossCover, transform.position, Quaternion.Euler(0, 0, GetRandomRotation(false)), transform);
                StartCoroutine(OnTransitionEndTimer(2, false));
                break;
            case 4:
                StartCoroutine(RemoveCubesRandomly(new List<Vector2>(), true, null));
                break;
            case 5:
                StartCoroutine(ShrinkCoverCubes(new List<Vector2>(), true, null));
                break;
            case 6:
                StartCoroutine(OpenDoor(new List<Vector2>(), Vector3.zero, true, null));
                break;
            default:
                PlayRandomExitTransition();
                break;
        }
    }

    private IEnumerator OnTransitionEndTimer(float time, bool isEnterTransition)
    {
        yield return new WaitForSeconds(time);
        if (isEnterTransition)
        {
            OnPlayEnterTransitionEnd();
        } else
        {
            OnPlayExitTransitionEnd();
        }
    }

    private IEnumerator GrowCubesFromCenter(int x, int y, bool onEven)
    {
        yield return new WaitForSeconds(0.15f);
        for (int i_x = -x; i_x <= x; i_x++)
        {
            if (CheckEvenOrOdd(onEven, i_x))
            {
                Instantiate(growCube, new Vector3(i_x * 2, y * 2, 0) + transform.position, Quaternion.identity, transform);
                Instantiate(growCube, new Vector3(i_x * 2, -y * 2, 0) + transform.position, Quaternion.identity, transform);
            }
        }
        for (int i_y = -y + 1; i_y < y; i_y++)
        {
            if (CheckEvenOrOdd(onEven, i_y))
            {
                Instantiate(growCube, new Vector3(x * 2, i_y * 2, 0) + transform.position, Quaternion.identity, transform);
                Instantiate(growCube, new Vector3(-x * 2, i_y * 2, 0) + transform.position, Quaternion.identity, transform);
            }
        }
        yield return new WaitForSeconds(0.15f);
        for (int i_x = -x; i_x <= x; i_x++)
        {
            if (CheckEvenOrOdd(!onEven, i_x)) 
            {
                Instantiate(growCube, new Vector3(i_x * 2, y * 2, 0) + transform.position, Quaternion.identity, transform);
                Instantiate(growCube, new Vector3(i_x * 2, -y * 2, 0) + transform.position, Quaternion.identity, transform);
            }
        }
        for (int i_y = -y + 1; i_y < y; i_y++)
        {
            if (CheckEvenOrOdd(!onEven, i_y))
            {
                Instantiate(growCube, new Vector3(x * 2, i_y * 2, 0) + transform.position, Quaternion.identity, transform);
                Instantiate(growCube, new Vector3(-x * 2, i_y * 2, 0) + transform.position, Quaternion.identity, transform);
            }
        }
        x++;
        y++;
        if (x <= 10)
        {
            StartCoroutine(GrowCubesFromCenter(x, y, !onEven));
        } else
        {
            StartCoroutine(OnTransitionEndTimer(0.95f, true));
        }
    }

    private IEnumerator PlaceCubesRandomly(List<Vector2> availablePositions, bool generatePositions)
    {
        List<Vector2> positions = availablePositions;
        if (generatePositions)
        {
            for (int x = -9; x <= 9; x++)
            {
                for (int y = -5; y <= 5; y++)
                {
                    positions.Add(new Vector2(x, y));
                }
            }
        }
        yield return new WaitForSeconds(0.005f);
        int index = Random.Range(0, positions.Count);
        Instantiate(growCube, positions[index] * 2 + (Vector2)transform.position, Quaternion.identity, transform);
        positions.RemoveAt(index);
        if (positions.Count > 0)
        {
            StartCoroutine(PlaceCubesRandomly(positions, false));
        } else
        {
            StartCoroutine(OnTransitionEndTimer(1.35f, true));
        }
    }

    private IEnumerator GrowCoverCubes(List<Vector2> availablePositions, bool generatePositions)
    {
        List<Vector2> positions = availablePositions;
        if (generatePositions)
        {
            for (int y = -5; y <= 5; y++)
            {
                positions.Add(new Vector2(0, y));
            }
        }
        yield return new WaitForSeconds(0.075f);
        int index = Random.Range(0, positions.Count);
        Instantiate(growCoverCube, positions[index] * 2 + (Vector2)transform.position, Quaternion.identity, transform);
        positions.RemoveAt(index);
        if (positions.Count > 0)
        {
            StartCoroutine(GrowCoverCubes(positions, false));
        }
        else
        {
            StartCoroutine(OnTransitionEndTimer(2f, true));
        }
    }

    private IEnumerator ShutDoor(List<Vector2> availablePositions, Vector3 startingRotation, bool generatePositionsAndRotation)
    {
        List<Vector2> positions = availablePositions;
        Vector3 rotation = startingRotation;
        if (generatePositionsAndRotation)
        {
            for (int y = -10; y <= 10; y++)
            {
                positions.Add(new Vector2(0, y));
            }
            rotation = new Vector3(0, 0, GetRandomRotation(false));
        }
        yield return new WaitForSeconds(0.1f);
        int index = Random.Range(0, positions.Count);
        Instantiate(shutDoor, Quaternion.Euler(rotation) * positions[index] * 2 + transform.position, Quaternion.Euler(rotation), transform);
        positions.RemoveAt(index);
        if (positions.Count > 0)
        {
            StartCoroutine(ShutDoor(positions, rotation, false));
        }
        else
        {
            StartCoroutine(OnTransitionEndTimer(1.15f, true));
        }
    }

    private IEnumerator ShrinkCubesFromCenter(int x, int y, bool onEven, bool GenerateCubes, GameObject[][] cubes)
    {
        if (GenerateCubes)
        {
            cubes = new GameObject[21][];
            for (int i_x = 0; i_x < 21; i_x++)
            {
                cubes[i_x] = new GameObject[21];
                for (int i_y = 0; i_y < 21; i_y++)
                {
                    cubes[i_x][i_y] = Instantiate(plainCube, new Vector3((i_x - 10) * 2, (i_y - 10) * 2, 0) + transform.position, Quaternion.identity, transform);
                }
            }
        }
        yield return new WaitForSeconds(0.15f);
        for (int i_x = -x; i_x <= x; i_x++)
        {
            if (CheckEvenOrOdd(onEven, i_x))
            {
                Destroy(cubes[i_x + 10][y + 10]);
                Destroy(cubes[i_x + 10][-y + 10]);
                Instantiate(shrinkCube, new Vector3(i_x * 2, y * 2, 0) + transform.position, Quaternion.identity, transform);
                Instantiate(shrinkCube, new Vector3(i_x * 2, -y * 2, 0) + transform.position, Quaternion.identity, transform);
            }
        }
        for (int i_y = -y + 1; i_y < y; i_y++)
        {
            if (CheckEvenOrOdd(onEven, i_y))
            {
                Destroy(cubes[x + 10][i_y + 10]);
                Destroy(cubes[-x + 10][i_y + 10]);
                Instantiate(shrinkCube, new Vector3(x * 2, i_y * 2, 0) + transform.position, Quaternion.identity, transform);
                Instantiate(shrinkCube, new Vector3(-x * 2, i_y * 2, 0) + transform.position, Quaternion.identity, transform);
            }
        }
        yield return new WaitForSeconds(0.15f);
        for (int i_x = -x; i_x <= x; i_x++)
        {
            if (CheckEvenOrOdd(!onEven, i_x))
            {
                Destroy(cubes[i_x + 10][y + 10]);
                Destroy(cubes[i_x + 10][-y + 10]);
                Instantiate(shrinkCube, new Vector3(i_x * 2, y * 2, 0) + transform.position, Quaternion.identity, transform);
                Instantiate(shrinkCube, new Vector3(i_x * 2, -y * 2, 0) + transform.position, Quaternion.identity, transform);
            }
        }
        for (int i_y = -y + 1; i_y < y; i_y++)
        {
            if (CheckEvenOrOdd(!onEven, i_y))
            {
                Destroy(cubes[x + 10][i_y + 10]);
                Destroy(cubes[-x + 10][i_y + 10]);
                Instantiate(shrinkCube, new Vector3(x * 2, i_y * 2, 0) + transform.position, Quaternion.identity, transform);
                Instantiate(shrinkCube, new Vector3(-x * 2, i_y * 2, 0) + transform.position, Quaternion.identity, transform);
            }
        }
        x--;
        y--;
        if (x >= 0)
        {
            StartCoroutine(ShrinkCubesFromCenter(x, y, !onEven, false, cubes));
        }
        else
        {
            StartCoroutine(OnTransitionEndTimer(0.95f, false));
        }
    }

    private IEnumerator RemoveCubesRandomly(List<Vector2> availablePositions, bool generatePositions, List<GameObject> cubes)
    {
        List<Vector2> positions = availablePositions;
        if (generatePositions)
        {
            cubes = new List<GameObject>();
            for (int x = -9; x <= 9; x++)
            {
                for (int y = -5; y <= 5; y++)
                {
                    positions.Add(new Vector2(x, y));
                    cubes.Add(Instantiate(plainCube, new Vector2(x, y) * 2 + (Vector2)transform.position, Quaternion.identity, transform));
                }
            }
        }
        yield return new WaitForSeconds(0.005f);
        int index = Random.Range(0, positions.Count);
        Instantiate(shrinkCube, positions[index] * 2 + (Vector2)transform.position, Quaternion.identity, transform);
        positions.RemoveAt(index);
        GameObject removeCube = cubes[index];
        cubes.RemoveAt(index);
        Destroy(removeCube);
        if (positions.Count > 0)
        {
            StartCoroutine(RemoveCubesRandomly(positions, false, cubes));
        }
        else
        {
            StartCoroutine(OnTransitionEndTimer(1.35f, false));
        }
    }

    private IEnumerator ShrinkCoverCubes(List<Vector2> availablePositions, bool generatePositions, List<GameObject> longCubes)
    {
        List<Vector2> positions = availablePositions;
        if (generatePositions)
        {
            longCubes = new List<GameObject>();
            for (int y = -5; y <= 5; y++)
            {
                positions.Add(new Vector2(0, y));
                longCubes.Add(Instantiate(plainLongCube, new Vector2(0, y) * 2 + (Vector2)transform.position, Quaternion.identity, transform));
            }
        }
        yield return new WaitForSeconds(0.075f);
        int index = Random.Range(0, positions.Count);
        Instantiate(shrinkCoverCube, positions[index] * 2 + (Vector2)transform.position, Quaternion.identity, transform);
        positions.RemoveAt(index);
        GameObject removeLongCube = longCubes[index];
        longCubes.RemoveAt(index);
        Destroy(removeLongCube);
        if (positions.Count > 0)
        {
            StartCoroutine(ShrinkCoverCubes(positions, false, longCubes));
        }
        else
        {
            StartCoroutine(OnTransitionEndTimer(2f, false));
        }
    }

    private IEnumerator OpenDoor(List<Vector2> availablePositions, Vector3 startingRotation, bool generatePositionsAndRotation, List<GameObject> longCubes)
    {
        List<Vector2> positions = availablePositions;
        Vector3 rotation = startingRotation;
        if (generatePositionsAndRotation)
        {
            longCubes = new List<GameObject>();
            rotation = new Vector3(0, 0, GetRandomRotation(false));
            for (int y = -10; y <= 10; y++)
            {
                positions.Add(new Vector2(0, y));
                longCubes.Add(Instantiate(plainLongCube, Quaternion.Euler(rotation) * new Vector2(0, y) * 2 + transform.position, Quaternion.Euler(rotation), transform));
            }
        }
        yield return new WaitForSeconds(0.1f);
        int index = Random.Range(0, positions.Count);
        Instantiate(openDoor, Quaternion.Euler(rotation) * positions[index] * 2 + transform.position, Quaternion.Euler(rotation), transform);
        positions.RemoveAt(index);
        GameObject removeLongCube = longCubes[index];
        longCubes.RemoveAt(index);
        Destroy(removeLongCube);
        if (positions.Count > 0)
        {
            StartCoroutine(OpenDoor(positions, rotation, false, longCubes));
        }
        else
        {
            StartCoroutine(OnTransitionEndTimer(1.15f, false));
        }
    }
}
