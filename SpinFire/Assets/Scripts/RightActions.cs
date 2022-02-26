using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightActions : MonoBehaviour,IPointerDownHandler
{
    private Player _player;
    private GameObject KickFX;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        KickFX = Resources.Load("KickFX") as GameObject;
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
            if (_player.isBoosting)
                _player.ChangeAniState(Player.AniStates.PierceKick);
            else
            {
                _player.ChangeAniState(Player.AniStates.LenaKick);
                Invoke("HitboxInstantiate",0.2f);
                
            }
            //Invoke("_player.ChangeAniState(Player.AniStates.forwards)",_player.anima.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void HitboxInstantiate()
    {
        Instantiate(KickFX, _player.transform);
    }
}
