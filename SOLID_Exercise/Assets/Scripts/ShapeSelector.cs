using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSelector : MonoBehaviour
{
    public ShapeMotor _shapeMotorUnderMouse { get; private set; }
    private Color lastColor;
    private ShapeMotor _shapeMotorUnderMouseLastFrame;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.Raycast(mousePos, Vector2.zero).collider;
        if (col == null)
            _shapeMotorUnderMouse = null;
        else
            _shapeMotorUnderMouse = col.GetComponent<ShapeMotor>();
        
        //When selection changes
        if (_shapeMotorUnderMouse != _shapeMotorUnderMouseLastFrame)
        {
            //When moving on to other shape or nothing
            if (_shapeMotorUnderMouseLastFrame != null || _shapeMotorUnderMouse == null)
            {
                SpriteRenderer rendererLastFrame = _shapeMotorUnderMouseLastFrame.GetComponent<SpriteRenderer>();
                rendererLastFrame.color = lastColor;
            }

            if (_shapeMotorUnderMouse != null)
            {
                SpriteRenderer renderer = _shapeMotorUnderMouse.GetComponent<SpriteRenderer>();
                lastColor = renderer.color;
                renderer.color = Color.white;
            }
        }


        _shapeMotorUnderMouseLastFrame = _shapeMotorUnderMouse;
    }
}