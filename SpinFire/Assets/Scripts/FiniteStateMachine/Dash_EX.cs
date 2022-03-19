using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_EX : CharaBaseState
{
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Dash");
        var aniEnd = machine.player.anima.GetCurrentAnimatorStateInfo(0);
        secs = aniEnd.length*2f;
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (!machine.player.wallColl)
        {
            machine.player._rig.velocity = new Vector2((machine.player.speed + machine.player.accel) * machine.player.face, machine.player._rig.velocity.y);
        }
        else
        {
            machine.player._rig.velocity = new Vector2(0f, machine.player._rig.velocity.y);
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
        Chronological(machine);
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

    void Chronological(CharaStateManager machine)
    {
        if (secs > 0f)
        {
            secs -= Time.deltaTime;
        }
        else
        {
            if (machine.player.isBoosting) machine.SwitchState(machine.boost);
            else
            {
                machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine._forwards:machine.suspended);
            }
        }
    }
}
