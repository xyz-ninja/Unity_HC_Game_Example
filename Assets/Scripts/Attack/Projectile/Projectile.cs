using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    [Header("Transforms")] 
    [SerializeField] private Transform _visualT;

    [Header("Components")] 
    [SerializeField] private ProjectileCollisions _collisions;
    [SerializeField] private MoveInDirection _moveInDirection;

    [Header("Options")] 
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _autoDestroyTime = 3f;
    [SerializeField] private float _beforeDestroyDelay = 0.1f;

    #region getters

    public int Damage => _damage;

    #endregion
    
    private Timer _autoDestroyTimer;
    
    private bool _isReady;

    private void Awake() {
        _autoDestroyTimer = new Timer(_autoDestroyTime);
    }

    // возможна перегрузка
    public void Init(Vector3 direction, string entityTag) {
        _moveInDirection.StartDirectionMove(direction);
        _collisions.AddTargetEntityTag(entityTag);

        _isReady = true;
    }

    private void Update() {
        
        if (_isReady == false) {
            _autoDestroyTimer.Reload();
            return;
        }
        
        _autoDestroyTimer.Update(Time.deltaTime);

        if (_autoDestroyTimer.IsFinish()) {
            DestroyProjectile();
        }
    }

    public void DestroyProjectile() {

        LeanPool.Despawn(gameObject, _beforeDestroyDelay);
        
        _autoDestroyTimer.Reload();
    }

    private void OnDisable() {
        _collisions.Clear();
        _moveInDirection.StopMove();
        
        _isReady = false;
    }
}
