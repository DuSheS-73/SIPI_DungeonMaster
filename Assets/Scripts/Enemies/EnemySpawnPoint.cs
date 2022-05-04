using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides logic for points, where can spawn enemies 
/// </summary>
public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    /// <summary>
    /// If enemy prefab is not null, instantiates an enemy 
    /// </summary>
    /// <returns>Object type of Enemy</returns>
    public Enemy SpawnEnemy()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is null");
            return null;
        }

        return Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
}
