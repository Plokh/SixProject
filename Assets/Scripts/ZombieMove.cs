using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    private bool ladoDir; //Verifica se o inimigo está olhando para a Direita
    private Animator animator; //Para utilizar a animação do inimigo andando
    [SerializeField] //Para criar a caixinha no Unity
    private float velocidade; //Velocidade em que o inimigo irá andar

    [SerializeField]
    private float tempoParado;
    [SerializeField]
    private float duracaoParado = 5;

    private float tempoPatrulha;
    [SerializeField]
    private float duracaoPatrulha = 5;

    private float tempoAtacar;
    [SerializeField]
    private float duracaoAtacar = 5;

    public bool estaPatrulhando;
    public bool atacar;

    public float playerDistancia;
    public float ataqueDistancia;
    public GameObject player;
    public GameObject prefabProjetil;
    public Transform instanciador;

    void Start()
    {
        animator = GetComponent<Animator>(); //Inicializando o animator
        ladoDir = true;//lado ao qual o personagem irá começar a andar
        estaPatrulhando = false;// deixo negado pois o persongaem irá começar parado
        atacar = false;//Não terá o alcance para atacar então começa negado
    }

    void Update()
    {
        MudarEstado();
        playerDistancia = transform.position.x - player.transform.position.x;// Pego a posição do persongaem e subtraio com a posição do player no eixo X pra saber a distancia em que o player está do personagem
        if (Mathf.Abs(playerDistancia) < ataqueDistancia) // Pego o resultado da distancia em que o player está e subtraio com  a distancia em que o personagem deve começar a atacar
        {
            atacar = true;//caso o player esteja perto o suficiente o personagem começa a atacar e sai do modo espera
            estaPatrulhando = false;
            Parado();
            Atirar();
        }
        else
        {
            MudarEstado();//caso contrário ele irá checar qual a ação que o mesmo deve realizar
        }
        
    }

    //métodos

    /*OnTriggerEnter2D: Aqui nós checamos se o inimigo chegou ao limite de sua patrulha, para isso colocamos um if que 
     utilizando a tag definida no Unity para o Limite checa se a posição deve ser alterada. Caso o inimigo tenha
     tocado o limite utilizamos o método MudarDirecao para que que ele altere a sua rota*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Limite")
        {
            MudarDirecao();
        }
    }


    /*MoverDir: Nesse método nós mudamos a direção baseada no retorno do método Localizacao 1(eixo y), informando que o mesmo
     irá para a direita. Neste método também é acionado a animação de movimentação do inimigo.*/
    private void Mover()
    {
        transform.Translate(Localizacao() * (velocidade * Time.deltaTime));
    }

    
    /*Localizacao: Nesse método é checado em qual lado o inimigo está, para a direita ou para a esquerda, se for direita
     irá retornar verdadeiro, e se for esquerda será falso, o resultado será utilizado para a movimentação (Mover...)*/
    private Vector2 Localizacao()
    {
        return ladoDir ? Vector2.right : Vector2.left; //Caso seja direita irá retornar 1, se for esquerda retorna -1
    }

    /*MudarDirecao: nesse método, estamos pegando a posição em que o inimigo em questão se encontra para que possamos 
    checar se o mesmo chegou ao limite ou não de sua patrulha.*/
    private void MudarDirecao()
    {
        ladoDir = !ladoDir;
        this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
    }
    
    //Métodos de ações e animações

    void Parado()
    { //Faço a checagem do tempo em que o persongaem ira parar para recomeçar a patrulhar
        tempoParado += Time.deltaTime;
        if (tempoParado <= duracaoParado)
        {
            animator.SetBool("AndandoDireita", estaPatrulhando);//ativo a animação andando quando o mesmo atinge o tempo limite de ficar parado
            tempoPatrulha = 0;
        }
        else
        {
            estaPatrulhando = true;
        }

    }

    void Patrulhar()
    {//Faço a checagem do tempo em que o persongaem ira patrulhar para depois fazer um intervalo
        tempoPatrulha += Time.deltaTime;
        if (tempoPatrulha <= duracaoPatrulha)
        {
            animator.SetBool("AndandoDireita", estaPatrulhando);//Desativo a animação andando quando o mesmo atinge o tempo limite de ficar patrulhando
            Mover();
            tempoParado = 0;
        }
        else
        {
            estaPatrulhando = false;
        }

    }

    void Atirar()
    {
        if(playerDistancia < 0 && !ladoDir || playerDistancia > 0 && ladoDir)//identifico qual o lado em que o player esta para mudar a direção em que o personagem ira atirar
        {
            MudarDirecao();
        }
        if (atacar == true)
        {//faço a checagem do tempo em que o personagem irá atirar
            tempoAtacar += Time.deltaTime;
            if(tempoAtacar >= duracaoAtacar)
            {
                animator.SetTrigger("Atacando");//ativo a animação de tiro do persongaem
                tempoAtacar = 0;
            }
        }
    }

    void MudarEstado()//Checo baseado nos estado qual será a próxima ação do persongaem
    {
        if (!atacar)
        {
            if (!estaPatrulhando)
            {
                Parado();
            }
            else
            {
                Patrulhar();
            }
        }
    }

    public void ResetarAtaques()// para que o personagem resete o ataque e continue atacando sem apssar para outro estado
    {
        atacar = false;
    }

    public void InstanciarProjetil()//Defino o obejto ao qual será instanciado, assim como a posição e rotação do mesmo
    {
        GameObject tmp = Instantiate(prefabProjetil, instanciador.position, instanciador.rotation);

        if (ladoDir)
        {
            tmp.GetComponent<Bala>().Inicializar(Vector2.right);//Identifico qual lado o player está para que a bala esteja do mesmo lado que o player
        }
        else
        {
            tmp.GetComponent<Bala>().Inicializar(Vector2.left);
        }
    }
}
