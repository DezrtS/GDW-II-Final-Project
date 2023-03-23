using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject debugCircle;
    public bool showAttack;

    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private KeyCode attackButton;

    [SerializeField] Animator animator;
    [SerializeField] Animator attackAnimator;

    private bool isAttacking = false;
    private float attackDirection = 1;

    private void Update()
    {
        if (Input.GetKeyDown(attackButton) && !isAttacking && Time.timeScale == 1)
        {
            isAttacking = true;
            animator.SetBool("IsAttacking", isAttacking);
            GameObject otherPlayer = PivotGameController.instance.GetClosestPlayer(gameObject);
            attackDirection = Mathf.Sign((otherPlayer.transform.position - transform.position).x);
            GetComponent<MovementV2>().keepRotation = true;
            if (attackDirection > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            } else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            StartCoroutine(InitiateAttack(playerWeapon.timeTillAttack));
        }
    }

    IEnumerator InitiateAttack(float timeTillAttack)
    {
        yield return new WaitForSeconds(timeTillAttack / 2f);
        attackAnimator.SetBool("IsAttacking", isAttacking);
        yield return new WaitForSeconds(timeTillAttack / 2f);
        Attack();
        isAttacking = false;
        animator.SetBool("IsAttacking", isAttacking);
        attackAnimator.SetBool("IsAttacking", isAttacking);
        GetComponent<MovementV2>().keepRotation = false;
    }

    public void Attack()
    {
        if (showAttack) {
            Instantiate(debugCircle, transform.position + Vector3.right * attackDirection * playerWeapon.attackRange, Quaternion.identity).transform.localScale = Vector3.one * playerWeapon.attackRadius;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + Vector3.right * attackDirection * playerWeapon.attackRange, playerWeapon.attackRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.gameObject.tag == "Player")
            {
                collider.gameObject.GetComponent<MovementV2>().ApplyKnockback(new Vector3(playerWeapon.knockbackAmount * attackDirection, 0, 0));
            }
        }
    }
}
