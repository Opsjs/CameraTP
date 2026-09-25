using UnityEngine;

public static class MathUtils
{
    public static Vector3 LinearBezier(Vector3 A, Vector3 B, float t)
    {
        return Vector3.Lerp(A, B, t);
    }

    public static Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t)
    {
        Vector3 p0 = LinearBezier(A, B, t);
        Vector3 p1 = LinearBezier(B, C, t);
        return LinearBezier(p0, p1, t);
    }

    public static Vector3 CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
    {
        Vector3 p0 = QuadraticBezier(A, B, C, t);
        Vector3 p1 = QuadraticBezier(B, C, D, t);
        return LinearBezier(p0, p1, t);
    }

    public static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 ac = target - a;
        Vector3 n = (b - a).normalized;
        float scalar = Vector3.Dot(ac, n);
        scalar = Mathf.Clamp(scalar, 0, Vector3.Distance(a, b));
        Vector3 nearestPoint = a + n * scalar;
        return nearestPoint;
    }
}
