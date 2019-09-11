using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _startPlayerPosition;
    private Vector3 _endPlayerPosition;

    [SerializeField]
    public Transform _leftPosition;
    [SerializeField]
    public Transform _rightPosition;
    [SerializeField]
    private PlayerController _player;

    public static InputManager _inputMngr;

    [SerializeField]
    private float _movementOffset = 1.5f;

    [SerializeField]
    private float _movementTime;
    [SerializeField]
    private float _movementduration = 0.1f;
    public void InputPlayer(SwipeData data)
    {
        switch(data.Direction)
        {
            case SwipeDirection.Left:
                if (_player.transform.position.x > _leftPosition.position.x)
                    StartCoroutine(MovePlayer("Left"));
                break;
            case SwipeDirection.Right:
                if (_player.transform.position.x < _rightPosition.position.x)
                    StartCoroutine(MovePlayer("Right"));
                break;
            case SwipeDirection.Up:
                _player.Jump();
                break;
        }
    }
    private IEnumerator MovePlayer(string direction)
    {
        switch (direction)
        {
            case "Left":
                _movementTime = 0f;
                _startPlayerPosition = _player.transform.position;
                _endPlayerPosition = new Vector3(_startPlayerPosition.x - _movementOffset, _player.transform.position.y, _player.transform.position.z);

                while (_movementTime < _movementduration)
                {
                    _movementTime += Time.deltaTime;
                    _player.transform.position = Vector3.Lerp(_startPlayerPosition, _endPlayerPosition, _movementTime / _movementduration);
                    yield return null;
                }
                break;
            case "Right":
                _movementTime = 0f;
                _startPlayerPosition = _player.transform.position;
                _endPlayerPosition = new Vector3(_startPlayerPosition.x + _movementOffset, _player.transform.position.y, _player.transform.position.z);

                while (_movementTime < _movementduration)
                {
                    _movementTime += Time.deltaTime;
                    _player.transform.position = Vector3.Lerp(_startPlayerPosition, _endPlayerPosition, _movementTime / _movementduration);
                    yield return null;
                }
                break;
        }
    }
}
