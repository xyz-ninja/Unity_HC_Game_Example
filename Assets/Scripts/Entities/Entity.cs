using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class Entity : MonoBehaviour {

    [Header("Entity Transforms")] 
    [SerializeField] protected Transform _rootT;
    [SerializeField] protected Transform _visualT;
    
    [Header("Entity Components")]
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected EntityMovement _movement;

    #region getters

    public Transform RootT => _rootT;

    public Rigidbody Rb => _rb;
    public EntityMovement Movement => _movement;

    #endregion
    
    [Button()]
    public void FillEntityComponents() {
        _rb = GetComponent<Rigidbody>();
        _movement = GetComponent<EntityMovement>();
        _movement.Entity = this;
    }

    private void OnDestroy() {
        _rootT.DOKill();
        transform.DOKill();
    }
}
