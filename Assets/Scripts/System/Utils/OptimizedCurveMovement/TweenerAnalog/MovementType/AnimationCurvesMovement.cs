using UnityEngine;

public class AnimationCurvesMovement : MonoBehaviour
{
    [Header("Cycle Durations (Seconds)")] 
    public float mCycleDurationX;
    public float mCycleDurationY;
    public float mCycleDurationZ;

    [Header("Animation Curves")] 
    public AnimationCurve mCurveX;
    public AnimationCurve mCurveY;
    public AnimationCurve mCurveZ;

    private float _mElapsed = 0f;
    private Vector3 _mTempPosition;

    public void MoveTo(Vector3 position, float delta)
    {
        Debug.Log("activated");

        _mElapsed += delta;

        _mTempPosition = position;

        _mTempPosition.x += mCurveX.Evaluate(mCycleDurationX);
        _mTempPosition.y += mCurveY.Evaluate(mCycleDurationY);
        _mTempPosition.z += mCurveZ.Evaluate(mCycleDurationZ);

        transform.localPosition = _mTempPosition;
    }

    private float Evaluate(float cycleDuration)
    {
        return Mathf.Sin(2f * Mathf.PI * _mElapsed * (1f / cycleDuration));
    }
}