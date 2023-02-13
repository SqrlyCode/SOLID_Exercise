using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBehaviour : MonoBehaviour, IShapeBehaviour
{
    [SerializeField] private GameObject _projectileGo;


    private void Awake()
    {
    }

    public void ShootAtPosition(Vector2 position)
    {
        Vector2 shootOrigin = (Vector2)transform.position + (Vector2)transform.up * 0.5f;
        GameObject newProjectileGo = Instantiate(_projectileGo, shootOrigin, Quaternion.identity);
        newProjectileGo.transform.up = position - (Vector2)transform.position;
        newProjectileGo.GetComponent<TriangleProjectile>().Init(this);
    }

    public Vector2 _Position => transform.position;
}
