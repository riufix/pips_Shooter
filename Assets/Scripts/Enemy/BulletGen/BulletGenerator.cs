using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] private Pattern _testPattern;

    public void PlayPattern(Pattern pattern){
        if (pattern != null) StartCoroutine(PlayPatternCoroutine());
        
        IEnumerator PlayPatternCoroutine(){
            float angleStep = pattern.ProjectileAngle / (pattern.NumberProjectile-1);
            float angle = pattern.ProjectileAngleOffset;
            if(pattern.CompositePattern.Count >= 1){
                foreach (Pattern compositePattern in pattern.CompositePattern)
                {
                    PlayPattern(compositePattern);
                }
            }
            for(int i = 0; i < pattern.NumberProjectile; i++){
                Vector2 direction = new Vector2(Mathf.Sin(angle*Mathf.PI/180),Mathf.Cos(angle*Mathf.PI/180));
                Rigidbody2D projSave = Instantiate(pattern.Projectile, transform).GetComponent<Rigidbody2D>();
                projSave.velocity = direction* pattern.ProjectileSpeed;
                projSave.transform.eulerAngles = new Vector3(0,0,Vector2.SignedAngle(Vector2.up, projSave.velocity));
                if(pattern.ProjectileDelay > 0){
                    yield return new WaitForSeconds(pattern.ProjectileDelay);
                }
                angle += angleStep;
            }
            
        }
    }

    [Button] public void TestPattern(){
        PlayPattern(_testPattern);
    }
}
