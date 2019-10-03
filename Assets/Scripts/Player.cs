using UnityEngine;
using UnityEditor;

public class Player
{
    [SerializeField]
    public Vector2 _playerVelocidade = new Vector2(0.8f, 0.8f);

    public Arma _playerArmaEquipada;
    public Transform _projetilRefTransform;

    Animator _playerAnimator;
    Transform _playerTransform;
    SpriteRenderer _playerSpriteRenderer;
    Rigidbody2D _playerRB2D;

    int andandoKey = Animator.StringToHash("andando");//https://docs.unity3d.com/ScriptReference/Animator.StringToHash.html

    Vector2 _playerMovimento;
    Vector3 _mouseRef;

    public Player(GameObject player)
    {
        _playerAnimator = player.GetComponent<Animator>();
        _playerTransform = player.GetComponent<Transform>();
        _playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        _playerRB2D = player.GetComponent<Rigidbody2D>();
        _playerArmaEquipada = null;
    }

    public void onFixedUpdate()
    {
        Movimento();
    }

    public void onUpdate()
    {
        _mouseRef = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Mouse ref e movimetno player é melhor pegar no monobehavor e passaro por referencia?
        Animacao();
        if (_playerArmaEquipada != null)
        {
            ArmaMovimento();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Atirar();
            }
        }
    }

    void Animacao()//Transição das animações
    { //https://www.youtube.com/playlist?list=PLUur4s1pRGf9jHKo5XAKqP2XyRw35czIK
        //Transição das animações
        if (_playerMovimento.x != 0 || _playerMovimento.y != 0)
        {
            _playerAnimator.SetTrigger(andandoKey);
        }
        else
        {
            _playerAnimator.ResetTrigger(andandoKey);
        }

        //Transição da direção da Sprinte
        if (_playerTransform.position.x < _mouseRef.x)
        {
            _playerSpriteRenderer.flipX = false;
        }
        else if (_playerTransform.position.x > _mouseRef.x)
        {
            _playerSpriteRenderer.flipX = true;
        }
    }   

    void Movimento()//Movimento do personagem
    {
        _playerMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _playerRB2D.MovePosition(_playerRB2D.position + _playerVelocidade * _playerMovimento.normalized * Time.fixedDeltaTime);
    }

    public void PegaArma(Arma armaPega)//Recebe o objeto class arma
    {
        _playerArmaEquipada = armaPega;
    }
        
    public void Atirar()//Atira com a arma
    {
        _playerArmaEquipada.Atira(_mouseRef);
    }

    public void ArmaMovimento()//Rotaciona a arma
    {
        _playerArmaEquipada.Movimenta(_mouseRef); //Arma seguir o mouse
    }

}