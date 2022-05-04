using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;

    [Header("Character Stats")]
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private TextMeshProUGUI _attackRangeText;

    /// <summary>
    /// Sets the coins UI element value
    /// </summary>
    public void SetCoins(int amount)
    {
        _coinsText.SetText(amount.ToString());
    }

    /// <summary>
    /// Sets the stats UI elements values
    /// </summary>
    public void SetCharacterStats(Character character)
    {
        _healthText.SetText(character.Health.ToString());
        _damageText.SetText(character.Damage.ToString());
        _attackSpeedText.SetText(character.AttackSpeed.ToString());
        _attackRangeText.SetText(character.AttackRange.ToString());
    }
}
