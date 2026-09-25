using System;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;


    private void Start()
    {

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
