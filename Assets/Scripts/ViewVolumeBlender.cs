using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
#region Singleton
    private static ViewVolumeBlender instance = null;
    public static ViewVolumeBlender Instance => instance;
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
#endregion    

    private List<AViewVolume> activeViewVolumes = new List<AViewVolume>();
    private Dictionary<AView, List<AViewVolume>> volumesPerView = new Dictionary<AView, List<AViewVolume>>();


    public void AddVolume(AViewVolume volume)
    {
        
        activeViewVolumes.Add(volume);
        if (!volumesPerView.ContainsKey(volume.view))
        {
            volumesPerView.Add(volume.view, new List<AViewVolume>());
            volume.view.SetActive(true);
        }
        volumesPerView[volume.view].Add(volume);
    }

    public void RemoveVolume(AViewVolume volume)
    {
        activeViewVolumes.Remove(volume);
        volumesPerView[volume.view].Remove(volume);
        if (volumesPerView[volume.view].Count == 0)
        {
            volumesPerView.Remove(volume.view);
            volume.view.SetActive(false);
        }
    }
}
