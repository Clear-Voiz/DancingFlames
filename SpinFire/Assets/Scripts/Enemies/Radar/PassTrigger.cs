using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTrigger : MonoBehaviour
{
    private RadStateMachine _rad;

    private void Awake()
    {
        _rad = GetComponentInParent<RadStateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _rad.currentState.OnTriggerEnter2D(_rad,other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _rad.currentState.OnTriggerExit2D(_rad,other);
    }
}
