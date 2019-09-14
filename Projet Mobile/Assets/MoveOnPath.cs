using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour
{
    public PathEditor _pathToFollow;
    public int _currentWayPointId = 0;
    public float _moveSpeed;
    private float _reachDistance = 0.5f;
    public float _rotationSpeed = 0.5f;
    public string _pathName;

    Vector3 _lastPosition;
    Vector3 _currentPosition;

    private void Start()
    {
        //_pathToFollow = GameObject.Find(_pathName).GetComponent<PathEditor>();
        _lastPosition = transform.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(_pathToFollow._wayPoints[_currentWayPointId].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _pathToFollow._wayPoints[_currentWayPointId].position, Time.deltaTime * _moveSpeed);

        if(distance <= _reachDistance)
        {
            _currentWayPointId++;
        }

        if (_currentWayPointId >= _pathToFollow._wayPoints.Count)
            _currentWayPointId = 0;
    }

}
