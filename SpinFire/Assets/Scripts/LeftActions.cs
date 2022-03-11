using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftActions : MonoBehaviour,IPointerDownHandler
{
    private CharaStateManager CSM;
    public event Action<CharaStateManager> OnPressedLeft;

    private void Awake()
    {
        CSM = FindObjectOfType<CharaStateManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPressedLeft?.Invoke(CSM);
    }
    
}