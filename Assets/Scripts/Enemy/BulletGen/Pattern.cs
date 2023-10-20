using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pattern")]
public class Pattern : ScriptableObject
{
    [SerializeField]private GameObject _projectile;
    [SerializeField]private int _numberProjectile;
    [SerializeField]private float _projectileSpeed;
    [SerializeField]private float _projectileDelay;
    [SerializeField]private float _projectileAngle;
    [SerializeField]private float _projectileAngleOffset;
    [SerializeField]private List<Pattern> _compositePattern;

    public GameObject Projectile { get => _projectile;}
    public int NumberProjectile { get => _numberProjectile;}
    public float ProjectileDelay { get => _projectileDelay;}
    public float ProjectileAngle { get => _projectileAngle;}
    public float ProjectileAngleOffset { get => _projectileAngleOffset;}
    public float ProjectileSpeed { get => _projectileSpeed;}
    public List<Pattern> CompositePattern { get => _compositePattern;}

    
}
