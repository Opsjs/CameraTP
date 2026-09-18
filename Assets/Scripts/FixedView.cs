using UnityEngine;

public class FixedView : AView
{
    public float yaw;
    public float pitch;
    public float roll;
    public float fov;
    public bool isActiveOnStart;

    private void Start()
    {
        if (isActiveOnStart)
        {
            SetActive(true);
        }
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
}
