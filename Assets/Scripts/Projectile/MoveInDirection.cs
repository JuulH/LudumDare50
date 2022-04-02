using System;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool moveRight;
    [SerializeField] private bool moveUp;
    private Vector3 _velocity;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector3 moveDir = transform.right;
        if (moveRight)
        {
            moveDir = transform.right;
        } else if (moveUp)
        {
            moveDir = transform.up;
        }
        _velocity = moveDir * speed;
        _rb.velocity = _velocity;
    }

    void Update()
    {
        _rb.velocity = _velocity;
    }
}
