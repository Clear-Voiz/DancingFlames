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
    public bool isClicked;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Boost();
        Replenish();
        _bar.fillAmount = _player.fuel / _player.maxFuel;
    }

    public void Boost()
    {
        if (isClicked && _player.fuel >0f && _player.currentState != Player.AniStates.UpKick.ToString())
        {
            _player.fuel -= 0.2f;
        }
        else if (_player.fuel <= 0f)
        {
            _player.fuel = 0f;
            _player.isBoosting = false;
            _player.isWallSliding = false;
            
        }

        /*if (!isClicked)
        {
            _player.isBoosting = false;
            _player._rig.gravityScale = 1;
            _player.isWallSliding = false;
        }*/
        if (isClicked && _player.currentState != Player.AniStates.UpKick.ToString() && _player.fuel>0f)
        {
            _player.isBoosting = true;
            _player._rig.velocity = Vector2.zero;
            _player._rig.gravityScale = 0;
        }
        else if (_player.fuel <= 0f)
        {
            _player.isBoosting = false;
            _player._rig.gravityScale = 1;
            _player.isWallSliding = false;
        }
    }

    private void Replenish()
    {
        if (_player.fuel < _player.maxFuel && !isClicked)
        {
            _player.fuel += 0.1f;
        }
    }
}
