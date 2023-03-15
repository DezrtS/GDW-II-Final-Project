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


    void Start()
    {
        spriteShapeController = trail.GetComponent<SpriteShapeController>();
        StartCoroutine(UpdateTrail());
    }

    private void FixedUpdate()
    {
        spriteShapeController.spline.SetPosition(spriteShapeController.spline.GetPointCount() - 1, transform.position - trail.transform.position - transform.up * 0.25f);
    }

    IEnumerator UpdateTrail()
    {
        yield return new WaitForSeconds(trailUpdateFrequency);
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
        } else
        {
            trailIndex++;
        }
        StartCoroutine(UpdateTrail());
    }
}
