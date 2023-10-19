using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class EnemyProj : MonoBehaviour
{
    protected float speed;
    private Vector2 _direction;
    [SerializeField]
    private UnityEvent _onHit;
    protected Rigidbody2D rb;

    public Vector2 Direction { get => _direction; set => _direction = value; }
    public float Speed { get => speed; set => speed = value; }

    private void Awake() {
        rb = transform.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }
    
    private void Start() {
        Init();
    }

    private void FixedUpdate() {
        UpdateLogic();
    }

    public virtual void Init(){
        rb.velocity = Direction * Speed;
    }

    public virtual void UpdateLogic(){
        float projectileAngle = Vector3.SignedAngle(Vector3.up, rb.velocity, Vector3.forward);
        transform.rotation = Quaternion.Euler(0,0,projectileAngle);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            _onHit.Invoke();
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "BulletWall"){
            Destroy(gameObject);
        }
    }
}
