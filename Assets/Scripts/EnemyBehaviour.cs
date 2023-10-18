using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField, Expandable] private EnemyTypes _enemy;
    [SerializeField] private Transform _spriteTransform;

    [SerializeField] private float _enableRange;
    private bool _isEnabled;
    private Rigidbody2D _rb;

    private Transform _playerTransform;

    private int _maxHp;
    private int _hp;
    private bool _shoot;

    private void Awake() {
        _playerTransform = PlayerController.player.transform;
        _maxHp = _enemy.Hp;
        _hp = _maxHp;
        _shoot = _enemy.Shoot;
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(_isEnabled){
            _enemy.Movement(_rb);
            float angle = Vector3.SignedAngle(Vector3.right, _rb.velocity, Vector3.forward);
            _spriteTransform.rotation = Quaternion.Euler(0,0,angle);
        }
        else{
            //Distance transform player transform;
        }
    }
}
