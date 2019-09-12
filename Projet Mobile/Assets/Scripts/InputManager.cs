using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _startPlayerPosition;
    private Vector3 _endPlayerPosition;

    
    [SerializeField]
    private PlayerController _player;

    private float _endRadius;
    public void InputPlayer(SwipeData data)
    {
        switch(data.Direction)
        {
            case SwipeDirection.Left:
                if (_player._radius > _player._minRadius)
                    _player.MoveLeft();
                break;
            case SwipeDirection.Right:
                if (_player._radius < _player._maxRadius)
                    _player.MoveRight();
                break;
            case SwipeDirection.Up:
                _player.Jump();
                break;
        }
    }
}
