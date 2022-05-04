using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider _playerHealthBar;
    [SerializeField] private TextMeshProUGUI _healthText; // [currentHealt] / [maxHealth]

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI _coinsText;

    [Header("Controls")]
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _attackJoystick;

    public Joystick MoveJoystick => _moveJoystick;
    public Joystick AttackJoystick => _attackJoystick;

    /// <summary>
    /// Sets the player max health UI element value
    /// </summary>
    public void SetPlayerMaxHealth(int value)
    {
        _playerHealthBar.maxValue = value;
    } 

    /// <summary>
    /// Sets the player current health UI element value
    /// </summary>
    public void SetPlayerHealth(int value)
    {
        _playerHealthBar.value = value;
        _healthText.SetText($"{value} / {_playerHealthBar.maxValue}");
    }

    /// <summary>
    /// Sets the earned coins UI element value
    /// </summary>
    public void SetCoinsText(int value)
    {
        _coinsText.SetText(value.ToString());
    }
}
