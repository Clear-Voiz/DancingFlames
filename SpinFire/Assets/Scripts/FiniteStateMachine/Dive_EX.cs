using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Dive");
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision other)
    {
        
    }
}
