using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _velocidade = 2f;
    public GameObject projetilPrefab;
    public Transform projetilSaida;
    public float projetilVelocidade = 1f;
    public Transform _armaEquipada;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Rigidbody2D _rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        Animator _animator = GetComponent<Animator>();
        Transform _transform = GetComponent<Transform>();

        Vector3 _posicaoRato = Camera.main.ScreenToWorldPoint(Input.mousePosition);      //Posição rato (tranformado para o espaço)

        //Moviemntando o personagem
        Vector3 _movimento = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        _transform.Translate(_velocidade * _movimento.normalized * Time.deltaTime);

        //Trocando a direção da imagem personagem (controlada pelo rato)
        if (_transform.position.x < _posicaoRato.x)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_transform.position.x > _posicaoRato.x)
        {
            _spriteRenderer.flipX = true;
        }

        //Transição das animações
        if (_movimento.x != 0 || _movimento.y != 0)
        {
            _animator.SetBool("andando", true);
        }
        else
        {
            _animator.SetBool("andando", false);
        }

        //Ataque do personagem

        Vector2 direcao = new Vector2(_posicaoRato.x - _armaEquipada.position.x, _posicaoRato.y - _armaEquipada.position.y); //pega direção

        _armaEquipada.transform.up = direcao.normalized; //Cajado seguir o mouse

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject projetil = (GameObject)Instantiate(projetilPrefab, projetilSaida.position, projetilSaida.localRotation);
            projetil.transform.localRotation = projetilSaida.rotation;

            //Estancia objeto
            projetil.GetComponent<Rigidbody2D>().velocity = direcao.normalized * projetilVelocidade; //joga o objeto
        }

        
    }  
}