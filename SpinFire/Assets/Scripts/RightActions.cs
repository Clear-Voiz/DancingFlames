using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightActions : MonoBehaviour,IPointerDownHandler
{
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
        if (_player.face == -1f)
        {
            _player.face = 1f;
            _player.scaleFact.x = _player.face;
            _player.transform.localScale = _player.scaleFact;
            _player.speed = 0f;
        }
        else
        {
            _player.ChangeAniState(_player.isBoosting?Player.AniStates.PierceKick:Player.AniStates.LenaKick);
            //Invoke("_player.ChangeAniState(Player.AniStates.forwards)",_player.anima.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
