using System;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 _velocity;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _velocity = transform.up * speed;
        _rb.velocity = _velocity;
    }

    void Update()
    {
        _rb.velocity = _velocity;
    }
}
