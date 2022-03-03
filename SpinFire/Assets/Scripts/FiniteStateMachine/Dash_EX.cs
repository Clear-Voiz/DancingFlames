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

    public override void UpdateState(CharaStateManager machine)
    {
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        
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
                machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine._forwards:machine.suspended);
            }
        }
    }
}
