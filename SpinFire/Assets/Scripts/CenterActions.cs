using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CenterActions : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
   public BoostBar BB;
   public Player _player;
   public Sprite[] options;
   public Image[] arrowRenderers;

   private void Awake()
   {
      BB = FindObjectOfType<BoostBar>();
      _player = FindObjectOfType<Player>();
   }

   public void OnPointerDown(PointerEventData eventData)
   {
     BB.isClicked = true;
   }

   public void OnPointerUp(PointerEventData eventData)
   {
       BB.isClicked = false;
      _player.isBoosting = false;
   }
}
