using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightActions : MonoBehaviour,IPointerDownHandler
{
    private CharaStateManager CSM;
    public event Action<CharaStateManager> OnPressedRight;

    private void Awake()
    {
        CSM = FindObjectOfType<CharaStateManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPressedRight?.Invoke(CSM);
    }
}