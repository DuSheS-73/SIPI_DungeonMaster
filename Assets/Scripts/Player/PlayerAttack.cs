using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides the player attack logic
/// </summary>
[RequireComponent(typeof(Animator), typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _attackPoint;

    [Header("Stats")]
    [SerializeField] private float _attackRange = .7f;
    [SerializeField] private float _timeBetweenAttacks = 1f;

    [Header("Sorting Layers")]
    [SerializeField] private LayerMask _enemyLayers;

    private Animator _animator;
    private Player _player;

    private float _lastTimeAttacked;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _player = gameObject.GetComponent<Player>();
    }

    /// <summary>
    /// Provides the player attack logic
    /// </summary>
    public void Attack()
    {
        if (Time.time - _lastTimeAttacked >= _timeBetweenAttacks)
        {
            _player.PlayWooshSfx();

            Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                Enemy enemy = hitEnemies[i].GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(_player.Damage);

                    Vector3 knockback = (enemy.transform.position - transform.position).normalized * _player.KnockbackForce;
                    enemy.RigidBody.AddForce(knockback, ForceMode.Impulse);
                }
            }

            _lastTimeAttacked = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
