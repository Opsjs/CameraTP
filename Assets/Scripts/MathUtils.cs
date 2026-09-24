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
}
