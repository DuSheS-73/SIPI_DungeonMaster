using System;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance => _instance;

    [Header("UI")]
    [SerializeField] private UI _ui;

    [Header("Combat")]
    [SerializeField] private float _knockbackForce = 30f;
    [SerializeField] private float _timeInvisibleAfterTookDamage = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;

    [Space]
    [SerializeField] private AudioClip _pickUpCoinSfx;
    [SerializeField] private AudioClip _footstepSfx;
    [SerializeField] private AudioClip _wooshSfx;

    private float _lastTimeDamaged;

    private int _earnedCoins = 0;

    public int Health { get; private set; }
    public int Damage { get; private set; }
    public float KnockbackForce { get; private set; }

    public UI Ui => _ui;

    private void Start()
    {
        _instance = this;
    }

    public void Init(Character character)
    {
        Instantiate(character.Prefab, transform);

        Health = character.Health;
        Damage = character.Damage;
        KnockbackForce = _knockbackForce;

        _ui.SetPlayerMaxHealth(Health);
        _ui.SetPlayerHealth(Health);
        _ui.SetCoinsText(_earnedCoins);
    }

    /// <summary>
    /// Reduces player's health by the specified amount.
    /// If after reducing player's health less or equals 0, triggers the Die event  
    /// </summary>
    /// <param name="amount">Amount of HP to reduce</param>
    public async Task TakeDamageAsync(int amount)
    {
        if (Time.time - _lastTimeDamaged >= _timeInvisibleAfterTookDamage)
        {
            Health -= amount;

            _ui.SetPlayerHealth(Health);

            _lastTimeDamaged = Time.time;

            if (Health <= 0)
            {
                await DieAsync();
            }
        }
    }

    /// <summary>
    /// Adds coins to player
    /// </summary>
    /// <param name="amount">Amount of coins to add</param>
    public void AddCoins(int amount)
    {
        _earnedCoins += amount;

        _ui.SetCoinsText(_earnedCoins);
    }

    

    private async Task DieAsync()
    {
        //GameManager.PauseGame();

        var assetLoader = new AssetsLoader();
        await assetLoader.LoadAsync<GameObject>("GameLostScreen");
    }

    public void PlayPickUpCoinSfx()
    {
        _audioSource.PlayOneShot(_pickUpCoinSfx);
    }

    public void PlayFootstepSfx()
    {
        _audioSource.PlayOneShot(_footstepSfx);
    }

    public void PlayWooshSfx()
    {
        _audioSource.PlayOneShot(_wooshSfx);
    }
}
