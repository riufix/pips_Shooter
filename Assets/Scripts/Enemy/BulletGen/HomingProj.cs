using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProj : EnemyProj
{
    private Transform _target;
    [SerializeField]
    private float _rotationSpeed = 200.0f;

    public override void Init(){
        Vector2 direction = PlayerSingleton.Instance.transform.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * Speed;
        _target = PlayerSingleton.Instance.transform;
    }

    public override void UpdateLogic()
    {
        Vector2 direction = PlayerSingleton.Instance.transform.position - transform.position;
        direction.Normalize();

        float rotate = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotate * _rotationSpeed;

        rb.velocity = transform.up * Speed;

        base.UpdateLogic();
    }
}
