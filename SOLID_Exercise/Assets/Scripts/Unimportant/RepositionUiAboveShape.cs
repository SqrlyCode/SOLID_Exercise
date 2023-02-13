using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionUiAboveShape : MonoBehaviour
{
    [SerializeField] private float _yOffset = -0.65f;
    
    
    void LateUpdate()
    {
        transform.position = transform.parent.position + Vector3.up * _yOffset;
        transform.rotation = Quaternion.identity;
    }
}
