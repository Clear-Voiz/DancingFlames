using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadStateMachine : MonoBehaviour
{
    public RadBaseState currentState;

    public InactiveState inactiveState = new InactiveState();
    public ActiveState activeState = new ActiveState();

    public Collider2D _col;
    public Animator anima;

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    void Start()
    {
        currentState = inactiveState;
        currentState.EnterState(this);
    }
    
    void Update()
    {
        currentState.UpdateState(this);
    }


    public void SwitchState(RadBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
