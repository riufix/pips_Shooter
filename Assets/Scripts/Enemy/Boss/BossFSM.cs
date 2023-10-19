using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class BossFSM : MonoBehaviour
{
    private enum STATES{
        IDLE = 0,
        CLAWS = 1,
        BUBBLES = 2,
        CRABS = 3,
        RAY = 4,
    }

    private STATES _state;
    [SerializeField] private List<STATES> _playlist = new List<STATES>();
    private int _counter = 0;

    [SerializeField, Foldout("Rayons")] private GameObject _rayon;
    [SerializeField, Foldout("Rayons")] private List<Transform> _transformRayons;
    [SerializeField, Foldout("Rayons")] private AnimationCurve _aimingCurve;
    [SerializeField, Foldout("Rayons")] private float _startAngle;
    [SerializeField, Foldout("Rayons")] private float _rayDuration;

    [SerializeField, Foldout("BulletGen Setup")] private BulletGenerator _bulletGen;
    [SerializeField, Foldout("BulletGen Setup")] private Pattern _crabPattern;
    [SerializeField, Foldout("BulletGen Setup")] private Pattern _bubblePattern;

    [SerializeField, Foldout("Animator Setup, pas toucher")] private Animator _anim;
    [SerializeField, Foldout("Animator Setup, pas toucher"), AnimatorParam("_anim")]private string _rightClawAtkTrigger;
    [SerializeField, Foldout("Animator Setup, pas toucher"), AnimatorParam("_anim")]private string _leftClawAtkTrigger;
    [SerializeField, Foldout("Animator Setup, pas toucher"), AnimatorParam("_anim")]private string _bubbleAtkTrigger;
    [SerializeField, Foldout("Animator Setup, pas toucher"), AnimatorParam("_anim")]private string _crabSpawnTrigger;
    [SerializeField, Foldout("Animator Setup, pas toucher"), AnimatorParam("_anim")]private string _rayAtkTrigger;


    private int Counter { get => _counter; set => _counter = value%_playlist.Count; }

    private void Start() {
        if (_playlist.Count != 0) _state = _playlist[0];
        else throw new ArgumentException("Playlist Vide");

        SwitchStates(_state);
    }

    #region States

    private void Idle(){
        StartCoroutine(IdleCoroutine());

        IEnumerator IdleCoroutine(){
            yield return new WaitForSeconds(1);
            EndState();
        }
    }

    private void Claws(){
        //Tire une pince au hasard puis joue la bonne anim
    }

    private void Bubbles(){
        //Tire les bulles avec le bulletGen et joue la bonne anim
    }

    private void Crabs(){
        //Spawn les crabes avec le bulletGen
    }

    private void Ray(){
        Transform spawner = _transformRayons[Random.Range(0, _transformRayons.Count)];
        float angle = _startAngle*Mathf.Sign(spawner.position.x);

        GameObject raySave = Instantiate(_rayon, spawner);
        raySave.transform.eulerAngles = new Vector3(0f,0f,angle);
        Debug.Log(raySave.transform.eulerAngles);

        StartCoroutine(Ciblage());
        
        IEnumerator Ciblage(){
            float maxAngle = Vector2.SignedAngle(Vector2.up, spawner.position - PlayerSingleton.Instance.transform.position) - angle;
            float timer = 0f;

            while (timer < _rayDuration){
                float w = maxAngle*_aimingCurve.Evaluate(timer/_rayDuration);
                timer += Time.deltaTime;
                raySave.transform.eulerAngles = new Vector3(0f,0f,w + angle);
                yield return 0;
            }

            raySave.transform.eulerAngles = new Vector3(0f,0f,maxAngle + angle);
            Destroy(raySave);
            yield return new WaitForSeconds(1);
            EndState();
        }
    }

    #endregion

    private void SwitchStates(STATES state){
        _state = state;
         switch (state)
        {
            case STATES.IDLE:
                Idle();
                break;
            case STATES.CLAWS:
                Claws();
                break;
            case STATES.BUBBLES:
                Bubbles();
                break;
            case STATES.CRABS:
                Crabs();
                break;
            case STATES.RAY:
                Ray();
                break;
            default:
                break;
        }
    }

    public void EndState(){
        Debug.Log("Ended State :" + _state);
        Counter += 1;
        _state = _playlist[Counter];
        SwitchStates(_state);
    }

}
