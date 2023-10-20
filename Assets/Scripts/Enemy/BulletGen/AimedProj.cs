using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AimedProj : EnemyProj
{
    public override void Init(){
        Vector2 direction = PlayerSingleton.Instance.transform.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * Speed;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
