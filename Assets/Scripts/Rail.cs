using System;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using Object = System.Object;

public class Rail : MonoBehaviour
{
    public bool IsLoop;
    public float position;

    private float length;
    


    private void Start()
    {
        length = GetLength();
        GetPosition(position);
    }

    private float GetLength()
    {
        float distance = 0;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            distance += Vector3.Distance(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }

        if (IsLoop)
        {
            distance += Vector3.Distance(transform.GetChild(transform.childCount - 1).transform.position, transform.GetChild(0).transform.position);
        }
        return distance;
    }

    private float GetLengthBetween(int start, int end)
    {
        return Vector3.Distance(transform.GetChild(start).transform.position, transform.GetChild(end).transform.position);
    }

    private float GetDistanceForPosition(float distance)
    {
        length = GetLength();
        if (!IsLoop) distance = Mathf.Clamp(distance, 0, length);
        else
        {
            if (distance < 0) return 0;
            if (distance >= length)
            {
                distance -= length;
                return GetDistanceForPosition(distance);
            }
        }
        return distance;
    }
    
    public Vector3 GetPosition(float distance)
    {
        distance = GetDistanceForPosition(distance);
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            float temp = GetLengthBetween(i, i + 1);
            if (temp >= distance)
            {
                Vector3 pos = (transform.GetChild(i + 1).transform.position - transform.GetChild(i).transform.position).normalized * distance;
                pos += transform.GetChild(i).transform.position;
                return pos;
            }
            else
            {
                distance -= temp;
            }
        }
        Vector3 pos2 = (transform.GetChild(0).transform.position - transform.GetChild(transform.childCount - 1).transform.position).normalized * distance;
        pos2 += transform.GetChild(transform.childCount - 1).transform.position;
        Debug.Log(pos2);
        return pos2;
        
    }
    
    public Vector3 GetNearestPointOnRail(Vector3 target)
    {
        List<Vector3> nearestPointsBySegment = new List<Vector3>();
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            nearestPointsBySegment.Add(MathUtils.GetNearestPointOnSegment(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position, target));
        }

        if (IsLoop)
        {
            nearestPointsBySegment.Add(MathUtils.GetNearestPointOnSegment(transform.GetChild(transform.childCount - 1).transform.position, transform.GetChild(0).transform.position, target));
        }
        
        return GetNearestPointFromList(nearestPointsBySegment, target);

    }
    

    private Vector3 GetNearestPointFromList(List<Vector3> points, Vector3 target)
    {
        Vector3 nearestPoint = points[0];
        foreach (Vector3 point in points)
        {
            if (Vector3.Distance(point, target) < Vector3.Distance(nearestPoint, target))
            {
                nearestPoint = point;
            }
        }
        return nearestPoint;
    }
    
    public void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            length = GetLength();
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GetPosition(position), .4f);
        
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.GetChild(i).transform.position, .4f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.GetChild(transform.childCount - 1).transform.position, .4f);
        if (IsLoop)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).transform.position, transform.GetChild(0).transform.position);
        }
        
        
    }
}
