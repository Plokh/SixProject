using UnityEngine;
using UnityEditor;

public class Arma
{
    public GameObject armaPrefab;
    public GameObject projetilPrefab;
    public Transform projetilSaida;
    public int dano = 12;
    public float projetilVelocidade = 1f;

    public Arma(GameObject arma)
    {
        _animator = arma.GetComponent<Animator>();
        _transform = arma.GetComponent<Transform>();
        _spriteRenderer = arma.GetComponent<SpriteRenderer>();
    }

    void Atirar()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            /*GameObject projetil = (GameObject)Instantiate(projetilPrefab, projetilSaida.position, projetilSaida.rotation); //Estancia objeto
            projetil.GetComponent<Rigidbody2D>().velocity = direcao.normalized * projetilVelocidade; //joga o objeto*/
        }
    }

}