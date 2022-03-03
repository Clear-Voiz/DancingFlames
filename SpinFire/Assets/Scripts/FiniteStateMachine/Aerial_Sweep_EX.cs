using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Aerial_Sweep_EX : CharaBaseState
{
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("AerialSweep");
        var aniEnd = machine.player.anima.GetCurrentAnimatorStateInfo(0);
        secs = aniEnd.length * 3f;
    }

    public override void UpdateState(CharaStateManager machine)
    {
       if (machine.player.isGrounded) machine.SwitchState(machine.land);
       Chronological(machine);
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision other)
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
                machine.SwitchState(machine.player._rig.velocity.y>=-1f?(CharaBaseState) machine.suspended:machine.descend);
            }
        }
    }
}
