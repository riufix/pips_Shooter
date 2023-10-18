using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField, Expandable] private EnemyTypes _enemy;
    [SerializeField] private Transform _spriteTransform;
    private int _maxHp;
    private int _hp;
    private bool _shoot;
    private Rigidbody2D _rb;

    private void Awake() {
        _maxHp = _enemy.Hp;
        _hp = _maxHp;
        _shoot = _enemy.Shoot;
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        _enemy.Movement(_rb);
        float projectileAngle = Vector3.SignedAngle(Vector3.right, _rb.velocity, Vector3.forward);
        _spriteTransform.rotation = Quaternion.Euler(0,0,projectileAngle);
    }
}
