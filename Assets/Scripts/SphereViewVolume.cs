using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public Transform target;
    public float outerRadius;
    public float innerRadius;

    private float distance;

    void Update()
    {

        distance = Vector3.Distance(transform.position, target.position);

        if (distance <= outerRadius && !IsActive)
        {
            Debug.Log("Sphere Volume ACTIVÉ !");
            SetActive(true);
        }

        if (distance > outerRadius && IsActive)
        {
            Debug.Log("Sphere Volume desACTIVÉ !");
            SetActive(false);
        }
    }

    public override float ComputeSelfWeight()
    {
        float innerRadiusDontMoreThanOuterRadius = Mathf.Min(outerRadius, innerRadius);

        if(distance <= innerRadiusDontMoreThanOuterRadius)
        {
            return 1.0f;
        }

        if (distance >= outerRadius)
        {
            return 0.0f;
        }

        float t = (distance - innerRadiusDontMoreThanOuterRadius) / (outerRadius - innerRadiusDontMoreThanOuterRadius);
        return Mathf.Clamp01(1.0f - t);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, innerRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
