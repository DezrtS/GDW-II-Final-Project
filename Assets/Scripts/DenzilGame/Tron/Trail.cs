using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Trail : MonoBehaviour
{
    [SerializeField] GameObject trail;
    [SerializeField] float trailUpdateFrequency = 1;

    SpriteShapeController spriteShapeController;

    int trailIndex = 2;
    int updateCount = 0;

    [SerializeField] int pointsTillExtend = 10;
    [SerializeField] int trailLength = 10;

    bool resetPosition = true;
    Vector3 position;

    void Start()
    {
        spriteShapeController = trail.GetComponent<SpriteShapeController>();
        StartCoroutine(UpdateTrail());
    }

    public void Restart(GameObject trail)
    {
        trailIndex = 2;
        updateCount = 0;
        spriteShapeController = trail.GetComponent<SpriteShapeController>();
        StartCoroutine(UpdateTrail());
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
            placedTrail = Instantiate(TrailGameController.instance.playerOneTrailPrefab);
        } else
        {
            placedTrail = Instantiate(TrailGameController.instance.playerTwoTrailPrefab);
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

    IEnumerator UpdateTrail()
    {
        //Debug.Log("Trail Length: " + spriteShapeController.spline.GetPointCount() + " at time: " + Time.timeSinceLevelLoad);
        yield return new WaitForSeconds(trailUpdateFrequency / 2);
        if (resetPosition)
        {
            position = transform.position - trail.transform.position - transform.up * 0.15f;
            resetPosition = false;
        }
        yield return new WaitForSeconds(trailUpdateFrequency / 2);
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
        StartCoroutine(UpdateTrail());
        
    }
}
