using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject _light;

    // Костыль шо пиздец. Но Init для начальной комнаты не вызывается,
    // А Start не всегда успевает отработать перед соединением комнат.
    // Возможные решения:
    // 1) Костылить вызов Init для начальной комнаты (ммм один костыль меняем другим... как вкусно...)
    // 2) Делать задержку перед соединением комнат, чтобы успел отработать Start для каждой комнаты
    [Tooltip("[0] - Top door\n[1] - Right door\n[2] - Bottom door\n[3] - Left door")]
    [SerializeField] private List<GameObject> _doors;

    [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;

    private List<Enemy> _enemies;
    private bool _clear;

    public Vector2Int GridPosition { get; private set; }

    public void Init(Vector2Int gridPosition)
    {
        GridPosition = gridPosition;
    }

    /// <summary>
    /// Disables/Hides the door
    /// </summary>
    /// <param name="doorType">Door to disable (top, right, bottom, left)</param>
    public void DisableDoor(DoorTypes doorType)
    {
        switch (doorType)
        {
            case DoorTypes.Top:
                _doors[0].SetActive(true);
                _doors[0] = null;
                break;

            case DoorTypes.Right:
                _doors[1].SetActive(true);
                _doors[1] = null;
                break;

            case DoorTypes.Bottom:
                _doors[2].SetActive(true);
                _doors[2] = null;
                break;

            case DoorTypes.Left:
                _doors[3].SetActive(true);
                _doors[3] = null;
                break;
        }
    }

    /// <summary>
    /// Invokes when player entered the room
    /// </summary>
    public void HandlePlayerEntered()
    {
        if (!_clear && _enemySpawnPoints != null && _enemySpawnPoints.Length > 0)
        {
            _light.SetActive(true);

            _enemies = new List<Enemy>();

            // close doors
            for (int i = 0; i < _doors.Count; i++)
            {
                if (_doors[i] == null)
                    continue;

                _doors[i].SetActive(true);
            }

            // spawn enemies
            for (int i = 0; i < _enemySpawnPoints.Length; i++)
            {
                Enemy enemy = _enemySpawnPoints[i].SpawnEnemy();

                if (enemy == null)
                    continue;

                _enemies.Add(enemy);
                enemy.OnDie += OnEnemyDie;
            }
        }
    }

    private void OnEnemyDie(object sender, System.EventArgs args)
    {
        Enemy enemy = sender as Enemy;
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
        {
            _clear = true;

            // open the doors
            for (int i = 0; i < _doors.Count; i++)
            {
                if (_doors[i] == null)
                    continue;

                _doors[i].SetActive(false);
            }
        }
    }
}
