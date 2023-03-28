using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [SerializeField] private GameObject pushObject;
    private Animator pushAnimator;

    [SerializeField] private float pushDelay = 1.2f;
    private float pushDuration = 0.3f;

    [SerializeField] private int pushPower = 5;

    private bool isPushing;
    private bool canPush = true;

    private void Start()
    {
        pushAnimator = pushObject.GetComponent<Animator>();
    }

    public void ResetPushPower()
    {
        pushPower = 5;
    }

    public void Push()
    {
        if (canPush && Time.timeScale == 1)
        {
            //isPushing = true;
            canPush = false;
            //pushAnimator.SetBool("IsPushing", isPushing);
            pushAnimator.SetBool("IsPushing", true);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(pushObject.transform.position, 1);
            foreach (Collider2D collider in colliders)
            {
                if ((collider.tag == "Player1" || collider.tag == "Player2") && collider.gameObject != gameObject)
                {
                    collider.gameObject.GetComponent<PushMovement>().KnockBack(transform.up, pushPower);
                    pushPower = pushPower + 3;
                }
            }
            StartCoroutine(EndPush());
        }
    }

    IEnumerator EndPush()
    {
        yield return new WaitForSeconds(pushDuration);
        pushAnimator.SetBool("IsPushing", false);
        StartCoroutine(PushCooldown());
    }


    IEnumerator PushCooldown()
    {
        yield return new WaitForSeconds(pushDelay);
        canPush = true;
        //yield return new WaitForSeconds(pushDelay / 2f);
        //isPushing = false;
        //Physics2D.OverlapCircleAll(pushObject.transform.position, 1);
        //yield return new WaitForSeconds(pushDelay / 2f);
        //canPush = true;
        //pushAnimator.SetBool("IsPushing", isPushing);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            collision.GetComponent<PushMovement>().KnockBack(transform.up, pushPower);
            pushPower++;
        }
    }
}
