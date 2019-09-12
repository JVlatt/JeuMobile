using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _rotateAmount = 1.0f;

    [SerializeField]
    private float _jumpHeight = 5.0f;

    private Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + _rotateAmount * Time.deltaTime, transform.rotation.eulerAngles.z);
        _rb.velocity = transform.TransformDirection(new Vector3(0, 0, _speed));
    }
    public void Jump()
    {
        if(Mathf.Abs(_rb.velocity.y)<0.1f)
        {
            _rb.AddForce(new Vector3(0, _jumpHeight, 0));
        }
    }
}
