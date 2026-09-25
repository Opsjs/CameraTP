using UnityEngine;

[System.Serializable]
public class Curve
{

    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;

    public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        Vector3 localPoint = GetPosition(t);
        return localToWorldMatrix.MultiplyPoint(localPoint);
    }

    public void DrawGizmos(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = c;
        
        Vector3 WorldA = localToWorldMatrix.MultiplyPoint(A);
        Vector3 WorldB = localToWorldMatrix.MultiplyPoint(B);
        Vector3 WorldC = localToWorldMatrix.MultiplyPoint(C);
        Vector3 WorldD = localToWorldMatrix.MultiplyPoint(D);

        Gizmos.DrawSphere(WorldA, 0.5f);
        Gizmos.DrawSphere(WorldB, 0.5f);
        Gizmos.DrawSphere(WorldC, 0.5f);
        Gizmos.DrawSphere(WorldD, 0.5f);

        int steps = 20;
        Vector3 previousPoint = WorldA;
        for(int i = 1; i <= steps; i++)
        {
            float stepT = i / (float) steps;
            Vector3 CurrentPoint = GetPosition(stepT, localToWorldMatrix);
            Gizmos.DrawLine(previousPoint, CurrentPoint);
            previousPoint = CurrentPoint;
        }

    }
}
