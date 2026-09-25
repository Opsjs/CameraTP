using UnityEngine;

public static class MathUtils
{
    public static void LinearBezier(Vector3 A, Vector3 B, float t)
    {
        
    }

    public static void QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t)
    {

    }

    public static void CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
    {

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
