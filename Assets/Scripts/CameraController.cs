using System;
using UnityEngine;

[System.Serializable]
public struct CameraConfiguration
{
    public float yaw;
    public float pitch;
    public float roll;
    public Vector3 pivot;
    public float distance;
    public float fov;
    
    public Quaternion GetRotation()
    {
        return Quaternion.Euler(pitch, yaw, roll);
    }

    public Vector3 GetPosition()
    {
        return pivot + GetRotation() * (Vector3.back * distance);
    }
    
    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fov, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }

}

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public CameraConfiguration cameraConfiguration;
    
    private static CameraController instance = null;
    public static CameraController Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        ApplyConfiguration();
        cameraConfiguration.DrawGizmos(Color.blue);
    }

    
    private void ApplyConfiguration()
    {
        camera.fieldOfView = cameraConfiguration.fov;
        camera.transform.rotation = cameraConfiguration.GetRotation();
        camera.transform.position = cameraConfiguration.GetPosition();
    }

}
