using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides logic for character upgrade system
/// </summary>
public class UpgradeSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GeneralPlayerData _player;
    [SerializeField] private CharacterSelector _characterSelector;

    [Header("UI")]
    [SerializeField] private TitleScreenUI _ui;
    [SerializeField] private UpgradeMenu _upgradeMenu;

    private int _statTypeIndex;
    private int _upgradePrice;

    /// <summary>
    /// Shows the upgrade menu
    /// </summary>
    public void ShowUpgradeWindowButtonClick(int statTypeIndex)
    {
        _statTypeIndex = statTypeIndex;

        string statName = "NULL";

        switch (_statTypeIndex)
        {
            case 0:
                statName = "Health";
                _upgradePrice = _characterSelector.SelectedCharacter.HealthUpgardePrice;
                break;

            case 1:
                statName = "Damage";
                _upgradePrice = _characterSelector.SelectedCharacter.DamageUpgardePrice;
                break;

            case 2:
                statName = "Attack Speed";
                _upgradePrice = _characterSelector.SelectedCharacter.AttackSpeedUpgardePrice;
                break;

            case 3:
                statName = "Attack Range";
                _upgradePrice = _characterSelector.SelectedCharacter.AttackRangeUpgardePrice;
                break;
        }

        _upgradeMenu.ReplaceTextVariables(statName, _upgradePrice);
        _upgradeMenu.SetUpgaradeButtonInteractable(_upgradePrice <= _player.Coins);

        _upgradeMenu.gameObject.SetActive(true);
    }

    /// <summary>
    /// Upgrade button click event.
    /// Reduces player coins and increments the stat level, if player has enougth coins.
    /// </summary>
    public void UpgardeButtonClick()
    {
        if (_upgradePrice <= _player.Coins)
        {
            _player.AddCoins(-_upgradePrice);

            IncrementStatUpgradeLevel();

            var saveSystem = new SaveSystem();
            saveSystem.SaveCharacter(_characterSelector.SelectedCharacter);

            _ui.SetCharacterStats(_characterSelector.SelectedCharacter);
        }
    }

    private void IncrementStatUpgradeLevel()
    {
        Character selectedCharacter = _characterSelector.SelectedCharacter;
        switch (_statTypeIndex)
        {
            case 0:
                selectedCharacter.SetHealthUpgradeLevel(selectedCharacter.HealthUpgradeLevel + 1);
                break;

            case 1:
                selectedCharacter.SetDamageUpgradeLevel(selectedCharacter.DamageUpgradeLevel + 1);
                break;

            case 2:
                selectedCharacter.SetAttackSpeedUpgradeLevel(selectedCharacter.AttackSpeedUpgradeLevel + 1);
                break;

            case 3:
                selectedCharacter.SetAttackRangeUpgradeLevel(selectedCharacter.AttackRangeUpgradeLevel + 1);
                break;
        }
    }
}
