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


    void Start()
    {
        spriteShapeController = trail.GetComponent<SpriteShapeController>();
        StartCoroutine(UpdateTrail());
    }

    IEnumerator UpdateTrail()
    {
        yield return new WaitForSeconds(trailUpdateFrequency);
        spriteShapeController.spline.InsertPointAt(trailIndex, transform.position - trail.transform.position - transform.up * 0.15f);
        spriteShapeController.spline.SetTangentMode(trailIndex, ShapeTangentMode.Continuous);
        trailIndex++;
        StartCoroutine(UpdateTrail());
    }
}
