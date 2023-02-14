using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour, IShapeBehaviour
{
    [SerializeField] private int _maxAmountOfProjectiles = 8;
    [SerializeField] private float _projectileSpawnCooldown = 0.5f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileRotationSpeed = 30;


    private Vector2[] _projectileSlotLocalPositions;
    private List<CircleProjectile> _projectileSlots;
    private float _projectileSpawnTimer;
    private int _availableProjectiles;
    private float _curProjectileAngleOffset;

    public Vector2 _Position => transform.position;

    private void Awake()
    {
        _projectileSlotLocalPositions = new Vector2[_maxAmountOfProjectiles];
        _projectileSlots = new List<CircleProjectile>();
    }

    private void Update()
    {
        _projectileSpawnTimer += Time.deltaTime;
        CalculateProjectilePositions();
        if (_projectileSpawnTimer > _projectileSpawnCooldown && _availableProjectiles < _maxAmountOfProjectiles)
        {
            _projectileSpawnTimer = 0;
            SpawnProjectile();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _projectileSlots.Count; i++)
        {
            _projectileSlots[i].transform.localPosition = Vector2.Lerp(_projectileSlots[i].transform.localPosition, _projectileSlotLocalPositions[i], 0.2f+Time.deltaTime * 10);
        }
    }


    public void ShootAtPosition(Vector2 position)
    {
        if (_availableProjectiles > 0)
        {
            int closestIndex = -1;
            for (int i = 0; i < _projectileSlots.Count; i++)
            {
                if (_projectileSlots[i] != null)
                {
                    if (closestIndex == -1)
                    {
                        closestIndex = i;
                        continue;
                    }

                    if (Vector2.Distance(position, _projectileSlots[i].transform.position) < Vector2.Distance(position, _projectileSlots[closestIndex].transform.position))
                        closestIndex = i;
                }
            }

            Vector2 projectileDir = position - (Vector2)_projectileSlots[closestIndex].transform.position;
            _projectileSlots[closestIndex]._MoveDir = projectileDir;
            _projectileSlots[closestIndex].destroyed -= CircleParticle_Destroyed;
            _projectileSlots[closestIndex].transform.parent = null;
            _projectileSlots.RemoveAt(closestIndex);
            _availableProjectiles--;
        }
    }

    private void SpawnProjectile()
    {
        _availableProjectiles++;

        GameObject newProjectileGo = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        CircleProjectile newProjectile = newProjectileGo.GetComponent<CircleProjectile>();
        newProjectile.transform.parent = transform;
        newProjectile.Init(this);
        newProjectile.destroyed += CircleParticle_Destroyed;
        _projectileSlots.Add(newProjectile);
    }

    private void CalculateProjectilePositions()
    {
        _curProjectileAngleOffset += Time.deltaTime * _projectileRotationSpeed;
        _curProjectileAngleOffset = _curProjectileAngleOffset % 360;

        float degreeBetweenProjectiles = 360f / _availableProjectiles;
        for (int i = 0; i < _availableProjectiles; i++)
        {
            _projectileSlotLocalPositions[i] = Quaternion.Euler(0, 0, -_curProjectileAngleOffset + degreeBetweenProjectiles * i) * Vector2.up;
        }
    }

    private void CircleParticle_Destroyed(CircleProjectile destroyedProjectile)
    {
        if (_projectileSlots.Contains(destroyedProjectile))
        {
            _projectileSlots.Remove(destroyedProjectile);
            _availableProjectiles--;
        }
    }
}