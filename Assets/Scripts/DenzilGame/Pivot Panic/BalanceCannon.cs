using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceCannon : MonoBehaviour
{
    [SerializeField] GameObject barrel;
    [SerializeField] ParticleSystem cannonFire;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(3 + Random.Range(-100, 100) / 100f);
        GameObject newBarrel = Instantiate(barrel, transform.position, Quaternion.identity);
        newBarrel.GetComponent<Rigidbody2D>().AddForce(transform.right / (8 + Random.Range(0, 8)), ForceMode2D.Impulse);
        cannonFire.Play();
        StartCoroutine(Shoot());
    }


}
