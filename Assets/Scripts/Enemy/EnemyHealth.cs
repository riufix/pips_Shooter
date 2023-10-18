using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviour))]
public class EnemyHealth : MonoBehaviour
{
    private int _hp;

    private void Awake() {
        _hp = GetComponent<EnemyBehaviour>().Enemy.Hp;
    }

    
}
