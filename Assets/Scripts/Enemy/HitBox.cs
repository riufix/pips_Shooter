using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    [SerializeField] int _hitDmg;
    [SerializeField] UnityEvent _onHit;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            _onHit.Invoke();
            other.SendMessage("takeDamage", _hitDmg);
        }
    }
}
