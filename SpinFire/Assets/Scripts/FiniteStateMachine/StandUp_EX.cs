using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp_EX : CharaBaseState
{
    private float time;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("StandUp");
        time = 0.4f;

        machine.player.hasAirdodged = false;
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        time = machine.ring.alarm[2] = machine.ring.Alarm(time, SwitchState, machine);
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

    private void SwitchState(CharaStateManager machine)
    {
        machine.SwitchState(machine.stand);
    }
}
