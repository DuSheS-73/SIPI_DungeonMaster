using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Coin : Pickupable
{
    [SerializeField] private int _value = 10;

    protected override void OnPickUp(Player player)
    {
        player.AddCoins(_value);
        player.PlayPickUpCoinSfx();        

        Destroy(gameObject);
    }
}
