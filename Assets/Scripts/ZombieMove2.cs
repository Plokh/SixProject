using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove2 : MonoBehaviour
{
    private bool ladoCima; //Verifica se o inimigo está olhando para a Direita
    private Animator animator; //Para utilizar a animação do inimigo andando
    [SerializeField] //Para criar a caixinha no Unity
    private float velocidade; //Velocidade em que o inimigo irá andar
    void Start()
    {
        animator = GetComponent<Animator>(); //Inicializando o animator
        ladoCima = true;
    }

    void Update()
    {
        //Checamos em qual lado o inimigo está indo para ativar sua animação e movimentação correspondente
        if (ladoCima == true)
        {
            MoverCima();
        }

        if (ladoCima == false)
        {
            MoverBaixo();
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
    private void MoverCima()
    {
        animator.SetBool("Descendo", false); //Desativamos a animação para a esquerda
        transform.Translate(Localizacao() * (velocidade * Time.deltaTime));
        animator.SetBool("Subindo", true);//Ativamos a animação para a direita
        
    }

    /*MoverEsq: Nesse método nós mudamos a direção baseada no retorno do método Localizacao -1(eixo y), informando que o mesmo
     irá para a esquerda. Neste método também é acionado a animação de movimentação do inimigo.*/
    private void MoverBaixo()
    {
        animator.SetBool("Subindo", false);//Desativamos a animação para a direita
        transform.Translate(Localizacao() * (velocidade * Time.deltaTime));
        animator.SetBool("Descendo", true);//Ativamos a animação para a esquerda
        
    }

    /*Localizacao: Nesse método é checado em qual lado o inimigo está, para a direita ou para a esquerda, se for direita
     irá retornar verdadeiro, e se for esquerda será falso, o resultado será utilizado para a movimentação (Mover...)*/
    private Vector2 Localizacao()
    {
        return ladoCima ? Vector2.up : Vector2.down; //Caso seja direita irá retornar 1, se for esquerda retorna -1
    }

    /*MudarDirecao: nesse método, estamos pegando a posição em que o inimigo em questão se encontra para que possamos 
    checar se o mesmo chegou ao limite ou não de sua patrulha.*/
    private void MudarDirecao()
    {
        ladoCima = !ladoCima;
        this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
    }
}
