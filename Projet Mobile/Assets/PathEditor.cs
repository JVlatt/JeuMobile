using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEditor : MonoBehaviour
{
    public Color _rayColor = Color.white;
    public List<Transform> _wayPoints = new List<Transform>();
    Transform[] _array;

    private void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;
        _array = GetComponentsInChildren<Transform>();
        _wayPoints.Clear();
        foreach(Transform wayPoint in _array)
        {
            if(wayPoint != this.transform)
            {
                _wayPoints.Add(wayPoint);
            }
        }

        for(int i = 0; i < _wayPoints.Count; i++)
        {
            Vector3 position = _wayPoints[i].position;
            if(i > 0)
            {
                Vector3 previous = _wayPoints[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position,0.3f);
            }
        }
    }
}
