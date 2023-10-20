using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStick : MonoBehaviour
{
    [SerializeField] float _stickForce;
    [SerializeField] int _hitDmg;
    [SerializeField] UnityEvent _onStick;
    [SerializeField] bool _isStuck;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            _onStick.Invoke();
            _isStuck = true;
            other.SendMessage("takeDamage", _hitDmg);
            other.SendMessage("Slow", _stickForce);
            Destroy(gameObject,1);
        }
    }

    private void Update() {
        if(_isStuck) transform.position = PlayerSingleton.Instance.transform.position;
    }
}
