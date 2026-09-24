using UnityEngine;

public class Curve : MonoBehaviour
{

    private Vector3 A;
    private Vector3 B;
    private Vector3 C;
    private Vector3 D;

    public Vector3 GetPosition(float t)
    {
        return GetPosition(t);
    }

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        return GetPosition(t);
    }

    public void DrawGizmos(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = c;
        Gizmos.DrawLine(A, B);

    }
}
