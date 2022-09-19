using System;
using UnityEngine;

[Serializable]
public class QuadraticBezierMovement : MovementTypeBase
{
    [SerializeField] private Vector3 _bending;

    public override Vector3 NextPosition(Vector3 targetPosition, float delta) {
        Progress = Mathf.Min(Progress + delta * StepScale, 1.0f);

        var bezierX = BezierUtils.GetPointQuadratic(Progress, InitialPosition.x, targetPosition.x + _bending.x,
            targetPosition.x);

        var bezierY = BezierUtils.GetPointQuadratic(Progress, InitialPosition.y, targetPosition.y + _bending.y,
            targetPosition.y);

        var bezierZ = BezierUtils.GetPointQuadratic(Progress, InitialPosition.z, targetPosition.z + _bending.z,
            targetPosition.z);

        var nextPosition = new Vector3(bezierX, bezierY, bezierZ);

        return nextPosition;
    }

    public override void OnInit() {
    }
}
