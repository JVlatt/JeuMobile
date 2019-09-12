using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _radius = 1.0f;
    [SerializeField]
    private float _distance = 1.0f;


    private void Update()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Cos(_player._rotateAngle - _distance) * _radius;
        position.z = Mathf.Sin(_player._rotateAngle - _distance) * _radius;

        transform.position = position;
        transform.LookAt(_target);
    }

}
