using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtaque : MonoBehaviour
{
    private Animator animator;//Inicializa o animator
    public float playerDistancia; //Distancia do player
    public float ataqueDistancia; //Limite para começar a atacar

    public GameObject player;
    public GameObject prefabProjetil; //Pra utilizar o prefab
    public Transform instanciador;// instanciador dos projéteis
    private bool ladoDir; //Lado em que ira atirar

    public bool estaEsperando;
    public bool estaAtacando;

    /*Começo com ele patrulhando o mapa para achar o inimigo, destivando assim o ataque e setando que o ladoDireito é o lado em que irá atirar os projéteis*/
    void Start()
    {
        animator = GetComponent<Animator>();
        estaEsperando = true;
        estaAtacando = false;
        ladoDir = true;
        
    }

    void Update()
    {
        playerDistancia = transform.position.x - player.transform.position.x; // Pego a posição do boss e subtraio com a posição do personagem no eixo X pra saber a distancia em que o player está do boss
        if (Mathf.Abs(playerDistancia) < ataqueDistancia) // Pego o resultado da distancia em que o player está e subtraio com  a distancia em que o boss deve começar a atacar
        {
            estaAtacando = true; //caso o player esteja perto o suficiente o boss começa a atacar e sai do modo espera
            estaEsperando = false;
            Atacando();
        }
        else
        {
            estaAtacando = false;//caso contrário o mesmo continua esperando
            estaEsperando = true;
            Esperando();
        }
    }

    void Atacando()
    {
        if (estaAtacando == true)
        {
            animator.SetBool("Esperando", false);// desativo a animação esperando e ativo a animação atacando do boss
            animator.SetTrigger("EstaAtacando");
        }
    }

    void Esperando()
    {
        if (estaEsperando == true)
        {
            animator.SetBool("Esperando", true);//ativo a animação de espera do boss           
        }
    }

    public void InstanciarProjetil()//Defino o obejto ao qual será instanciado, assim como a posição e rotação do mesmo
    {
        GameObject tmp = Instantiate(prefabProjetil, instanciador.position, instanciador.rotation);

        if (ladoDir)
        {
            tmp.GetComponent<Bala>().Inicializar(Vector2.right);//Como o boss irá atacar pro lado direito ele inicializa a classe bala para pegar os atributos da bala e fazer a mesma aparecer no cenário
        }
        
    }
}
