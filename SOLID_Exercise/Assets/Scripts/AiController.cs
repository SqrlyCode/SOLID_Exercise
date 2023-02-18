using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiController : MonoBehaviour
{
    private IShapeBehaviour _shapeBehaviour;
    private ShapeMotor _shapeMotor;

    private void Awake()
    {
        _shapeBehaviour = GetComponent<IShapeBehaviour>();
        _shapeMotor = GetComponent<ShapeMotor>();
    }

    private void Update()
    {
        IShapeBehaviour closestShape = GetClosestEnemyShape();
        if (closestShape == null)
            return;
        
        Vector2 dirToClosestShape = (closestShape._Position - (Vector2)transform.position).normalized;

        if(Vector2.Distance(transform.position, closestShape._Position) > 4)
            _shapeMotor._MoveInput = dirToClosestShape;
        else
        {
            _shapeMotor._MoveInput = Vector2.zero;
            _shapeMotor.LookAtPosition(closestShape._Position);
            _shapeBehaviour.ShootAtPosition(closestShape._Position);
        }
    }
    
    IShapeBehaviour GetClosestEnemyShape()
    {
        List<IShapeBehaviour> allShapes = new List<IShapeBehaviour>();

        var x = FindObjectsOfType<MonoBehaviour>().OfType<IShapeBehaviour>();
        foreach (IShapeBehaviour shape in x) {
            if(shape.GetType() != _shapeBehaviour.GetType())
                allShapes.Add (shape);
        }

        if (allShapes.Count == 0)
            return null;

        IShapeBehaviour closestShape = allShapes[0];
        foreach (var shape in allShapes)
        {
            if (Vector2.Distance(transform.position, shape._Position) < Vector2.Distance(transform.position, closestShape._Position))
                closestShape = shape;
        }

        return closestShape;
    }
}
