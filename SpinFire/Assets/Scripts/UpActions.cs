using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpActions : MonoBehaviour,IPointerDownHandler
{
    private Player _player;
    public CharaStateManager CSM;
    public event Action<CharaStateManager> OnPressedUp;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        CSM = FindObjectOfType<CharaStateManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPressedUp?.Invoke(CSM);
        /*
        if (_player.isGrounded && _player.isBoosting==false)
        {
            _player._rig.AddForce(Vector2.up * (2f+_player.speed), ForceMode2D.Impulse);
            _player.isGrounded = false;
           // _player.ChangeAniState(Player.AniStates.Jump);
        }
        else if (_player.isGrounded && _player.isBoosting)
        {
            _player.ChangeAniState(Player.AniStates.UpKick);
            _player._rig.gravityScale = 1;
            _player._rig.AddForce(Vector2.up * (6f), ForceMode2D.Impulse);
            _player.isGrounded = false;
            _player.isBoosting = false;
            Instantiate(_player.PS, transform.position, Quaternion.identity); //Quaternion.Euler(0f, 0f, -122f)
        }
        */
    }

    
}
