using System;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;

    public bool isActiveOnStart;

    protected virtual void Start()
    {
        if (isActiveOnStart)
        {
            SetActive(true);
        }
    }

    public void SetActive(bool isActive)
    {
        if (isActive) CameraController.Instance.AddView(this);
        else CameraController.Instance.RemoveView(this);
    }
    public virtual CameraConfiguration GetConfiguration()
    {
        return CameraController.Instance.Configuration;
    }

    protected virtual void OnDrawGizmos()
    {
        GetConfiguration().OnDrawGizmos(Color.aquamarine);
    }
}
