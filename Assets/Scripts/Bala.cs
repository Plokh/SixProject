using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bala : MonoBehaviour
{
    [SerializeField]
    private float velocidade; //velocidade da bala
    private Vector2 direcao; //definir a direção em que a bala irá
    private Rigidbody2D rb;//inicializando um rigidbody por script para fazer com que a bala percorra um caminho
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = direcao * velocidade;//Baseado na direcao em que o persongaem está ele multiplica com a velcidade para que a bala ande
    }

    public void Inicializar(Vector2 _direcao) //define a direção em que a bala irá
    {
        direcao = _direcao;
    }
    private void OnBecameInvisible()//Destrói a bala caso a mesma saia do cenário
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D outro) //Destói a bala caso colida com algo que não seja um inimigo
    {
        if (outro.gameObject.tag != "Inimigo")
        {
            Destroy(gameObject);
        }
    }
}


