using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Assets.Script;
using UnityEngine.UI;


public class ShootButton : MonoBehaviour
{
    [SerializeField]
    Image _powerBar;
    [SerializeField]
    Image _powerBar2;
    float _holdTime = 0f;
    private bool _isHolding = false;

    private void Update()
    {
        if (_isHolding && _holdTime <= GameManager.GetManager()._player._maxHoldTime)
        {
            _holdTime += Time.deltaTime;
            _powerBar.fillAmount = _holdTime / GameManager.GetManager()._player._maxHoldTime;
            _powerBar2.fillAmount = _holdTime / GameManager.GetManager()._player._maxHoldTime;
        }
    }

    private void Start()
    {
        _powerBar.fillAmount = 0f;
        _powerBar2.fillAmount = 0f;
    }
    public void Hold()
    {
        _isHolding = true;
        GameManager.GetManager()._player._canMove = false;
    }
    public void Release()
    {
        _powerBar.fillAmount = 0f;
        _powerBar2.fillAmount = 0f;
        _isHolding = false;
        GameManager.GetManager()._player.Shoot(_holdTime / GameManager.GetManager()._player._maxHoldTime);
        GameManager.GetManager()._player._canMove = true;
        _holdTime = 0;
    }
}
