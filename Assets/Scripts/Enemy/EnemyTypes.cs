using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

enum MovementType{
    Straight,
    Still,
    Homing,
    Sinusoid,
}

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyTypes : ScriptableObject
{

    [SerializeField] private int _hp;
    [SerializeField] private MovementType _movementType;
    [SerializeField] private bool _shoot;
    [SerializeField, EnableIf(nameof(_shoot))] private GameObject _projectile;
    [SerializeField, EnableIf(nameof(_shoot))] private float _shootDelay;
    [SerializeField, EnableIf(nameof(_shoot))] private float _shotSpeed;

    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _moveSpeed;
    [SerializeField, EnableIf(nameof(IsSin))] private float _sineAmplitude;
    [SerializeField, EnableIf(nameof(IsSin))] private float _sineFrequency;

    bool IsSin => _movementType == MovementType.Sinusoid;

    float _time = 0;

    public int Hp { get => _hp;}
    public bool Shoot { get => _shoot;}
    public Sprite Sprite { get => _sprite;}
    public GameObject Projectile { get => _projectile; set => _projectile = value; }
    public float ShootDelay { get => _shootDelay;}
    public float ShotSpeed { get => _shotSpeed;}

    public void Movement(Rigidbody2D rb, Transform target = null){
        switch (_movementType)
        {
            case MovementType.Straight:
                Straight(rb);
                break;
            case MovementType.Still:
                Still(rb);
                break;
            case MovementType.Homing:
                Homing(rb, target);
                break;
            case MovementType.Sinusoid:
                Sinusoid(rb);
                break;
        }
        rb.velocity = RotateV2(rb.velocity, Vector2.Angle(Vector2.down, rb.transform.up)/180*Mathf.PI);
    }

    private void Straight(Rigidbody2D rb){
        rb.velocity = Vector2.up * _moveSpeed;
    }

    private void Still(Rigidbody2D rb){
        rb.velocity = new Vector2(0,0);
    }

    private void Homing(Rigidbody2D rb, Transform target){

    }

    private void Sinusoid(Rigidbody2D rb){
        _time += Time.deltaTime;
        rb.velocity = new Vector2(Mathf.Cos(_time*_sineFrequency)*_sineAmplitude,_moveSpeed);
        Debug.Log(rb.velocity); 
    }

    private Vector2 RotateV2(Vector2 v, float theta){
        float x = v.x * Mathf.Cos(theta) - v.y * Mathf.Sin(theta);
        float y = v.x * Mathf.Sin(theta) + v.y * Mathf.Cos(theta);
        return new Vector2(x,y);
    }

}
