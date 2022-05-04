using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayerMovement), typeof(PlayerAttack))]
public class PlayerInputHandler : MonoBehaviour
{
    private Player _player;
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;

    private Joystick _moveJoystick;
    private Joystick _attackJoystick;

    private void Start()
    {
        _player = gameObject.GetComponent<Player>();
        _playerMovement = gameObject.GetComponent<PlayerMovement>();
        _playerAttack = gameObject.GetComponent<PlayerAttack>();

        _moveJoystick = _player.Ui.MoveJoystick;
        _attackJoystick = _player.Ui.AttackJoystick;
    }

    private void Update()
    {
        if (_moveJoystick != null)
            HandleMoveInput();

        if (_attackJoystick != null) 
            HandleAttackInput();
    }

    /// <summary>
    /// Handles the movement ui joystick input
    /// </summary>
    private void HandleMoveInput()
    {
        var move = new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical);
         _playerMovement.Move(move);
    }

    /// <summary>
    /// Handles the attack ui joystick input
    /// </summary>
    private void HandleAttackInput()
    {
        float x = _attackJoystick.Horizontal;
        float y = _attackJoystick.Vertical;

        var rotation = new Vector2(x, -y);

        if (rotation != Vector2.zero)
        {
            _playerMovement.Rotate(rotation, 90f);
            _playerAttack.Attack();
        }
    }
}
