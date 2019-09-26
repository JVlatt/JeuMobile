using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class BossScript : MonoBehaviour
{

    public float _hp = 100;
    private const float tempsPoison = 0.5f;


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
            Destroy(this.gameObject);
            GameManager.GetManager()._UIManager._bossUI.SetActive(false);
        }

    }

    public void setupPoison(float value)
    {

    }
}
