using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    public float _radius = 4f;
    public float _minRadius = 3f;
    public float _maxRadius = 5f;

    public float _rotateAngle = 0f;

    [SerializeField]
    private float _jumpHeight = 5.0f;

    private Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rotateAngle += Time.deltaTime * _speed;

        Vector3 position = transform.position;
        position.x = Mathf.Cos(_rotateAngle) * _radius;
        position.z = Mathf.Sin(_rotateAngle) * _radius;

        transform.position = position;
        transform.eulerAngles = new Vector3(0, - _rotateAngle * Mathf.Rad2Deg, 0);
    }
    public void Jump()
    {
        if(Mathf.Abs(_rb.velocity.y)<0.1f)
        {
            _rb.AddForce(new Vector3(0, _jumpHeight, 0));
        }
    }

    public void MoveLeft()
    {
        _radius -= 1;
    }
    public void MoveRight()
    {
        _radius += 1;
    }
}
