using UnityEngine;

public class FreeFollowView : AView
{
    public float[] pitch = new float[3];
    public float[] roll = new float[3];
    public float[] fov = new float[3];

    public float yaw;
    public float yawSpeed = 100f;

    public Transform target;

    public Curve curve = new Curve();

    public float curvePosition;
    public float curveSpeed;


    void Start()
    {
        
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        yaw += horizontalInput * yawSpeed * Time.deltaTime;

        float verticalInput = Input.GetAxis("Vertical");
        curvePosition += verticalInput * curveSpeed * Time.deltaTime;
        curvePosition = Mathf.Clamp01(curvePosition);
    }

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();

        Matrix4x4 curveToWorldMatrix = Matrix4x4.TRS(target.position,Quaternion.Euler(0.0f, yaw, 0.0f), Vector3.one);

        config.pivot = curve.GetPosition(curvePosition, curveToWorldMatrix);
        config.distance = 0.0f;
        config.yaw = yaw;

        if (curvePosition <= 0.5f)
        {
            float t = curvePosition / 0.5f;
            config.pitch = Mathf.Lerp(pitch[0], pitch[1], t);
            config.roll = Mathf.Lerp(roll[0], roll[1], t);
            config.fov = Mathf.Lerp(fov[0], fov[1], t);
        }
        else
        {
            float t = (curvePosition - 0.5f) / 0.5f;
            config.pitch = Mathf.Lerp(pitch[1], pitch[2], t);
            config.roll = Mathf.Lerp(roll[1], roll[2], t);
            config.fov = Mathf.Lerp(fov[1], fov[2], t);
        }

            return config;
    }

    private void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Matrix4x4 curveToWorldMatrix = Matrix4x4.TRS(target.position, Quaternion.Euler(0.0f, yaw, 0.0f), Vector3.one);
            curve.DrawGizmos(Color.yellow, curveToWorldMatrix);
        }
    }
}
