
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _amountOfShapesToSpawn = 10;
    [SerializeField] private Rect _spawnArea;
    [SerializeField] private GameObject[] _shapePrefabs;
    [SerializeField] private bool _spawnOtherShapes = true;
    
    
    void Start()
    {
        if (_spawnOtherShapes)
        {
            for (int i = 0; i < _amountOfShapesToSpawn; i++)
                SpawnShape();
        }
    }

    private void SpawnShape()
    {
        Vector2 rndPos = new Vector2(Random.Range(-_spawnArea.width/2, _spawnArea.width/2), Random.Range(-_spawnArea.height/2, _spawnArea.height/2));
        
        int rndShapeIndex = Random.Range(0, _shapePrefabs.Length);
        Instantiate(_shapePrefabs[rndShapeIndex], rndPos, Quaternion.identity);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0,1,0,0.25f);
        Gizmos.DrawCube(_spawnArea.position, new Vector3(_spawnArea.width, _spawnArea.height, 0.1f));
    }
}
