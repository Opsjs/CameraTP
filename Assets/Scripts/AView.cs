using System;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;
    public virtual CameraConfiguration GetConfiguration()
    {
        return CameraController.Instance.Configuration;
    }
}
