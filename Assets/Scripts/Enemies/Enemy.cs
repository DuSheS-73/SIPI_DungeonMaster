using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

/// <summary>
/// Implements base logic for each enemy 
/// </summary>
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public event EventHandler OnDie;

    [Header("Stats")]
    [SerializeField] private int _health = 3;
    [SerializeField] private int _damage = 5;

    [Header("Prefabs")]
    [SerializeField] private Coin _coinPrefab;

    private Animator _animator;
    private Rigidbody _rb;
    private NavMeshAgent _agent;

    public Rigidbody RigidBody => _rb;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _agent = gameObject.GetComponent<NavMeshAgent>();

        StartCoroutine( SetDestinationCoroutine() );
    }

    private IEnumerator SetDestinationCoroutine()
    {
        yield return new WaitForSecondsRealtime(.75f);

        while (true)
        {
            _agent.SetDestination(Player.Instance.transform.position);
            yield return new WaitForSecondsRealtime(.2f);
        }
    }

    private async Task OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //await Player.Instance.TakeDamageAsync(_damage);
        }
    }


    /// <summary>
    /// Reduces enemy's health by the specified amount.
    /// If after reducing enemy's health less or equals 0, triggers the Die event  
    /// </summary>
    /// <param name="amount">Amount of HP to reduce</param>
    public void TakeDamage(int amount)
    {
        _health -= amount;

        if (_health <= 0)
            Die();
    }


    /// <summary>
    /// Triggers the Die event.
    /// Spawns coins.
    /// Destroyes (removes from scene) the object
    /// </summary>
    private void Die()
    {
        if (OnDie != null)
            OnDie(this, EventArgs.Empty);

        int coinsToSpawn = Random.Range(3, 5);
        for (int i = 0; i < coinsToSpawn; i++)
        {
            Coin coinClone = Instantiate(_coinPrefab, transform.position, Quaternion.identity);

            Rigidbody coinRb = coinClone.GetComponent<Rigidbody>();

            Vector3 force = Random.insideUnitSphere;
            force.y = Mathf.Abs(force.y);

            coinRb.AddForce(force, ForceMode.Acceleration);
        }

        Destroy(gameObject);
    }
}
