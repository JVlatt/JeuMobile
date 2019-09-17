using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script;


public class UIManager : MonoBehaviour
{
    public Slider _bossHpBar;
    public GameObject _bossUI;

    private void Awake()
    {
        GameManager.GetManager()._UIManager = this;
    }
    public void SetBossHP()
    {
        _bossHpBar.value = GameManager.GetManager()._currentBoss._hp;
    }
}
