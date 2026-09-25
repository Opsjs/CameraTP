using System;
using UnityEngine;
using static UnityEngine.Mathf;

public class FixedFollowView : AView
{
    public float roll;
    public float fov;
    public GameObject target;
    public GameObject centralPoint;
    public float yawOffsetMax;
    public float pitchOffsetMax;

    public float yaw;
    private float pitch;
    private Vector3 dir;
    private float originalYaw;
    private void Start()
    {
        
        dir = (centralPoint.transform.position - transform.position).normalized;
        originalYaw = Atan2(dir.x, dir.z) * Rad2Deg;
    }
    private void Update()
    {
        dir = (target.transform.position - transform.position).normalized;
        yaw = Atan2(dir.x, dir.z) * Rad2Deg;
        yaw = Clamp(DeltaAngle(originalYaw, yaw), -yawOffsetMax, yawOffsetMax);
        yaw += originalYaw;
        pitch = -Asin(dir.y) * Rad2Deg;
    }
    
    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration cameraConfiguration = new CameraConfiguration();
        cameraConfiguration.yaw = yaw;
        cameraConfiguration.pitch = pitch;
        cameraConfiguration.roll = roll;
        cameraConfiguration.fov = fov;
        cameraConfiguration.pivot = transform.position;
        cameraConfiguration.distance = 0;
        return cameraConfiguration;
    }
    


    protected override void OnDrawGizmos()
    {
        GetConfiguration().OnDrawGizmos(Color.red);
    }
}
