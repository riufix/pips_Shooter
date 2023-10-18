using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] Slider _lifeSlider;

    [SerializeField] int _life = 100;
    [SerializeField] bool _isDead = false;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void takeDamage(int amount)
    {
        if (!_isDead)
        {
            _life -= amount;
            _lifeSlider.value = _life;
        }
        else
            return;
    }

    [Button ("takeDmg")]
    private void T()
    {
        takeDamage(10);
    }
}
