using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePath : MonoBehaviour
{

    [SerializeField]
    public PathEditor _pathToFollow;
    [SerializeField]
    public int _currentWayPointId = 0;
    
    [SerializeField]
    public float _moveSpeed = 2.0f;
    [SerializeField]
    public bool _canRotate = true;
    [SerializeField]
    public float _rotationSpeed = 5.0f;

    [SerializeField]
    public float _reachDistance = 0.5f;

    private void Update()
    {
        float distance = Vector3.Distance(_pathToFollow._wayPoints[_currentWayPointId].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _pathToFollow._wayPoints[_currentWayPointId].position, Time.deltaTime * _moveSpeed);

        if(_canRotate)
        {
            var rotation = Quaternion.LookRotation(_pathToFollow._wayPoints[_currentWayPointId].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }

        if (distance <= _reachDistance)
        {
            _currentWayPointId++;
        }

        if (_currentWayPointId >= _pathToFollow._wayPoints.Count)
            _currentWayPointId = 0;
    }
    
}
