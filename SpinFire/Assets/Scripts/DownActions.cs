using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DownActions : MonoBehaviour,IPointerDownHandler
{
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_player.isBoosting)
        {
            if (!_player.isGrounded)
            {
                _player.ChangeAniState(Player.AniStates.Dive);
                _player.speed = 0;
                _player._rig.velocity = Vector2.zero;
                _player._rig.AddForce(Vector2.down * 6f, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (!_player.isGrounded)
            {
                _player.ChangeAniState(Player.AniStates.AerialSweep);
                _player.speed = 0;
                Rigidbody2D _rig = _player.GetComponent<Rigidbody2D>();
                _rig.velocity = Vector2.zero;
                _rig.AddForce(Vector2.down * 6f, ForceMode2D.Impulse);
            }
            else
            {
                _player.ChangeAniState(Player.AniStates.Dash);
            }

        }
    }
}
