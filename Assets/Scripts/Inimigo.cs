using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo 	//Basicamente seguimos esses videos, porém modificando para nossas necessidades.
{										//https://www.youtube.com/channel/UCoxRNjIDKlzxxl8OOJub6CA
    bool entroNoRange = false;			//https://www.youtube.com/channel/UCC2k-SBDIRK1Jg1vJSYOBYg
    bool estaDirecaoPlayer = true;		//https://www.youtube.com/channel/UCok1vSaNxZZrxufASLHSqJg
    bool estaParado = false;			//https://unity3d.com/pt/learn/tutorials
    bool estaRecuando = false;

    int andandoKey = Animator.StringToHash("andando");

    public Vector2 _inimigoVelocidade = new Vector2(0.5f, 0.5f); //Velocidade em que o inimigo irá andar
    public float _inimigoRangeAtaque = 1;
    public float _inimigoRangeRecua = 2;
    public float tempoParado;
    public float tempoRecua;
    public float duracaoRecua = 3;
    public float duracaoParado = 3;
    public float playerDistancia;


    public Transform _playerTransform, _projetilRefTranform, _inimigoTranform;
    public GameObject _projetilPrefabObjeto;
    public Animator _inimigoAnimator; //Para utilizar a animação do inimigo andando
    public Rigidbody2D _inimigoRB2D;
    public SpriteRenderer _inimigoSpriteRenderer;

    public Inimigo(GameObject inimigo,Transform playerTranform, GameObject projetilPrefab)
    {
        _inimigoAnimator = inimigo.GetComponent<Animator>();//Inicializando o animator
        _inimigoTranform = inimigo.GetComponent<Transform>();
        _inimigoRB2D = inimigo.GetComponent<Rigidbody2D>();
        _inimigoSpriteRenderer = inimigo.GetComponent<SpriteRenderer>();
        _playerTransform = playerTranform;
        _projetilPrefabObjeto = projetilPrefab;
        _projetilRefTranform = inimigo.transform.GetChild(0);
    }

    public void onUpdate()
    {
        playerDistancia = Vector3.Distance(_inimigoTranform.position, _playerTransform.position);

        Animacao();
        Atirar();

        if (estaDirecaoPlayer) //se esta em direção ao player
        {
            if (playerDistancia <= _inimigoRangeAtaque) //se entrou no range de ataque
            {
                entroNoRange = true;
                estaDirecaoPlayer = false;
                estaParado = true;
            }
            else
            {
                //entroNoRange = false;
                estaDirecaoPlayer = true;
                estaParado = false;
            }
            MovimentoDirecaoPlayer();
        }
        else if (estaParado) //se esta na fase parado
        {
            if (tempoParado <= duracaoParado) //se o tempo de ficar parado ainda não foi alcançado
            {
                tempoParado += Time.deltaTime;
                estaDirecaoPlayer = false;
                estaParado = true;
            }
            else
            {
                estaDirecaoPlayer = false;
                estaParado = false;
                estaRecuando = true;
                tempoParado = 0;
            }
            Parado();
        }
        else if (estaRecuando) //se esta recuando em relção ao player
        {
            if(playerDistancia <= _inimigoRangeRecua || tempoRecua <= duracaoRecua) //se o tempo de de recuar ou a distancia maxima de recuo não foi atingido
            {
                tempoRecua += Time.deltaTime;
            }
            else
            {
                estaDirecaoPlayer = true;
                estaParado = false;
                estaRecuando = false;
                tempoRecua = 0;
            }
            MovimentoDirecaoRecuaPlayer();
        }
    }

    //métodos

    /*OnTriggerEnter2D: Aqui nós checamos se o inimigo chegou ao limite de sua patrulha, para isso colocamos um if que
      utilizando a tag definida no Unity para o Limite checa se a posição deve ser alterada. Caso o inimigo tenha
      tocado o limite utilizamos o método MudarDirecao para que que ele altere a sua rota
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Limite")
        {
            Animacao();
        }
    }*/

    private void MovimentoDirecaoPlayer()
    {

        _inimigoRB2D.MovePosition(_inimigoRB2D.position + _inimigoVelocidade * (_playerTransform.position - _inimigoTranform.position).normalized * Time.fixedDeltaTime);
    }

    private void MovimentoDirecaoRecuaPlayer()
    {
        _inimigoRB2D.MovePosition(_inimigoRB2D.position + _inimigoVelocidade * (_inimigoTranform.position - _playerTransform.position).normalized * Time.fixedDeltaTime);
    }

    private void Animacao()
    {
        //Transição das animações
        if (!estaParado)
        {
            _inimigoAnimator.SetTrigger(andandoKey);
        }
        else
        {
            _inimigoAnimator.ResetTrigger(andandoKey);
        }

        //Transição da direção da Sprinte
        if (_playerTransform.position.x < _inimigoTranform.position.x)
        {
            _inimigoSpriteRenderer.flipX = true;
        }
        else if (_playerTransform.position.x > _inimigoTranform.position.x)
        {
            _inimigoSpriteRenderer.flipX = false;
        }
    }

    void Parado()
    {
        _inimigoRB2D.MovePosition(_inimigoRB2D.position);
    }

    void Atirar()
    {
        if (entroNoRange == true)
        {
            GameObject projetil = GameObject.Instantiate(_projetilPrefabObjeto, _projetilRefTranform.position, _projetilRefTranform.rotation);
            projetil.GetComponent<Rigidbody2D>().velocity = (_playerTransform.position - _inimigoTranform.position) * 1f; //joga o objeto
            entroNoRange = false;
        }
    }

}