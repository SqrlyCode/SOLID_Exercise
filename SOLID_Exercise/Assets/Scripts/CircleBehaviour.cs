using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour, IShapeBehaviour
{
    public Vector2 _Position => transform.position;
    public void ShootAtPosition(Vector2 position)
    {
        
    }
}
