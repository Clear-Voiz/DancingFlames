using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UIElements;

public class Referencer : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
   public BoostBar BB;
   public Player _player;
   public Movement movement;

   private void Awake()
   {
      BB = FindObjectOfType<BoostBar>();
      _player = FindObjectOfType<Player>();
      movement = FindObjectOfType<Movement>();
   }

   public void OnPointerDown(PointerEventData eventData)
   {
      BB.isClicked = true;
   }

   public void OnPointerUp(PointerEventData eventData)
   {
      BB.isClicked = false;
      _player.isBoosting = false;
      _player._rig.gravityScale = 1;
      _player.isWallSliding = false;
   }
}
