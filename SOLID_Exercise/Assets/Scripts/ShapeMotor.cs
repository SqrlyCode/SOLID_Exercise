using UnityEngine;
using DG.Tweening;

public class ShapeMotor : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private bool _lookAtCursor;
    
    [SerializeField] private ShapeHpDisplay _healthBar;
    
    
    private int _health;
    public Vector2 _MoveInput { get; set; }

    public float _Health
    {
        get => _health;
        set
        {
            _health = (int)Mathf.Clamp(value, 0, 100);
            if (_health <= 0)
            {
                Die();
            }
            else
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
        _rb.velocity = _MoveInput * (Time.deltaTime * _moveSpeed);
    }

    public void LookAtPosition(Vector2 position)
    {
        if (_lookAtCursor)
        {
            Vector2 dir = position - _rb.position;
            transform.up = dir;
        }
    }

    private void Die()
    {
        transform.DOScale(Vector3.zero, 0.1f)
            .SetEase(Ease.OutSine)
            .OnComplete(() => Destroy(gameObject));
    }
}