using System;
using UnityEngine;
using static UnityEngine.Mathf;

public class DollyView : AView
{
    public bool isActive;
    
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

    private void Start()
    {
        SetActive(isActive);
    }


    private void Update()
    {
        dir = (target.transform.position - transform.position).normalized;
        yaw = Atan2(dir.x, dir.z) * Rad2Deg;
        pitch = -Asin(dir.y) * Rad2Deg;

        Debug.Log(Input.GetAxis("Horizontal"));
        distanceOnRail += Input.GetAxis("Horizontal") * speed;

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("fire");
        }
        
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
    
    
    public void SetActive(bool isActive)
    {
        if (isActive) CameraController.Instance.AddView(this);
        else CameraController.Instance.RemoveView(this);
    }
    
    protected override void OnDrawGizmos()
    {
        GetConfiguration().OnDrawGizmos(Color.blueViolet);
    }
}
