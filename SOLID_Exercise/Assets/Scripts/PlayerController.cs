using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ShapeMotor _controlledShapeMotor;
    private IShapeBehaviour _shapeBehaviour;
    private ShapeSelector _shapeSelector;

    private void Awake()
    {
        _controlledShapeMotor.GetComponent<AiController>().enabled = false;
        _shapeBehaviour = _controlledShapeMotor.GetComponent<IShapeBehaviour>();
        _shapeSelector = GetComponent<ShapeSelector>();
    }

    void Update()
    {
        if (_controlledShapeMotor == null)
            return;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(horInput, vertInput);
        _controlledShapeMotor._MoveInput = moveInput;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _controlledShapeMotor.LookAtPosition(mouseWorldPos);

        //LeftClick
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _shapeBehaviour.ShootAtPosition(mouseWorldPos);
        }

        //RightClick
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (_shapeSelector._shapeMotorUnderMouse != null)
            {
                _controlledShapeMotor.GetComponent<AiController>().enabled = true;
                _controlledShapeMotor = _shapeSelector._shapeMotorUnderMouse;
                _shapeBehaviour = _shapeSelector._shapeMotorUnderMouse.GetComponent<IShapeBehaviour>();
                _shapeSelector._shapeMotorUnderMouse.GetComponent<AiController>().enabled = false;
            }
        }
    }
}