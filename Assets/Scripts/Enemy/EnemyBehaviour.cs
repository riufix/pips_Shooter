using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField, Expandable] private EnemyTypes _enemy;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _enableRange;
    private bool _isEnabled = false;
    private bool _shooting = false;
    private Rigidbody2D _rb;

    private Transform _playerTransform;

    public EnemyTypes Enemy { get => _enemy;}

    private void Awake() {
        _spriteRenderer.sprite = _enemy.Sprite;
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _playerTransform = PlayerSingleton.Instance.transform;
    }

    private void FixedUpdate() {
        if(_isEnabled){
            if(_enemy.Shoot && !_shooting) StartCoroutine(Shoot());

            _enemy.Movement(_rb);

            float angle = Vector3.SignedAngle(Vector3.down, _rb.velocity, Vector3.forward);
            _spriteRenderer.transform.rotation = Quaternion.Euler(0,0,angle);

            Destroy(gameObject, 7);
        }
        else{
            if(Mathf.Abs(_playerTransform.position.y - transform.position.y) <= _enableRange ){
                _isEnabled = true;
            }
        }
    }

    public IEnumerator Shoot(){
        _shooting = true;
        while (_shooting){
            EnemyProj projSave = Instantiate(_enemy.Projectile, transform).GetComponent<EnemyProj>();
            projSave.Direction = transform.up;
            projSave.Speed = _enemy.ShotSpeed;
            yield return new WaitForSeconds(_enemy.ShootDelay);
        }
    }
}
