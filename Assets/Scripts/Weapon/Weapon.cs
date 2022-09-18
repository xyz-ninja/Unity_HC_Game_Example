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
    [SerializeField] private LayerMask _enemiesLayer;

    private bool _isMeleeMode = false;
    private bool _isFirearmsMode = false;

    private void OnValidate() {
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
