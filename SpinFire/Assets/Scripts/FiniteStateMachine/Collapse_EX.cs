using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Collapse");
        machine.player.invenci = true;
        float time = machine.player.anima.GetCurrentAnimatorStateInfo(0).length;
        machine.StartCoroutine(machine.ring.alarm[1] = machine.ring.Alarm(time, SwitchState, machine));
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (machine.player.wallColl)
        {
            machine.player.speed = 0f;
            machine.player._rig.velocity = Vector2.zero;
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
        
    }

    public override void ExitState(CharaStateManager machine)
    {
        
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        
    }

    private void SwitchState(CharaStateManager machine)
    {
        machine.SwitchState(machine.standUp); //change this to stand up
    }
}
