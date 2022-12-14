using System;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    private enum MOVE_TYPE { VELOCITY }
    
    [field: SerializeField] public Entity Entity { get; set; }

    [Header("Options")]
    [SerializeField] private MOVE_TYPE _moveType = MOVE_TYPE.VELOCITY;
    [SerializeField] private float _moveSpeed = 80f;
    [SerializeField] private bool _rotateToMoveDirection = true;
    
    private bool _disableYChange = true;
    
    private Vector3 _targetDirection;
    private bool _isMove;

    #region getters/setters

    public bool RotateToMoveDirection {
        get => _rotateToMoveDirection;
        set => _rotateToMoveDirection = value;
    }

    public bool IsMove => _isMove;

    #endregion
    
    private void Update() {
        if (_isMove) {
            if (_rotateToMoveDirection) {
                Entity.RootT.RotateToDirection(_targetDirection);
            }
        }
    }

    private void FixedUpdate() {
        
        if (_isMove == false) {
            return;
        }

        switch (_moveType) {
            case MOVE_TYPE.VELOCITY:
                
                var targetVelocity = _targetDirection * (_moveSpeed * Time.fixedDeltaTime);

                if (_disableYChange) {
                    targetVelocity.y = Entity.Rb.velocity.y;
                }
                
                Entity.Rb.velocity = targetVelocity;
                
                break;
        }
    }

    public void MoveInDirection(Vector3 direction) {
        _isMove = true;
        _targetDirection = direction;
    }

    public void Stop() {
        _isMove = false;
        _targetDirection = default;
    }
}
