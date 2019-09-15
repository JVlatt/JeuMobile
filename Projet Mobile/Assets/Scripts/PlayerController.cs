using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PathEditor _pathToFollow;
    public List<PathEditor> _paths;
    public int _pathId = 1;
    public int _currentWayPointId = 0;
    public float _moveSpeed;
    private float _reachDistance = 0.5f;
    public float _rotationSpeed = 0.5f;

    Vector3 _lastPosition;
    Vector3 _currentPosition;

    [SerializeField]
    private float _jumpHeight = 5.0f;

    private Rigidbody _rb;
    private void Start()
    {
        _pathToFollow = _paths[_pathId];
        _lastPosition = transform.position;
    }

    private void Update()
    {
        _pathToFollow = _paths[_pathId];

        float distance = Vector3.Distance(_pathToFollow._wayPoints[_currentWayPointId].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _pathToFollow._wayPoints[_currentWayPointId].position, Time.deltaTime * _moveSpeed);

        var rotation = Quaternion.LookRotation(_pathToFollow._wayPoints[_currentWayPointId].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);

        if(distance <= _reachDistance)
        {
            _currentWayPointId++;
        }

        if (_currentWayPointId >= _pathToFollow._wayPoints.Count)
            _currentWayPointId = 0;
    }

    public void Jump()
    {
        if (Mathf.Abs(_rb.velocity.y) < 0.1f)
        {
            _rb.AddForce(new Vector3(0, _jumpHeight, 0));
        }
    }
}
