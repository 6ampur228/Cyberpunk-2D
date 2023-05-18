using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _storageSpawnPoints;
    [SerializeField] private Enemy _template;
    [SerializeField] private AudioSource _dieSound;
    [SerializeField ]private Player _target;

    private Transform[] _spawnPoints;

    private void Start()
    {
        _spawnPoints = new Transform[_storageSpawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = _storageSpawnPoints.GetChild(i);

            Enemy spawnedEnemy = Instantiate(_template, _spawnPoints[i].position, Quaternion.identity);

            spawnedEnemy.SetDieSound(_dieSound);
            spawnedEnemy.SetTarget(_target);
        }
    }
}
