﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _startPlayerPosition;
    private Vector3 _endPlayerPosition;

    public Vector3 _offset;

    
    [SerializeField]
    private PlayerController _player;

    private float _endRadius;
    public void InputPlayer(SwipeData data)
    {
        switch(data.Direction)
        {
            case SwipeDirection.Left:
                if (_player._pathId > 0 && _player._canMove)
                {
                    _player.transform.position = _player.transform.position + _player.transform.TransformDirection(Vector3.left - _offset);
                    Camera.main.transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(Vector3.right + _offset);
                    _player._pathId--;
                }
                break;
            case SwipeDirection.Right:
                if (_player._pathId < 2 && _player._canMove)
                {
                    _player.transform.position = _player.transform.position + _player.transform.TransformDirection(Vector3.right + _offset);
                    Camera.main.transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(Vector3.left - _offset);
                    _player._pathId++;
                }
                break;
            case SwipeDirection.Up:
                _player.Jump();
                break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SwipeData _data = new SwipeData();
            _data.Direction = SwipeDirection.Left;
            InputPlayer(_data);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SwipeData _data = new SwipeData();
            _data.Direction = SwipeDirection.Right;
            InputPlayer(_data);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwipeData _data = new SwipeData();
            _data.Direction = SwipeDirection.Up;
            InputPlayer(_data);
        }
    }
}
