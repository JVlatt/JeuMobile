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
    [SerializeField]
    private float m_maxHoldTime;
    public float _maxHoldTime
    {
        get { return m_maxHoldTime; }
        set { m_maxHoldTime = value; }
    }

    [Header("Timer Item")]
    private float timerAcceleration;
    private float accelerationValue;
    private float timerPoison;
    private float poisonValue;


    private void Start()
    {
        _pathToFollow = _paths[_pathId];
        _lastPosition = transform.position;
        GameManager.GetManager()._player = this;
    }

    private void Update()
    {
        Move();
        ItemTimer();
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
        transform.position = Vector3.MoveTowards(transform.position, _pathToFollow._wayPoints[_currentWayPointId].position, Time.deltaTime * (_moveSpeed+accelerationValue));

        var rotation = Quaternion.LookRotation(_pathToFollow._wayPoints[_currentWayPointId].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);

        if (distance <= _reachDistance)
        {
            _currentWayPointId++;
        }

        if (_currentWayPointId >= _pathToFollow._wayPoints.Count)
        {
            _currentWayPointId = 0;
            GameManager.GetManager()._bossManager._turn += 1;
            GameManager.GetManager()._bossManager._turnCounter += 1;
        }
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
                if (poisonValue > 0)
                    GameManager.GetManager()._currentBoss.setupPoison(poisonValue);
            }
            if (amount > 0.3f && !_hasShot)
            {
                GameManager.GetManager()._currentBoss._hp -= _damages;
                _hasShot = true;
                Debug.Log("Damages done : " + _damages * 1.5f);
                if (poisonValue > 0)
                    GameManager.GetManager()._currentBoss.setupPoison(poisonValue);
            }
            
            GameManager.GetManager()._UIManager.SetBossHP();
            _hasShot = false;
        }
    }

    private void ItemTimer()
    {
        if (timerAcceleration > 0)
            timerAcceleration -= Time.deltaTime;
        else if(timerAcceleration>-1)
        {
            accelerationValue = 0;
        }

        if (timerPoison > 0)
            timerPoison -= Time.deltaTime;
        else if (timerPoison > -1)
        {
            poisonValue = 0;
        }
    }

    public void SetupPoison(float time,float value)
    {
        timerPoison = time;
        poisonValue = value;
    }

    public void SetupAcc(float time, float value)
    {
        timerAcceleration = time;
        accelerationValue = _moveSpeed*value;
    }

    public void ActiveItem(ItemPassif.TYPE type,float value)
    {
        switch (type)
        {
            case ItemPassif.TYPE.ATTAQUE:
                _damages += value;
                break;
            case ItemPassif.TYPE.SPEED_ATTAQUE:
                _maxHoldTime -= value;
                break;
            case ItemPassif.TYPE.DOUBLE_ATTAQUE:
                break;
            case ItemPassif.TYPE.BERSERK:
                break;
            case ItemPassif.TYPE.NUDITE:
                break;
            case ItemPassif.TYPE.CHANCE:
                break;
            case ItemPassif.TYPE.SUPERCAPSULE:
                break;
            case ItemPassif.TYPE.SHIELD:
                break;
            case ItemPassif.TYPE.RETURN:
                break;
            default:
                break;
        }
    }
}
