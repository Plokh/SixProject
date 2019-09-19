using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoPersonagens : MonoBehaviour
{
    [SerializeField]
    Collider2D personagem;
    [SerializeField]
    Collider2D inimigos;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), personagem, true); //Para ignorar as colisões entres os persongagens do jogo
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), inimigos, true); //Para ignorar as colisões entres os inimigos do jogo
    }
}