using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] int _hitDmg;
    [SerializeField] UnityEvent _onHit;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy"){
            _onHit.Invoke();
            other.SendMessage("TakeDamage", _hitDmg);
        }
    }
}
