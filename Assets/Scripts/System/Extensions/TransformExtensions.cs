using DG.Tweening;
using UnityEngine;

public static class TransformExtensions {

    public static void RotateToDirection(this Transform T, Vector3 direction, bool withAnimation = true) {

        var checkPosition = T.position;
        checkPosition.y = direction.y;

        var targetDirection = ((checkPosition + direction) - checkPosition).normalized;

        if (targetDirection == Vector3.zero || T.forward == targetDirection) {
            return;
        }

        var targetRotationEuler = Quaternion.LookRotation(targetDirection).eulerAngles;
        
        if (withAnimation) {
            T.DORotate(targetRotationEuler, 0.3f);
        } else {
            T.rotation = Quaternion.Euler(targetRotationEuler);
        }
    }
}
