using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToCam : MonoBehaviour
{
    [SerializeField] private int _enableRange;
    [SerializeField] private Transform _camera;
    bool _first = true;

    private void Update() {
        if(_first){
            if(Mathf.Abs(_camera.position.y - transform.position.y) <= _enableRange ){
                _first = false;
                GetComponent<BossFSM>().enabled = true;
                transform.SetParent(_camera);
            }
        }
    }

}
