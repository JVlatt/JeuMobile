using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class BossScript : MonoBehaviour
{

    public float _hp = 100;
    private const float tempsPoison = 0.5f;
    [SerializeField]
    private float _poisonDuration = 2.0f;
    private float _poisonTimer = 0f;
    private float _poisonTick = 0f;
    private float _poisonDamages = 0f;
    private bool _poisoned = false;
    [SerializeField]
    private float _attackCoolDown = 2.0f;
    private float _attackTimer = 0f;
    [SerializeField]
    private float _pauseDuration = 10.0f;
    private float _pauseTimer = 0f;
    [SerializeField]
    private int _nbAttack = 10;
    private int _attackCounter = 0;

    [SerializeField]
    private Transform _spawnPos;

    [SerializeField]
    private float _spawnDistance = 10.0f;
    private float _dist = 0f;
    private int _spawnId = 0;
    private int _spawnIdstart = 0;
    private int _spawnIdend = 0;

    private bool _exter = true;

    private void Awake()
    {
        GameManager.GetManager()._currentBoss = this;
    }
    private void Start()
    {
        GameManager.GetManager()._UIManager._bossUI.SetActive(true);
    }

    private void Update()
    {
        if (_hp <= 0)
        {
            GameManager.GetManager()._bossManager._turnCounter = 0;
            Destroy(transform.parent.gameObject);
            GameManager.GetManager()._UIManager._bossUI.SetActive(false);
            GameManager.GetManager()._LevelManager.BossKilled();
        }
        else
        {
            if(_poisoned)
            {
                _poisonTimer += Time.deltaTime;
                _poisonTick += Time.deltaTime;
                if(_poisonTimer < _poisonDuration)
                {
                    if (_poisonTick >= tempsPoison)
                    {
                        _hp -= _poisonDamages;
                        _poisonTick = 0.0f;
                    }
                }
                else
                {
                    _poisonTimer = 0;
                    _poisonTick = 0;
                    _poisoned = false;
                }
            }
            _pauseTimer += Time.deltaTime;
            if(_pauseTimer >= _pauseDuration)
            {
                _attackTimer += Time.deltaTime;
                if(_attackTimer >= _attackCoolDown)
                {
                    _attackTimer = 0;
                    Shoot(_exter);
                    if (_attackCounter >= _nbAttack)
                        _pauseTimer = 0;
                }
            }

        }

    }

    public void setupPoison(float value)
    {
        _poisonDamages = value;
        _poisonTimer = 0;
        _poisonTick = 0;
        _poisoned = true;
    }

    public void Shoot(bool isExter)
    {
        GetPositions();
        //Instantiate(Resources.Load("Test"), _spawnPos.position, Quaternion.identity);
        //_attackCounter++;
    }

    public void GetPositions()
    {
        _spawnId = GameManager.GetManager()._player._currentWayPointId;
        float distance = Vector3.Distance(GameManager.GetManager()._player.transform.position, GameManager.GetManager()._player._paths[1]._wayPoints[GameManager.GetManager()._player._currentWayPointId].position);
        if ( distance >= _spawnDistance)
        {
            Vector3 norm = Vector3.Normalize(GameManager.GetManager()._player._paths[1]._wayPoints[GameManager.GetManager()._player._currentWayPointId].position - GameManager.GetManager()._player.transform.position);
            _spawnPos.position = GameManager.GetManager()._player.transform.position + (_spawnDistance * norm);
            _spawnPos.rotation = Quaternion.LookRotation(GameManager.GetManager()._player._paths[1]._wayPoints[GameManager.GetManager()._player._currentWayPointId].position - _spawnPos.position);
        }
        else
        {
            float newDist = _spawnDistance - distance;
            _spawnIdstart = GameManager.GetManager()._player._currentWayPointId;
            _spawnIdend = GameManager.GetManager()._player._currentWayPointId + 1;

            if (_spawnIdend >= GameManager.GetManager()._player._paths[1]._wayPoints.Count - 1)
            {
                _spawnIdend = 0;
            }

            while (newDist > Vector3.Distance(GameManager.GetManager()._player._paths[1]._wayPoints[_spawnIdstart].position, GameManager.GetManager()._player._paths[1]._wayPoints[_spawnIdend].position))
            {
                newDist -= Vector3.Distance(GameManager.GetManager()._player._paths[1]._wayPoints[_spawnIdstart].position, GameManager.GetManager()._player._paths[1]._wayPoints[_spawnIdend].position);
                Debug.Log("Dist restante : " + newDist);
                _spawnIdstart++;
                _spawnIdend++;
                if (_spawnIdend >= GameManager.GetManager()._player._paths[1]._wayPoints.Count)
                {
                    _spawnIdend = 0;
                }
                if (_spawnIdstart >= GameManager.GetManager()._player._paths[1]._wayPoints.Count)
                {
                    _spawnIdstart = 0;
                }
            }
            Vector3 norm = Vector3.Normalize(GameManager.GetManager()._player._paths[1]._wayPoints[_spawnIdend].position - GameManager.GetManager()._player._paths[1]._wayPoints[_spawnIdstart].position);
            _spawnPos.position = GameManager.GetManager()._player._paths[1]._wayPoints[_spawnIdstart].position + (newDist * norm);
            _spawnPos.rotation = Quaternion.LookRotation(GameManager.GetManager()._player._paths[1]._wayPoints[_spawnIdstart].position - _spawnPos.position);
        }
    }
}
