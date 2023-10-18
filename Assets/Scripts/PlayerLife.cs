using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] Slider _lifeSlider;
    [SerializeField] TMP_Text _lifepercent;

    [SerializeField] int _life = 100;
    [SerializeField] bool _isDead = false;


    void Start()
    {
        _lifepercent.text = _life.ToString();
    }

    void Update()
    {
        
    }

    public void takeDamage(int amount)
    {
        if (!_isDead)
        {
            _life -= amount;
            _lifepercent.text = _life.ToString();
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
