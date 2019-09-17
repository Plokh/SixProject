using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField]
    private float velocidade = 0;
    float horizontal;
    float vertical;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        MovimentarY(vertical);

        horizontal = Input.GetAxis("Horizontal");
        MovimentarX(horizontal);

    }

        private void MovimentarY(float v)
    {
        rb2D.velocity = new Vector2(v*velocidade, rb2D.velocity.x);
    }
        private void MovimentarX(float h)
    {
        rb2D.velocity = new Vector2(h*velocidade, rb2D.velocity.x);
    }
}
