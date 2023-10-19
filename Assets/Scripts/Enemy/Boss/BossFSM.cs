using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

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
    
    [Header("Animator Setup, pas toucher")]
    [SerializeField] private Animator _anim;
    [SerializeField, AnimatorParam("_anim")]private string _rightClawAtkTrigger;
    [SerializeField, AnimatorParam("_anim")]private string _leftClawAtkTrigger;
    [SerializeField, AnimatorParam("_anim")]private string _bubbleAtkTrigger;
    [SerializeField, AnimatorParam("_anim")]private string _crabSpawnTrigger;
    [SerializeField, AnimatorParam("_anim")]private string _rayAtkTrigger;


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
        //Tire les bulles dans une coroutine et joue la bonne anim
    }

    private void Crabs(){
        //Spawn les crabes avec le bulletGen
    }

    private void Ray(){
        //Tire un spawner de Rayon parmis ceux rentre et joue l'anim
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
