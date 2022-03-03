using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide_EX : CharaBaseState
{

    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("WallSlide");
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (!machine.player.isBoosting) machine.SwitchState(machine.suspended);
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision other)
    {
        
    }
}
