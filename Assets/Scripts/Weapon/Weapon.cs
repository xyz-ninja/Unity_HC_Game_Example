using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Header("Main")]
    [SerializeField] private WeaponData _weaponData;

    [Header("Transforms")] 
    [SerializeField] private Transform _shootPointT;
    
    [Header("Options")]
    [ShowIf("_isFirearmsMode"), SerializeField] private GameObject _projectile;
    [SerializeField] private bool _autoSearchEnemies = true;
    [ShowIf("_autoSearchEnemies")] private float _searchEnemiesDelay = 0.3f;
    [SerializeField] private LayerMask _enemiesLayer;

    public Action OnAttack;
    
    private bool _isMeleeMode;
    private bool _isFirearmsMode;

    private void Update() {
        if (_autoSearchEnemies) {
            
        }
    }

    public void Attack() {
        OnAttack.Invoke();

        switch (_weaponData.AttackMode) {
            case WeaponData.ATTACK_MODE.FIREARMS:
                //var projectile = 
                break;
        }
    }
    
    private void OnValidate() {
        if (_weaponData == null) {
            return;
        }
        
        _isMeleeMode = _weaponData.AttackMode == WeaponData.ATTACK_MODE.MELEE;
        _isFirearmsMode = _weaponData.AttackMode == WeaponData.ATTACK_MODE.FIREARMS;
    }

    private void OnDrawGizmos() {
        if (_weaponData == null) {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _weaponData.AttackRange);
    }
}
