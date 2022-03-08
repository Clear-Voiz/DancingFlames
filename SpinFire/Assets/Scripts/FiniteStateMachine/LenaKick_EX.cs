using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenaKick_EX : CharaBaseState
{
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("LenaKick");
       var aniEnd = machine.player.anima.GetCurrentAnimatorStateInfo(0);
       secs = aniEnd.length;

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

    void Chronological(CharaStateManager machine)
    {
        if (secs > 0)
        {
            secs -= Time.deltaTime;
        }
        else
        {
            if (machine.player.isBoosting)
            {
                machine.SwitchState(machine.boost);
                return;
            }
            if (!machine.player.isGrounded)
            {
                if (machine.player._rig.velocity.y > -1)
                {
                    machine.SwitchState(machine.player._rig.velocity.y > -1? (CharaBaseState) machine.suspended : machine.descend);
                    return;
                }
            }
            machine.SwitchState(machine._forwards);
        }
    }
}
