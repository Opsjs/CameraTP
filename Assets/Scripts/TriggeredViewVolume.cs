
using System;
using UnityEngine;

public class TriggeredViewVolume : AViewVolume
{
    public GameObject target;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target.tag))
        {
            SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(target.tag))
        {
            SetActive(false);   
        }
    }
}
