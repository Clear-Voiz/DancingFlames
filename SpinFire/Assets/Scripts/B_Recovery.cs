using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Recovery : MonoBehaviour
{
    private Player _player;
    private float RecoveryValue;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        RecoveryValue = 20f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_player.fuel < _player.maxFuel-RecoveryValue)
            {
                _player.fuel += RecoveryValue;
            }
            else
            {
                _player.fuel = _player.maxFuel;
            }
            Destroy(gameObject);
        }
    }
}
