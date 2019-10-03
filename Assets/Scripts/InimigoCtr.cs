using UnityEngine;
using UnityEditor;

public class InimigoCtr : MonoBehaviour
{
    public GameObject __tiroPrefabObjeto;
    public Transform _playerTranform;
    Inimigo inimigo = null;

    private void Awake()
    {
        inimigo = new Inimigo(this.gameObject, _playerTranform, __tiroPrefabObjeto);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        inimigo.onUpdate();
    }

    private void FixedUpdate()
    {
        
    }
}