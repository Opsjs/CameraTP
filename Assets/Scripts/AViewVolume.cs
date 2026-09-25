using System;
using UnityEngine;

public abstract class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;

    private int uid;
    public int Uid => uid;
    private static int nextUid = 0;
    
    protected bool IsActive {get; private set;}

    private void Awake()
    {
        uid = nextUid;
        nextUid++;
    }

    public virtual float ComputeSelfWeight()
    {
        return 1.0f;
    }

    protected virtual void SetActive(bool active)
    {
        IsActive = active;
        if (active)
        {
            ViewVolumeBlender.Instance.AddVolume(this);
        }
        else
        {
            ViewVolumeBlender.Instance.RemoveVolume(this);
        }
    }
}
