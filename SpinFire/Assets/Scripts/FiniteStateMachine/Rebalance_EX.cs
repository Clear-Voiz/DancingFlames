﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebalance_EX : CharaBaseState
{
    private float _exitTime;
    private CharaStateManager _charaMachine;
    public override void EnterState(CharaStateManager machine)
    {
        _exitTime = 0.4f;
        _charaMachine = machine;
        machine.player.anima.Play("Rebalance");
        
        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        _exitTime = machine.ring.alarm[7] = machine.ring.Alarm(_exitTime, Suspended);
    }

    public override void ExitState(CharaStateManager machine)
    {
        
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.fall);
        }
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        
    }

    private void Suspended()
    {
        _charaMachine.SwitchState(_charaMachine.suspended);
    }
}
