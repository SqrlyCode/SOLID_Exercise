using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShapeBehaviour
{
    Vector2 _Position { get; }
    void ShootAtPosition(Vector2 position);
}
