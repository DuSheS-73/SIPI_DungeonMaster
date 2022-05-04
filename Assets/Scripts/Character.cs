using UnityEngine;

[CreateAssetMenu(fileName="New Character", menuName="Character")]
public class Character : ScriptableObject
{
    [SerializeField] private string _saveFileName = "character";

    [Header("GFX")]
    [SerializeField] private GameObject _prefab;

    [Header("Health")]
    [SerializeField] private int _healthDefaultValue;
    [SerializeField] private int _healthUpgradeLevel;
    [SerializeField] private int _healthDefaultUpgradePrice;

    [Header("Damage")]
    [SerializeField] private int _damageDefaultValue;
    [SerializeField] private int _damageUpgradeLevel;
    [SerializeField] private int _damageDefaultUpgradePrice;

    [Header("Attack Speed")]
    [SerializeField] private int _attackSpeedDefaultValue;
    [SerializeField] private int _attackSpeedUpgradeLevel;
    [SerializeField] private int _attackSpeedDefaultUpgradePrice;

    [Header("Attack Range")]
    [SerializeField] private int _attackRangeDefaultValue;
    [SerializeField] private int _attackRangeUpgradeLevel;
    [SerializeField] private int _attackRangeDefaultUpgradePrice;


    public string SaveFileName => _saveFileName;
    public GameObject Prefab => _prefab;


    public int Health => _healthDefaultValue + ((_healthUpgradeLevel - 1) * 10);
    public int HealthUpgradeLevel => _healthUpgradeLevel;
    public int HealthUpgardePrice => _healthDefaultUpgradePrice * _healthUpgradeLevel;

    public void SetHealthUpgradeLevel(int level)
    {
        _healthUpgradeLevel = level;
    }


    public int Damage => _damageDefaultValue + ((_damageUpgradeLevel - 1) * 10);
    public int DamageUpgradeLevel => _damageUpgradeLevel;
    public int DamageUpgardePrice => _damageDefaultUpgradePrice * _damageUpgradeLevel;

    public void SetDamageUpgradeLevel(int level)
    {
        _damageUpgradeLevel = level;
    }


    public int AttackSpeed => _attackSpeedDefaultValue + ((_attackSpeedUpgradeLevel - 1) * 10);
    public int AttackSpeedUpgradeLevel => _attackSpeedUpgradeLevel;
    public int AttackSpeedUpgardePrice => _attackSpeedDefaultUpgradePrice * _attackSpeedUpgradeLevel;

    public void SetAttackSpeedUpgradeLevel(int level)
    {
        _attackSpeedUpgradeLevel = level;
    }


    public int AttackRange => _attackRangeDefaultValue + ((_attackRangeUpgradeLevel - 1) * 10);
    public int AttackRangeUpgradeLevel => _attackRangeUpgradeLevel;
    public int AttackRangeUpgardePrice => _attackRangeDefaultUpgradePrice * _attackRangeUpgradeLevel;

    public void SetAttackRangeUpgradeLevel(int level)
    {
        _attackRangeUpgradeLevel = level;
    }
}
