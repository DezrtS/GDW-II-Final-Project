using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    [SerializeField] private float minBouncePower;
    [SerializeField] private float maxBouncePower;
    [SerializeField] private float powerIncrease;

    private float bouncePower;

    public Rigidbody2D body;


    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        bouncePower = minBouncePower;
        body.sharedMaterial.bounciness = bouncePower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IncreaseBouncePower();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        IncreaseBouncePower();
    }

    void IncreaseBouncePower()
    {
        bouncePower = bouncePower + powerIncrease;
        body.sharedMaterial.bounciness = bouncePower;
    }
}
