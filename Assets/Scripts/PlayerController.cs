using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _lifeTime = 5f;

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMovement(InputValue val)
    {
        Vector2 Mov = val.Get<Vector2>();
        _rb.velocity = Mov * _speed;
    }

    private void OnShoot()
    {

    }
}
