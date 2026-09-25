using System;
using UnityEngine;
using static UnityEngine.Mathf;

public class DollyView : AView
{
    
    public float roll;
    public float distance;
    public float fov;

    public GameObject target;

    public Rail rail;
    public float distanceOnRail;
    public float speed;

    private Vector3 dir;
    private float yaw;
    private float pitch;




    private void Update()
    {
        dir = (target.transform.position - transform.position).normalized;
        yaw = Atan2(dir.x, dir.z) * Rad2Deg;
        pitch = -Asin(dir.y) * Rad2Deg;

        Debug.Log(Input.GetAxis("Horizontal"));
        distanceOnRail += Input.GetAxis("Horizontal") * speed;
        
        this.transform.position = rail.GetPosition(distanceOnRail);
        
        
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
        GetConfiguration().OnDrawGizmos(Color.blueViolet);
    }
}
