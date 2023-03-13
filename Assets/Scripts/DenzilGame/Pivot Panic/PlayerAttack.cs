using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject debugCircle;
    public bool showAttack;

    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private KeyCode attackButton;

    private bool isAttacking = false;
    private float attackDirection = 1;

    private void Update()
    {
        if (Input.GetKeyDown(attackButton) && !isAttacking)
        {
            isAttacking = true;
            GameObject otherPlayer = GameController.instance.GetClosestPlayer(gameObject);
            attackDirection = Mathf.Sign((otherPlayer.transform.position - transform.position).x);
            StartCoroutine(InitiateAttack(playerWeapon.timeTillAttack));
        }
    }

    IEnumerator InitiateAttack(float timeTillAttack)
    {
        yield return new WaitForSeconds(timeTillAttack);
        Attack();
        isAttacking = false;
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
