using System;
using SerializationUtils.SR;
using UnityEngine;

public class MoveToTransform : MonoBehaviour {
    [SR] [SerializeReference] private MovementTypeBase _movementType;
    [SerializeField] private float _minimumDistance = 0.2f;

    public Transform Target { get; private set; }
    public Vector3 TargetPosition { get; private set; }

    bool isMoveToTransform = false;
    bool isMoveToPoint = false;

    [SerializeField] private bool _isFinished = true;
    private Action _completeAction;

    private void Update() {
        if (_isFinished || _movementType == null || (isMoveToTransform && Target == null) ||
            (isMoveToPoint && TargetPosition == null)) {
            return;
        }

        Vector3 targetPos = Vector3.zero;

        if (isMoveToPoint) {
            targetPos = TargetPosition;
        } else {
            targetPos = Target.position;
        }

        var position = _movementType.NextPosition(targetPos, Time.deltaTime);
        transform.position = position;

        var currentDistance = CalculateFullDistance(transform.position, targetPos);
        if (currentDistance > _minimumDistance) {
            return;
        }

        _isFinished = true;

        _completeAction.Invoke();
        _completeAction = default;
    }

    private float CalculateFullDistance(Vector3 currentPosition, Vector3 targetPosition) {
        return Mathf.Sqrt(
            Mathf.Pow(targetPosition.x - currentPosition.x, 2) +
            Mathf.Pow(targetPosition.y - currentPosition.y, 2) +
            Mathf.Pow(targetPosition.z - currentPosition.z, 2)
        );
    }

    public void StartMoveToTransform(Transform target, float speed, Action onComplete = default) {
        isMoveToTransform = true;
        isMoveToPoint = false;

        Target = target;
        _isFinished = false;

        _movementType.Speed = speed;
        _movementType.Init(transform.position, target.position);

        _completeAction = onComplete;
    }

    public void StartMoveToPoint(Vector3 pos, float speed, Action onComplete = null) {
        if (_movementType == null) {
            Debug.Log("_movementType missed!");

            return;
        }

        isMoveToTransform = false;
        isMoveToPoint = true;

        TargetPosition = pos;
        _isFinished = false;

        _movementType.Speed = speed;


        _movementType.Init(transform.position, pos);

        _completeAction = onComplete;
    }
}