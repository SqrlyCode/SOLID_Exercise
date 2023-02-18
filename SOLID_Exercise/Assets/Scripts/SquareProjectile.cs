using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class SquareProjectile : MonoBehaviour, IProjectile
{
    public SquareProjectileState _State { get; private set; }
    public int _Damage => _damage;
    public IShapeBehaviour _Creator { get; private set; }
    public event Action<SquareProjectile> destroyed;
        
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _moveSpeed = 8;
    [SerializeField] private float _rotationSpeed = 180;
    
    
    private Vector2 _destination;
    private Vector2 _originPosition;
    
    public enum SquareProjectileState
    {
        Idle,
        FlyingTowardsDestination,
        Returning
    }


    public void Init(IShapeBehaviour creator)
    {
        _State = SquareProjectileState.Idle;
        _Creator = creator;
        
        Vector3 initScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(initScale, 0.2f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, -_rotationSpeed * Time.deltaTime);
        
        switch (_State)
        {
            case SquareProjectileState.Idle:
                UpdateIdle();
                break;
            case SquareProjectileState.FlyingTowardsDestination:
                UpdateFlyingTowardsDestination();
                break;
            case SquareProjectileState.Returning:
                UpdateReturning();
                break;
        }
    }

    private void UpdateIdle()
    {
        transform.position = _originPosition;
    }

    private void UpdateFlyingTowardsDestination()
    {
    }

    private void UpdateReturning()
    {
        Vector2 dir = (_originPosition - (Vector2)transform.position).normalized;
        // transform.position = (Vector2)transform.position + dir * (Time.deltaTime * _moveSpeed);
        float t = _moveSpeed / Vector2.Distance(transform.position, _originPosition);//For constant movespeed
        transform.position = Vector3.Lerp(transform.position, _originPosition, 0.1f + Time.deltaTime * t);
        if (Vector2.Distance(transform.position, _originPosition) < 0.1f)
        {
            _State = SquareProjectileState.Idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        ShapeMotor shapeMotor = col.GetComponent<ShapeMotor>();
        IShapeBehaviour shapeBehaviour = col.GetComponent<IShapeBehaviour>();
        IProjectile projectile = col.GetComponent<IProjectile>();

        if (projectile!= null && projectile.GetType() != this.GetType())
        {
            Die();
        }
        //If ShapeMotor is on Object and Shapebehavior is something else than the Type that fired. i.e. triangles can not hit other triangles
        else if (shapeBehaviour != null && shapeBehaviour.GetType() !=  _Creator.GetType())
        {
            shapeMotor._Health -= _Damage;
            Die();
        }
        else if (col.CompareTag("Wall"))
            Die();
    }

    public void SetDestination(Vector3 destination)
    {
        if (_State == SquareProjectileState.Idle)
        {
            _destination = destination;
            _State = SquareProjectileState.FlyingTowardsDestination;
            
            float moveDuration = Vector2.Distance(transform.position, _destination) / _moveSpeed;
            transform.DOMove(_destination, moveDuration)
                // .SetSpeedBased(true)
                .SetEase(Ease.OutSine)
                .OnComplete(()=>_State = SquareProjectileState.Returning);
        }
    }

    public void UpdateOriginPosition(Vector2 originPos)
    {
        _originPosition = originPos;
    }

    public void Die()
    {
        DOTween.Kill(transform);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        destroyed?.Invoke(this);
    }
}
