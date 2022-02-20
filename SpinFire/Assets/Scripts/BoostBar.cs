using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    private Image _bar;
    private Movement _mov;
    private Player _player;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        _player.fuel = 40f;
        _player.maxFuel = 40f;
    }

    private void Update()
    {
        Boost();
        Replenish();
        _bar.fillAmount = _player.fuel / _player.maxFuel;
    }

    private void Boost()
    {
        if (Input.GetKey(KeyCode.Space) && _player.fuel >0f && _player.currentState != Player.AniStates.UpKick.ToString())
        {
            _player.fuel -= 0.2f;
        }
        else if (_player.fuel <= 0f)
        {
            _player.fuel = 0f;
            _player.isBoosting = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _player.isBoosting = false;
            _player._rig.gravityScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _player.currentState != Player.AniStates.UpKick.ToString() && _player.fuel>0f)
        {
            _player.isBoosting = true;
            _player._rig.velocity = Vector2.zero;
            _player._rig.gravityScale = 0;
        }
        else if (_player.fuel <= 0f)
        {
            _player.isBoosting = false;
            _player._rig.gravityScale = 1;
        }
    }

    private void Replenish()
    {
        if (_player.fuel < _player.maxFuel && !Input.GetKey(KeyCode.Space))
        {
            _player.fuel += 0.1f;
        }
    }
}
