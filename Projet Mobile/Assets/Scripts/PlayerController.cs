using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PathEditor _pathToFollow;
    public List<PathEditor> _paths;
    public int _pathId = 1;
    [SerializeField]
    private int _currentWayPointId = 0;
    [SerializeField]
    private float _moveSpeed;
    private float _reachDistance = 1.0f;
    [SerializeField]
    private float _rotationSpeed = 0.5f;

    Vector3 _lastPosition;
    Vector3 _currentPosition;

    [SerializeField]
    private float _jumpHeight = 5.0f;
    [SerializeField]
    private float _shootCooldown = 0.5f;
    private float _shootTimer = 0.0f;
    [SerializeField]
    private float _shootDuration = 1.0f;
    private float _shootDurationTimer = 0f;
    public float _damages = 10.0f;
    public bool _canMove = true;
    private bool _isShooting = false;

    [SerializeField]
    private Rigidbody _rb;
    private void Start()
    {
        _pathToFollow = _paths[_pathId];
        _lastPosition = transform.position;
    }

    private void Update()
    {
        Move();
        _shootTimer += Time.deltaTime;
        if(_isShooting)
        {
            _canMove = false;
            _shootDurationTimer += Time.deltaTime;
            if(_shootDurationTimer >= _shootDuration)
            {
                _canMove = true;
                _shootDurationTimer = 0;
                _isShooting = false;
            }
        }
    }

    public void Jump()
    {
        if (Mathf.Abs(_rb.velocity.y) < 0.1f)
        {
            _rb.AddForce(new Vector3(0, _jumpHeight, 0));
        }
    }

    private void Move()
    {
        _pathToFollow = _paths[_pathId];

        float distance = Vector3.Distance(_pathToFollow._wayPoints[_currentWayPointId].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _pathToFollow._wayPoints[_currentWayPointId].position, Time.deltaTime * _moveSpeed);

        var rotation = Quaternion.LookRotation(_pathToFollow._wayPoints[_currentWayPointId].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);

        if (distance <= _reachDistance)
        {
            _currentWayPointId++;
        }

        if (_currentWayPointId >= _pathToFollow._wayPoints.Count)
            _currentWayPointId = 0;
    }

    public void Shoot()
    {
        if(_shootTimer >= _shootCooldown && !_isShooting && GameManager.GetManager()._currentBoss != null)
        {
            GameManager.GetManager()._currentBoss._hp -= _damages;
            GameManager.GetManager()._UIManager.SetBossHP();
            _shootTimer = 0;
            _isShooting = true;
        }
    }
}
