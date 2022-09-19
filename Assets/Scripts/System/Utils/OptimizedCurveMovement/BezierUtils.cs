using UnityEngine;

public static class BezierUtils {
    public static float GetPointQuadratic(this float t, float p0, float p1, float p2) {
        t = Mathf.Clamp01(t);
        var oneMinusT = 1f - t;

        return
            p0 * oneMinusT * oneMinusT +
            p1 * 2f * t * oneMinusT +
            p2 * t * t;
    }

    public static float GetPointCubic(this float t, float p0, float p1, float p2, float p3) {
        t = Mathf.Clamp01(t);
        var oneMinusT = 1f - t;

        return
            p0 * oneMinusT * oneMinusT * oneMinusT +
            p1 * 3f * t * oneMinusT * oneMinusT +
            p2 * 3f * t * t * oneMinusT +
            p3 * t * t * t;
    }
}