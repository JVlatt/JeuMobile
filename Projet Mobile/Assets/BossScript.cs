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

    private Transform _spawnPos;

    [SerializeField]
    private float _spawnDistance = 10.0f;

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
        if(isExter)
        {
            Instantiate(Resources.Load("Exter1"));
            Instantiate(Resources.Load("Exter2"));
        }
        else
        {
            Instantiate(Resources.Load("Inter"));
        }
        _exter = !isExter;
        _attackCounter++;
    }

    public void GetPositions()
    {
        float distance = Vector3.Distance(GameManager.GetManager()._player.transform.position, GameManager.GetManager()._player._paths[1]._wayPoints[GameManager.GetManager()._player._currentWayPointId].position);
        if ( distance >= _spawnDistance)
        {
            Vector3 norm = Vector3.Normalize(GameManager.GetManager()._player._paths[1]._wayPoints[GameManager.GetManager()._player._currentWayPointId].position - GameManager.GetManager()._player.transform.position);
            _spawnPos.position = GameManager.GetManager()._player.transform.position + (_spawnDistance * norm);
            _spawnPos.rotation = Quaternion.LookRotation(GameManager.GetManager()._player._paths[1]._wayPoints[GameManager.GetManager()._player._currentWayPointId].position - transform.position);
        }
        else
        {
            float dist = _spawnDistance - distance;
        }
    }
}
