using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class BossManager : MonoBehaviour
{
    private int m_turns = 0;
    public int _turn
    {
        get { return m_turns; }
        set { m_turns = value; }
    }
    private int m_turnCounter = 0;
    public int _turnCounter
    {
        get { return m_turnCounter; }
        set { m_turnCounter = value; }
    }
    private int m_bossCounter = 0;
    public int _bossCounter
    {
        get { return m_bossCounter; }
        set { m_bossCounter = value; }
    }
    private void Start()
    {
        GameManager.GetManager()._bossManager = this;
    }
    private void Update()
    {
        if(_turnCounter >= 4 && GameManager.GetManager()._currentBoss == null)
        {
            GameObject _boss = Instantiate(Resources.Load("Boss"),Camera.main.transform) as GameObject;
            _bossCounter += 1;
            _boss.GetComponentInChildren<BossScript>()._hp = m_bossCounter * 100;
            m_turnCounter = 0;
        }
    }
}
