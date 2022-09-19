using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform _visualT;
    
    [Header("Components")]
    [SerializeField] private Rigidbody _rb;

    [Header("Options")]
    [SerializeField] private float _moveSpeed = 120f;
    [SerializeField] private bool _rotateToDirection = true;
    
    private Vector3 _targetMoveDirection = default;

    private bool _isMoving = false;

    #region getters

    public bool IsMoving => _isMoving;

    public Vector3 TargetMoveDirection => _targetMoveDirection;

    #endregion
    
    private void FixedUpdate() {
        if (_targetMoveDirection != default) {
            _rb.velocity = _targetMoveDirection * (_moveSpeed * Time.fixedDeltaTime);
            
            if (_rotateToDirection) {
                RotateToDirection(_targetMoveDirection);
            }
        }
    }

    public void StartDirectionMove(Vector3 direction) {
        if (_targetMoveDirection == default) {
            _targetMoveDirection = direction;

            _isMoving = true;
        }
    }
    
    public void RotateToDirection(Vector3 direction) {
        _visualT.RotateToDirection(direction);
    }

    public void StopMove() {
        _rb.velocity = Vector3.zero;
        _targetMoveDirection = default;
        _isMoving = false;
    }
}
