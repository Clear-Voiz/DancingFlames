using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Fall");
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (machine.player.wallColl)
        {
            machine.player._rig.velocity = new Vector2(0f, machine.player._rig.velocity.y);
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player.isGrounded) machine.SwitchState(machine.standUp);
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
}
