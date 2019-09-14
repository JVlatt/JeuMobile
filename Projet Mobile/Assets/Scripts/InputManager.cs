using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _startPlayerPosition;
    private Vector3 _endPlayerPosition;

    
    [SerializeField]
    private MoveOnPath _player;

    private float _endRadius;
    public void InputPlayer(SwipeData data)
    {
        switch(data.Direction)
        {
            case SwipeDirection.Left:
                if (_player._pathId > 0)
                {
                    _player._pathId--;
                    _player.transform.position = _player.transform.position + _player.transform.TransformDirection(Vector3.left);
                }
                break;
            case SwipeDirection.Right:
                if (_player._pathId < 2)
                {
                    _player.transform.position = _player.transform.position + _player.transform.TransformDirection(Vector3.right);
                    _player._pathId++;
                }
                break;
            case SwipeDirection.Up:
                //_player.Jump();
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
    }
}
