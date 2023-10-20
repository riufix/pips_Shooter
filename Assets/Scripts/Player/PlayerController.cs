using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _bulletprefab;

    [SerializeField] float _speed = 5f;
    [SerializeField] float _lifeTime = 5f;
    [SerializeField] float _bulletSpeed = 5f;
    [SerializeField] float _shootDelay = .5f;

    [SerializeField] InputActionReference _shoot;
    [SerializeField] InputActionReference _movement;

    bool _slowed = false;
    bool _reloading = false;
    float _slowForce = 1;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movement.action.performed += OnMovement;
        _movement.action.canceled += OnMovement;
        OnMovement(new());
    }

    private void OnDestroy()
    {

        _movement.action.performed -= OnMovement;
        _movement.action.canceled -= OnMovement;
    }


    void Update()
    {
        if (_shoot.action.IsPressed())
        {
            if(!_reloading){
                _reloading = true;
                GameObject newBullet = Instantiate(_bulletprefab, _firePoint.position, transform.rotation);
                Vector3 forward = newBullet.transform.up;
                Rigidbody2D bullettrajectory = newBullet.GetComponent<Rigidbody2D>();
                bullettrajectory.velocity = forward * _bulletSpeed;
                Destroy(newBullet, _lifeTime);
                StartCoroutine(Reload());
            }
        }
    }

    private void OnMovement(CallbackContext ctx)
    {
        Vector2 Mov = _movement.action.ReadValue<Vector2>();
        _rb.velocity = Mov * _speed + new Vector2(0,3);
        if (_slowed) _rb.velocity /= _slowForce;
    }   

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(_shootDelay);
        _reloading = false;
    }

    private void Slow(float slowForce){
        _slowed = true;
        _slowForce = slowForce;

        StartCoroutine(ResetSlow());

        IEnumerator ResetSlow(){
            yield return new WaitForSeconds(1);
            _slowed = false;
        }
    }
}
