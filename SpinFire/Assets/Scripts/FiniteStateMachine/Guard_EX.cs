using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_EX : CharaBaseState
{
    private float secs = 0.6f;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.invenci = true;
        machine.player.anima.Play("Guard");
        secs = 0.5f;
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        secs = machine.ring.alarm[9] = machine.ring.Alarm(secs,Next,machine);
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player.invenci = false;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        
    }

    private void Next(CharaStateManager machine)
    {
        machine.SwitchState(machine.stand);
    }
}
