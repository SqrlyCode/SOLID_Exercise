using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBehaviour : MonoBehaviour, IShapeBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileRespawnCooldown = 1;

    private float[] _projectileRespawnTimer;
    private List<SquareProjectile> _projectiles;//0:top, 1:right, 2:bot, 3:left 
    private ShapeMotor _shapeMotor;
    
    public Vector2 _Position => transform.position;

    private void Awake()
    {
        _projectileRespawnTimer = new float[4];
        _projectiles = new List<SquareProjectile> {null,null,null,null};
        _projectiles.Capacity = 4;
        _shapeMotor = GetComponent<ShapeMotor>();
        _shapeMotor.died += ShapeMotor_Died;
        for (int i = 0; i < 4; i++)
        {
            SpawnProjectile(i);
        }
    
    }

    private void Update()
    {
        UpdateProjectilePositions();
        
        //Respawn Projectiles
        for (int i = 0; i < _projectiles.Count; i++)
        {
            if (_projectiles[i] == null)
            {
                _projectileRespawnTimer[i] += Time.deltaTime;
                if (_projectileRespawnTimer[i] >= _projectileRespawnCooldown)
                {
                    SpawnProjectile(i);
                    _projectileRespawnTimer[i] = 0;
                }
            }
        }
    }

    public void ShootAtPosition(Vector2 position)
    {
        foreach (var projectile in _projectiles)
        {
            projectile?.SetDestination(position);
        }
    }

    private void SpawnProjectile(int index)
    {
        _projectiles[index] = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<SquareProjectile>();
        _projectiles[index].Init(this);
        _projectiles[index].destroyed += SquareProjectile_Destroyed;
    }
    
    private void UpdateProjectilePositions()
    {
        _projectiles[0]?.UpdateOriginPosition(transform.position + Vector3.up);
        _projectiles[1]?.UpdateOriginPosition(transform.position + Vector3.right);
        _projectiles[2]?.UpdateOriginPosition(transform.position + Vector3.down);
        _projectiles[3]?.UpdateOriginPosition(transform.position + Vector3.left);
    }
    
    private void SquareProjectile_Destroyed(SquareProjectile projectile)
    {
        int index = _projectiles.IndexOf(projectile);
        _projectiles[index] = null;
    }
    
    private void ShapeMotor_Died()
    {
        for (int i=0; i< _projectiles.Count; i++)
        {
            if(_projectiles[i] != null)
                _projectiles[i].Die();
        }
    }
}
