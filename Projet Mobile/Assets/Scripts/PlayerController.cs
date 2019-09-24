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
    public bool _canMove = true;
    public bool _hasShot = false;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _damages = 5.0f;
    private void Start()
    {
        _pathToFollow = _paths[_pathId];
        _lastPosition = transform.position;
        GameManager.GetManager()._player = this;
    }

    private void Update()
    {
        Move();
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

    public void Shoot(float amount)
    {
        if(GameManager.GetManager()._currentBoss != null)
        {
            Debug.Log("Amount is : " + amount);
            if (amount > 0.7f && !_hasShot)
            {
                GameManager.GetManager()._currentBoss._hp -= _damages * 2.0f;
                _hasShot = true;
                Debug.Log("Damages done : " + _damages * 2.0f);
            }
            if (amount > 0.3f && !_hasShot)
            {
                GameManager.GetManager()._currentBoss._hp -= _damages;
                _hasShot = true;
                Debug.Log("Damages done : " + _damages * 1.5f);
            }
            
            GameManager.GetManager()._UIManager.SetBossHP();
            _hasShot = false;

        }
    }
}
