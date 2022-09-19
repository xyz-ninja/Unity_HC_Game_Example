using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour {

    [Header("Entity Transforms")] 
    [SerializeField] protected Transform _rootT;
    [SerializeField] protected Transform _visualT;
    
    [Header("Entity Components")]
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected EntityMovement _movement;
    [SerializeField] protected EntityZoneObserver _zoneObserver;
    [SerializeField] protected Health _health;
    
    #region getters

    public Transform RootT => _rootT;

    public Rigidbody Rb => _rb;
    public EntityMovement Movement => _movement;
    public EntityZoneObserver ZoneObserver => _zoneObserver;
    public Health Health => _health;
    
    #endregion
    
    [Button()]
    public void FillEntityComponents() {
        _rb = GetComponent<Rigidbody>();
        _movement = GetComponent<EntityMovement>();
        _movement.Entity = this;
        _zoneObserver = GetComponent<EntityZoneObserver>();
        _health = GetComponent<Health>();
    }

    protected virtual void OnDisable() {
        _rootT.DOKill();
        transform.DOKill();
    }
}
