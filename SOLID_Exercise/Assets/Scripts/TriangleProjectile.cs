using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.U2D.Path;
using UnityEngine;

public class TriangleProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 90; //In Degrees per second
    [SerializeField] private float _targettingRange = 5f;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private int _damage = 10;
    
    public int _Damage => _damage;
    public IShapeBehaviour _Creator { get; private set; }
    

    private Rigidbody2D _rb;

    public void Init(IShapeBehaviour creator)
    {
        _Creator = creator;
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Rotate to target
        IShapeBehaviour closestTarget = GetClosestTarget(_targettingRange, _targetLayerMask);
        if (closestTarget != null)
        {
            Vector2 dirToTarget = closestTarget._Position - (Vector2)transform.position;
            float angleToTarget = Vector2.Angle(transform.up, dirToTarget);
            Vector2 newDir = Vector2.Lerp(transform.up, dirToTarget, Time.deltaTime * _rotationSpeed / angleToTarget);
            transform.up = newDir;
        }

        //Move to target
        _rb.MovePosition((Vector2)transform.position + (Vector2)transform.up * (Time.deltaTime * _moveSpeed));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        ShapeMotor shape = col.GetComponent<ShapeMotor>();
        IShapeBehaviour shapeBehaviour = col.GetComponent<IShapeBehaviour>();
        //If ShapeMotor is on Object and Shapebehavior is something else than the Type that fired. i.e. triangles can not hit other triangles
        if (shape != null && shapeBehaviour.GetType() !=  _Creator.GetType())
        {
            shape._Health -= _damage;
            Destroy(gameObject);
        }
    }

    IShapeBehaviour GetClosestTarget(float range, LayerMask layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
        List<IShapeBehaviour> shapesInRange = new List<IShapeBehaviour>();
        //Filter Shapes in range so that they don't contain null or other triangles
        foreach (Collider2D col in colliders)
        {
            IShapeBehaviour shapeBehaviour = col.GetComponent<IShapeBehaviour>();
            if (shapeBehaviour != null &&
                (shapeBehaviour is TriangleBehaviour == false))
            {
                shapesInRange.Add(shapeBehaviour);
            }
        }

        if (shapesInRange.Count == 0)
            return null;

        IShapeBehaviour closestShape = shapesInRange[0];
        foreach (var shape in shapesInRange)
        {
            if (Vector2.Distance(transform.position, shape._Position) < Vector2.Distance(transform.position, closestShape._Position))
                closestShape = shape;
        }

        return closestShape;
    }

}