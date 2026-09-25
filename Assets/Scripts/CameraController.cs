using System;
using System.Collections.Generic;
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
    
    public void OnDrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fov, 10f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }

}

public class CameraController : MonoBehaviour
{
    public Camera camera;
    private CameraConfiguration configuration;
    public CameraConfiguration Configuration {get {return configuration;}}
    private CameraConfiguration targetConfiguration;
    private List<AView> activeViews = new List<AView>();
    public float smoothSpeed = 5;

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

    private void Start()
    {
        if (activeViews.Count == 0) return;
        configuration = ComputeAverage();
        targetConfiguration = ComputeAverage();
    }

    private void Update()
    {
        targetConfiguration = ComputeAverage();

        configuration.yaw = Mathf.LerpAngle(configuration.yaw, targetConfiguration.yaw, smoothSpeed * Time.deltaTime);
        configuration.pitch = Mathf.LerpAngle(configuration.pitch, targetConfiguration.pitch, smoothSpeed * Time.deltaTime);
        configuration.roll = Mathf.LerpAngle(configuration.roll, targetConfiguration.roll, smoothSpeed * Time.deltaTime);

        configuration.pivot = Vector3.Lerp(configuration.pivot, targetConfiguration.pivot, smoothSpeed * Time.deltaTime);
        configuration.distance = Mathf.Lerp(configuration.distance, targetConfiguration.distance, smoothSpeed * Time.deltaTime);
        configuration.fov = Mathf.Lerp(configuration.fov, targetConfiguration.fov, smoothSpeed * Time.deltaTime);


        ApplyConfiguration();
    }

    public void OnDrawGizmos()
    {
        //configuration.OnDrawGizmos(Color.coral);
    }

    private void ApplyConfiguration()
    {
        if (activeViews.Count == 0) return;
        if (camera == null)
        {
            return;
        }

        camera.fieldOfView = configuration.fov;
        camera.transform.rotation = configuration.GetRotation();
        camera.transform.position = configuration.GetPosition();
    }

    public void AddView(AView view)
    {
        activeViews.Add(view);
    }

    public void RemoveView(AView view)
    {
        activeViews.Remove(view);
    }

    public CameraConfiguration ComputeAverage()
    {
        CameraConfiguration cameraConfiguration = new CameraConfiguration();
        float totalWeight = 0;
        Vector3 totalYawVector = Vector3.zero;
        foreach (AView view in activeViews)
        {
            cameraConfiguration.pivot += view.GetConfiguration().pivot * view.weight;
            cameraConfiguration.pitch += view.GetConfiguration().pitch * view.weight;
            cameraConfiguration.roll += view.GetConfiguration().roll * view.weight;
            cameraConfiguration.distance += view.GetConfiguration().distance * view.weight;
            cameraConfiguration.fov += view.GetConfiguration().fov * view.weight;
            
            
            totalWeight += view.weight;
        }
        cameraConfiguration.pivot /= totalWeight;
        cameraConfiguration.pitch /= totalWeight;
        cameraConfiguration.roll /= totalWeight;
        cameraConfiguration.distance /= totalWeight;
        cameraConfiguration.fov /= totalWeight;
        cameraConfiguration.yaw = ComputeAverageYaw();

        return cameraConfiguration;

    }

    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews)
        {
            CameraConfiguration config = view.GetConfiguration();
            sum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad),
                Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }

}
