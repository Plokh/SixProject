using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtr : MonoBehaviour
{
    public GameObject _armaObjeto;
    Player player = null;
    Arma arma = null;

    private void Awake()
    {
        player = new Player(this.gameObject);
    }

    private void Start()
    {
        _armaObjeto = (GameObject)Instantiate(_armaObjeto, this.gameObject.transform.GetChild(0));
        arma = _armaObjeto.GetComponent<ArmaCtr>().armaCriada();
        player.PegaArma(arma);
    }

    void FixedUpdate()
    {
        player.onFixedUpdate();
    }

    void Update()
    {
        player.onUpdate();
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //collision.gameObject.GetComponent<Arma>();
        }
    }*/
}