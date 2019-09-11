using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private float _jumpHeight = 5.0f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, 0, _speed * Time.deltaTime));
    }
    public void Jump()
    {
        if(Mathf.Abs(_rb.velocity.y)<0.1f)
        {
            _rb.AddForce(new Vector3(0, _jumpHeight, 0));
        }
    }
}
