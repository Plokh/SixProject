using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D outro)
    {
        if(outro.gameObject.tag != "Player") { //O projétil não atinge o player
            Destroy(gameObject);
        }
    }
}
