using System;
using UnityEngine;

public class ShapeMotor : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private int _health;
    
    
    public Vector2 _MoveInput { get; set; }
    public float _Health => _health;

    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _MoveInput * (Time.deltaTime * _moveSpeed));
    }


    public void LookAtPosition(Vector2 position)
    {
        Vector2 dir = position - _rb.position;
        transform.up = dir;
    }
}