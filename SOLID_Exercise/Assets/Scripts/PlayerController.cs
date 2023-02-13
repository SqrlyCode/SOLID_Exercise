using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ShapeMotor _shapeMotor;
    private IShapeBehaviour _shapeBehaviour;

    private void Awake()
    {
        _shapeBehaviour = _shapeMotor.GetComponent<IShapeBehaviour>();
    }

    void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(horInput, vertInput);
        _shapeMotor._MoveInput = moveInput;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _shapeMotor.LookAtPosition(mouseWorldPos);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _shapeBehaviour.ShootAtPosition(mouseWorldPos);
        }
    }
}