using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private int _hp;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDeath;

    private void Awake() {
        if(GetComponent<EnemyBehaviour>() != null) _hp = GetComponent<EnemyBehaviour>().Enemy.Hp;
    }

    private void TakeDamage(int dmg){
        _hp -= dmg;
        if(_hp<=0) Death();
        else _onDamage.Invoke();
    }

    private void Death(){
        Destroy(gameObject);
        _onDeath.Invoke();
    }
}
