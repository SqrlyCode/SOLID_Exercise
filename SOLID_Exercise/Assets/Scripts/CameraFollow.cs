using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    void Update()
    {
        Vector3 newPos = _followTarget.position;
        newPos.z = transform.position.z;

        transform.position = newPos;
    }
}
