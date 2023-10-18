using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    public static GameObject player;

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _bulletprefab;

    [SerializeField] float _speed = 5f;
    [SerializeField] float _lifeTime = 5f;
    [SerializeField] float _bulletDmg = 2f;
    [SerializeField] float _bulletSpeed = 5f;

    [SerializeField] bool _isPressed = false;

    [SerializeField] InputActionReference _mouse;
    [SerializeField] InputActionReference _movement;

    
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;

        _rb = GetComponent<Rigidbody2D>();

        _mouse.action.started += OnShoot;
        _mouse.action.canceled += OnShoot;

        _mouse.action.canceled += OnShootStop;

        _movement.action.performed += OnMovement;
        _movement.action.canceled += OnMovement;
    }

    private void OnDestroy()
    {
        _mouse.action.started -= OnShoot;
        _mouse.action.canceled -= OnShoot;

        _mouse.action.canceled -= OnShootStop;

        _movement.action.performed -= OnMovement;
        _movement.action.canceled -= OnMovement;
    }


    void Update()
    {
        if (_mouse.action.IsPressed())
        {
            _isPressed = true;
            OnShoot(new());
        }
        else
        {
            _isPressed = false;
            OnShootStop(new());
        }
    }

    private void OnMovement(CallbackContext ctx)
    {
        Vector2 Mov = _movement.action.ReadValue<Vector2>();
        _rb.velocity = Mov * _speed;
    }   

    private void OnShoot(CallbackContext ctx)
    {

        StartCoroutine(Shoot());
    }
    private void OnShootStop(CallbackContext ctx)
    {
        StopCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (_isPressed)
        {
            GameObject newBullet = Instantiate(_bulletprefab, _firePoint.position, transform.rotation);
            Vector3 forward = newBullet.transform.up;
            Rigidbody2D bullettrajectory = newBullet.GetComponent<Rigidbody2D>();
            bullettrajectory.velocity = forward * _bulletSpeed;
            Destroy(newBullet, 3f);
            yield return new WaitForSeconds(2.5f);
        }
    }
}
