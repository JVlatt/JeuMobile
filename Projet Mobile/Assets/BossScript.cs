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
        }

    }

    public void setupPoison(float value)
    {
        _poisonDamages = value;
        _poisonTimer = 0;
        _poisonTick = 0;
        _poisoned = true;
    }
}
