using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp_EX : CharaBaseState
{
    private CharaStateManager charaMachine;
    private float time;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("StandUp");
        time = machine.player.anima.GetCurrentAnimatorStateInfo(0).length;
        charaMachine = machine;

        machine.player.hasAirdodged = false;
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        time = machine.ring.alarm[2] = machine.ring.Alarm(time, SwitchState);
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

    private void SwitchState()
    {
        charaMachine.SwitchState(charaMachine.stand);
    }
}
