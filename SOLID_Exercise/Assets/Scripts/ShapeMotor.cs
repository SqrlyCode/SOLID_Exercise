using System;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Serialization;

public class ShapeMotor : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private int _maxHealth = 100;
    private int _health;
    [SerializeField] private HealthBar _healthBar;

    public Vector2 _MoveInput { get; set; }

    public float _Health
    {
        get => _health;
        set
        {
            _health = (int)Mathf.Clamp(value, 0, 100);
            _healthBar._desiredValue = value / (float)_maxHealth;
        }
    }

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _Health = _maxHealth;
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