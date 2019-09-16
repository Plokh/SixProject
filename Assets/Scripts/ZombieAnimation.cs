using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Rigidbody2D rb2D;

    float horizontal;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Movimentar(horizontal);
    }

    private void Movimentar(float h)
    {
        rb2D.velocity = new Vector2(h, rb2D.velocity.y);
    }
}
