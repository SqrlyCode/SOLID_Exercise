using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionUiAboveShape : MonoBehaviour
{


    private void Awake()
    {
        
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}